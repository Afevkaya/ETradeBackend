using MediatR;

namespace ETradeBackend.Application.Features.Queries.Orders.GetByIdOrderQuery;

public record GetByIdOrderQueryRequest(Guid Id) : IRequest<GetByIdOrderQueryResponse>;