using ETradeBackend.Application.Abstractions.Services;
using ETradeBackend.Application.DTOs.Orders;
using ETradeBackend.Application.Repositories.Orders;
using ETradeBackend.Domain.Entities;

namespace ETradeBackend.Persistence.Services;

public class OrderService(IOrderWriteRepository orderWriteRepository) : IOrderService
{
    public async Task CreateOrderAsync(CreateOrder order)
    {
        if (order.BasketId == Guid.Empty) return;
        await orderWriteRepository.AddAsync(new Order
        {
            Address = order.Address,
            Description = order.Description,
            Id = order.BasketId
        });
        await orderWriteRepository.SaveChangesAsync();
    }
}