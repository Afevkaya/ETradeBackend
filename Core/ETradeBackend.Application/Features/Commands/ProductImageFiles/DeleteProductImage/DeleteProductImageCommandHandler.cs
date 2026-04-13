using ETradeBackend.Application.Repositories.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Application.Features.Commands.ProductImageFiles.DeleteProductImage;

public class DeleteProductImageCommandHandler(
    IProductReadRepository productReadRepository,
    IProductWriteRepository productWriteRepository) : IRequestHandler<DeleteProductImageCommandRequest, DeleteProductImageCommandResponse>
{
    public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await productReadRepository.Table.Include(p => p.ProductProductImageFiles.Select(pif => pif.ProductImageFile))
            .FirstOrDefaultAsync(p => p.Id == request.ProductId);
        var productImageFile = product?.ProductProductImageFiles.FirstOrDefault(pif => pif.ProductImageFileId == request.ImageId)?.ProductImageFile;
        
        
        if (productImageFile == null) return null;
        product?.ProductProductImageFiles.Remove(product.ProductProductImageFiles.FirstOrDefault(pif => pif.ProductImageFileId == request.ImageId));
        await productWriteRepository.SaveChangesAsync();
        return new();
    }
}