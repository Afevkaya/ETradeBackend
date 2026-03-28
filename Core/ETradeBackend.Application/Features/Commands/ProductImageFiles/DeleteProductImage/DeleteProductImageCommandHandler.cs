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
        var product = await productReadRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(p => p.Id == request.ProductId);
        var productImageFile = product?.ProductImageFiles.FirstOrDefault(pif => pif.Id == request.ImageId);
        if (productImageFile == null) return null;
        product?.ProductImageFiles.Remove(productImageFile);
        await productWriteRepository.SaveChangesAsync();
        return new();
    }
}