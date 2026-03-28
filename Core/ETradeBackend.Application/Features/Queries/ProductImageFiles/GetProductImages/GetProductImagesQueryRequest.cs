using MediatR;

namespace ETradeBackend.Application.Features.Queries.ProductImageFiles.GetProductImages;

public record GetProductImagesQueryRequest(Guid ProductId) : IRequest<List<GetProductImagesQueryResponse>>;