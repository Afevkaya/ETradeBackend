using ETradeBackend.Application.Repositories.Products;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Products.CreateProduct;

public class CreateProductCommandHandler(
    IProductWriteRepository productWriteRepository): IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        await productWriteRepository.AddAsync(new()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        });
        await productWriteRepository.SaveChangesAsync();
        return new();
    }
}