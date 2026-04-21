using ETradeBackend.Application.Abstractions.Services;
using ETradeBackend.Application.DTOs.Orders.Requests;
using ETradeBackend.Application.DTOs.Orders.Responses;
using ETradeBackend.Application.Repositories.CompletedOrders;
using ETradeBackend.Application.Repositories.Orders;
using ETradeBackend.Domain.Entities.CompletedOrders;
using ETradeBackend.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Services;

public class OrderService(
    IOrderWriteRepository orderWriteRepository, 
    IOrderReadRepository orderReadRepository,
    ICompletedOrderWriteRepository completedOrderWriteRepository,
    ICompletedOrderReadRepository completedOrderReadRepository) : IOrderService
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

        var result = from o in data
            join completedOrder in completedOrderReadRepository.Table on o.Id equals completedOrder.OrderId into co
            from completed in co.DefaultIfEmpty()
            select new
            {
                Id = o.Id,
                CreatedDate = o.CreatedAt,
                Code = o.Code,
                Basket = o.Basket,
                Completed = completed != null
            };
        
        return new ListOrderResponse(totalCount, await result.Select(o => new
        {
            Id = o.Id,
            CreatedDate = o.CreatedDate,
            OrderCode = o.Code,
            TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
            UserName = o.Basket.User.UserName,
            o.Completed
        }).ToListAsync());

    }

    public async Task<SingleOrderResponse?> GetOrderByIdAsync(Guid id)
    {
        var data = orderReadRepository.Table
            .Include(x => x.Basket)
            .ThenInclude(b => b.BasketItems)
            .ThenInclude(d => d.Product);
        
            var result = await (from o in data
            join completedOrder in completedOrderReadRepository.Table on o.Id equals completedOrder.OrderId into co
            from completed in co.DefaultIfEmpty()
            select new
            {
                Id = o.Id,
                CreatedDate = o.CreatedAt,
                Code = o.Code,
                Basket = o.Basket,
                Complated = completed != null ? true : false,
                Address = o.Address,
                Description = o.Description,
            }).FirstOrDefaultAsync(o => o.Id == id);
        
        if(result == null) return null;
        
        return new SingleOrderResponse(result.Id,result.Address,result.Description,result.Code,result.Basket.BasketItems
            .Select(bi => new
            {
                ProductName = bi.Product.Name,
                Price = bi.Product.Price,
                Quantity = bi.Quantity,
                ItemTotalPrice = bi.Product.Price * bi.Quantity
            }),
            result.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),result.CreatedDate,result.Complated);
    }

    public async Task<(bool, CompletedOrderResponse?)> CompleteOrderAsync(Guid id)
    {
        var order = await orderReadRepository.Table.Include(x => x.Basket)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) return (false, null);
        await completedOrderWriteRepository.AddAsync(new CompletedOrder
        {
            OrderId = order.Id
        });
        await completedOrderWriteRepository.SaveChangesAsync();
        return (true, new CompletedOrderResponse(order.Code, order.CreatedAt, order.Basket.User.NameSurname, order.Basket.User.Email));
    }

    private static string GenerateOrderCode() => Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
}