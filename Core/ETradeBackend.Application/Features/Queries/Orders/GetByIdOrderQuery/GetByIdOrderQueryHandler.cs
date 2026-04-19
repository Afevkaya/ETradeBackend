using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Queries.Orders.GetByIdOrderQuery;

public class GetByIdOrderQueryHandler(IOrderService orderService) : IRequestHandler<GetByIdOrderQueryRequest, GetByIdOrderQueryResponse>
{
    public async Task<GetByIdOrderQueryResponse> Handle(GetByIdOrderQueryRequest request, CancellationToken cancellationToken)
    {
        var order = await orderService.GetOrderByIdAsync(request.Id);
        return order is null ? throw new Exception("Sipariş bulunamadı") : new GetByIdOrderQueryResponse(order.Id, order.Address, order.Description, order.OrderCode, order.OrderItems, order.BasketTotalPrice, order.CreatedDate);
    }
}