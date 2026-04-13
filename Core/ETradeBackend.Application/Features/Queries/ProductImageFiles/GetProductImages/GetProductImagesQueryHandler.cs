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
        var product = await productReadRepository.Table
            .Include(p => p.ProductProductImageFiles)
            .ThenInclude(pif => pif.ProductImageFile)
            .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken: cancellationToken);
        
        return product == null ? null 
            : product.ProductProductImageFiles
                .Select(productProductImageFile => productProductImageFile.ProductImageFile)
                .Select(imageFile => 
                    new GetProductImagesQueryResponse(imageFile.Id, imageFile.Name, configuration["BaseStorageUrl"] + imageFile.Path)).ToList();
    }
}