using ETradeBackend.Application.DTOs.Orders;

namespace ETradeBackend.Application.Abstractions.Services;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrder order);
}