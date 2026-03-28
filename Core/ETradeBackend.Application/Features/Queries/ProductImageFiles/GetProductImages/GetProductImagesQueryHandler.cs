using ETradeBackend.Application.Repositories.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ETradeBackend.Application.Features.Queries.ProductImageFiles.GetProductImages;

public class GetProductImagesQueryHandler(
    IProductReadRepository productReadRepository,
    IConfiguration configuration) : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
{
    public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await productReadRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken: cancellationToken);
        
        var response = new List<GetProductImagesQueryResponse>(product?.ProductImageFiles.Select(pif 
            => new GetProductImagesQueryResponse(pif.Id, pif.Name, pif.Path)).ToList() ?? []);
        return response;
    }
}