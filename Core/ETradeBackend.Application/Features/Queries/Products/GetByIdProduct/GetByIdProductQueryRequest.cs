using MediatR;

namespace ETradeBackend.Application.Features.Queries.Products.GetByIdProduct;

public record GetByIdProductQueryRequest(Guid Id) : IRequest<GetByIdProductQueryResponse>;