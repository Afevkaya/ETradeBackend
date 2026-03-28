using ETradeBackend.Application.Repositories.Products;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Products.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductReadRepository productReadRepository,
    IProductWriteRepository productWriteRepository) : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await productReadRepository.GetSingleWhereAsync(x => x.Id == request.Id);
        if (product == null) return null;
        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;
        await productWriteRepository.SaveChangesAsync();
        return new();
    }
}