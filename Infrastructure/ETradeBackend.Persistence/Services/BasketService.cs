using ETradeBackend.Application.Abstractions.Services;
using ETradeBackend.Application.DTOs.Baskets;
using ETradeBackend.Application.Exceptions;
using ETradeBackend.Application.Repositories.BasketItems;
using ETradeBackend.Application.Repositories.Baskets;
using ETradeBackend.Application.Repositories.Orders;
using ETradeBackend.Domain.Entities.Baskets;
using ETradeBackend.Domain.Entities.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Services;

public class BasketService(
    IHttpContextAccessor httpContextAccessor,
    UserManager<AppUser> userManager,
    IOrderReadRepository orderReadRepository,
    IBasketWriteRepository basketWriteRepository,
    IBasketReadRepository basketReadRepository,
    IBasketItemReadRepository basketItemReadRepository,
    IBasketItemWriteRepository basketItemWriteRepository) : IBasketService
{
    public async Task<List<BasketItem>> GetBasketItemsAsync()
    {
        var basket = await ContextUser();
        if (basket is null)
            return [];
            
        var result = await basketReadRepository.Table
            .Include(b => b.BasketItems.Where(bi => !bi.IsDeleted && !bi.Product.IsDeleted))
            .ThenInclude(bi => bi.Product)
            .FirstOrDefaultAsync(b => b.Id == basket.Id);
        return result?.BasketItems.ToList() ?? [];
    }

    public async Task AddItemToBasketAsync(CreateBasketItem basketItem)
    {
        var basket = await ContextUser(); 
        if (basket == null) throw new BasketNotFoundException(); 
        var existingBasketItem = await basketItemReadRepository
            .GetSingleWhereAsync(b => b.BasketId == basket.Id && b.ProductId == basketItem.ProductId);
        if (existingBasketItem != null) existingBasketItem.Quantity += basketItem.Quantity;
        else
        {
            await basketItemWriteRepository.AddAsync(
                new()
                {
                    BasketId = basket.Id, ProductId = basketItem.ProductId, Quantity = basketItem.Quantity
                });
        }
        
        await basketWriteRepository.SaveChangesAsync();
    }

    public async Task UpdateQuantityAsync(UpdateBasketItem basketItem)
    {
        var existingBasketItem = await basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
        if (existingBasketItem == null) throw new BasketNotFoundException();
        existingBasketItem.Quantity = basketItem.Quantity;
        await basketItemWriteRepository.SaveChangesAsync();
    }

    public async Task RemoveBasketItemAsync(Guid basketItemId)
    {
        var _basketItem = await basketItemReadRepository.GetByIdAsync(basketItemId);
        if (_basketItem == null) throw new BasketNotFoundException();
        basketItemWriteRepository.Delete(_basketItem);
        await basketItemWriteRepository.SaveChangesAsync();
    }

    public Basket? GetUserActiveBasket { get => ContextUser().Result; }


    private async Task<Basket?> ContextUser()
    {
        var userName = httpContextAccessor.HttpContext?.User?.Identity?.Name;
        if (string.IsNullOrEmpty(userName)) return null;
        
        var user = await userManager.Users.Include(u=>u.Baskets)
            .FirstOrDefaultAsync(u => u.UserName == userName);
        if (user == null) return null;

        var basket = from userBasket in user.Baskets
            join order in orderReadRepository.Table on userBasket.Id equals order.Id
                into BasketOrder
            from order in BasketOrder.DefaultIfEmpty()
            select new { Basket = userBasket, Order = order };

        Basket? targetBasket;
        if(basket.Any(b => b.Order == null))
            targetBasket = basket.FirstOrDefault(b => b.Order == null)?.Basket;
        else
        {
            targetBasket = new();
            user.Baskets.Add(targetBasket);
        }

        await basketWriteRepository.SaveChangesAsync();
        return targetBasket;
    }
}