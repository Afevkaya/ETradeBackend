using MediatR;

namespace ETradeBackend.Application.Features.Queries.Products.GetAllProduct;

public record GetAllProductQueryRequest(int Page, int PageSize) : IRequest<GetAllProductQueryResponse>;