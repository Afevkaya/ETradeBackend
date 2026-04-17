using MediatR;

namespace ETradeBackend.Application.Features.Queries.Orders.GetAllOrder;

public record GetAllOrderQueryRequest(int Page, int PageSize) : IRequest<GetAllOrderQueryResponse>;