using ETradeBackend.Application.DTOs.Orders.Requests;
using ETradeBackend.Application.DTOs.Orders.Responses;

namespace ETradeBackend.Application.Abstractions.Services;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrder order);
    Task<ListOrderResponse> GetAllOrdersAsync(int page, int pageSize);
    Task<SingleOrderResponse?> GetOrderByIdAsync(Guid id);
}