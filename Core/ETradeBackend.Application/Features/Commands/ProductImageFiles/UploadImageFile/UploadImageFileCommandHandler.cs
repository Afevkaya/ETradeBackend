using ETradeBackend.Application.Abstractions.Storages;
using ETradeBackend.Application.Repositories.ProductImageFiles;
using ETradeBackend.Application.Repositories.Products;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Domain.Entities.Files;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.ProductImageFiles.UploadImageFile;

public class UploadImageFileCommandHandler(
    IStorageService storageService,
    IProductReadRepository productReadRepository,
    IProductWriteRepository productWriteRepository,
    IProductImageFileWriteRepository productImageFileWriteRepository) : IRequestHandler<UploadImageFileCommandRequest, UploadImageFileCommandResponse>
{
    public async Task<UploadImageFileCommandResponse> Handle(UploadImageFileCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await storageService.UploadAsync("product-images", request.FormFileCollection);
        var product = await productReadRepository.GetByIdAsync(request.ProductId);
        if (product == null) return null;
        await productImageFileWriteRepository.AddRangeAsync(result.Select(r=> new ProductImageFile
        {
            Name = r.fileName,
            Path = r.pathOrContainerName,
            Storage = storageService.StorageName,
            ProductProductImageFiles = new List<ProductProductImageFiles>
            {
                new()
                {
                    Product = product
                }
            }
        }).ToList());
            
        await productWriteRepository.SaveChangesAsync();
        return new();
    }
}