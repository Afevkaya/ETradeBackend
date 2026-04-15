using ETradeBackend.Application.DTOs.Baskets;
using ETradeBackend.Domain.Entities.Baskets;

namespace ETradeBackend.Application.Abstractions.Services;

public interface IBasketService
{
    Task<List<BasketItem>> GetBasketItemsAsync();
    Task AddItemToBasketAsync(CreateBasketItem basketItem);
    Task UpdateQuantityAsync(UpdateBasketItem basketItem);
    Task RemoveBasketItemAsync(Guid basketItemId);
}