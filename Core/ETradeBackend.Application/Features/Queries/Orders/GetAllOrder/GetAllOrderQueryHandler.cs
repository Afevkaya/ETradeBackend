using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Queries.Orders.GetAllOrder;

public class GetAllOrderQueryHandler(IOrderService orderService) : IRequestHandler<GetAllOrderQueryRequest, GetAllOrderQueryResponse>
{
    
    public async Task<GetAllOrderQueryResponse> Handle(GetAllOrderQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await orderService.GetAllOrdersAsync(request.Page, request.PageSize);
        return new GetAllOrderQueryResponse(data.TotalOrderCount, data.Orders);
    }

}