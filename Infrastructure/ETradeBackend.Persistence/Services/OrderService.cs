using ETradeBackend.Application.Abstractions.Services;
using ETradeBackend.Application.DTOs.Orders.Requests;
using ETradeBackend.Application.DTOs.Orders.Responses;
using ETradeBackend.Application.Repositories.Orders;
using ETradeBackend.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Services;

public class OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository) : IOrderService
{
    public async Task CreateOrderAsync(CreateOrder order)
    {
        const int maxRetryCount = 3;

        if (order.BasketId == Guid.Empty) return;

        for (var attempt = 1; attempt <= maxRetryCount; attempt++)
        {
            var code = GenerateOrderCode();

            await orderWriteRepository.AddAsync(new Order
            {
                Address = order.Address,
                Description = order.Description,
                Id = order.BasketId,
                Code = code
            });

            try
            {
                await orderWriteRepository.SaveChangesAsync();
                return;
            }
            catch (DbUpdateException ex)
            {
                foreach (var entry in ex.Entries)
                    entry.State = EntityState.Detached;

                var isCodeConflict = await orderReadRepository.GetWhere(o => o.Code == code, tracking: false).AnyAsync();
                if (!isCodeConflict)
                    throw;

                if (attempt == maxRetryCount)
                    throw new InvalidOperationException("Birden fazla denemeye rağmen sipariş kodu benzersiz bir şekilde oluşturulamadı.", ex);
            }
        }
    }

    public async Task<ListOrderResponse> GetAllOrdersAsync(int page, int pageSize)
    {
        var query = orderReadRepository.Table.Include(x => x.Basket)
            .ThenInclude(c => c.User)
            .Include(a => a.Basket)
            .ThenInclude(b => b.BasketItems)
            .ThenInclude(d => d.Product);
        var totalCount = await query.CountAsync();
        var data = query.Skip((page - 1) * pageSize).Take(pageSize);
        return new ListOrderResponse(totalCount, await data.Select(o => new
        {
            Id = o.Id,
            CreatedDate = o.CreatedAt,
            OrderCode = o.Code,
            TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
            UserName = o.Basket.User.UserName,
        }).ToListAsync());

    }

    public async Task<SingleOrderResponse?> GetOrderByIdAsync(Guid id)
    {
        var data = await orderReadRepository.Table
            .Include(x => x.Basket)
            .ThenInclude(b => b.BasketItems)
            .ThenInclude(d => d.Product).FirstOrDefaultAsync(o => o.Id == id);
        if(data == null) return null;
        return new SingleOrderResponse(data.Id,data.Address,data.Description,data.Code,data.Basket.BasketItems.Select(bi => new
        {
            ProductName = bi.Product.Name,
            Price = bi.Product.Price,
            Quantity = bi.Quantity,
            ItemTotalPrice = bi.Product.Price * bi.Quantity
        }),data.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),data.CreatedAt);
    }

    private static string GenerateOrderCode() => Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
}