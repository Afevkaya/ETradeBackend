using ETradeBackend.Application.Repositories.ProductImageFiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Application.Features.Commands.ProductImageFiles.ChangeShowcaseImage;

public class ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository) 
    : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
{

    public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request,
        CancellationToken cancellationToken)
    {
        var result = await productImageFileWriteRepository.Table.Include(x => x.ProductProductImageFiles)
            .Where(x => x.ProductProductImageFiles.Any(p => p.ProductId == request.ProductId))
            .ToListAsync(cancellationToken: cancellationToken);

        foreach (var item in result)
        {
            var productImageFile = item.ProductProductImageFiles.FirstOrDefault(x => x.ProductId == request.ProductId);
            if (productImageFile != null) productImageFile.IsShowcase = item.Id == request.ImageId;

            productImageFileWriteRepository.Update(item);
        }

        await productImageFileWriteRepository.SaveChangesAsync();
        return new();
    }
}