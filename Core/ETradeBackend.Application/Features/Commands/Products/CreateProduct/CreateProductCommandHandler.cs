using ETradeBackend.Application.Abstractions.Hubs;
using ETradeBackend.Application.Repositories.Products;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Products.CreateProduct;

public class CreateProductCommandHandler(
    IProductWriteRepository productWriteRepository,
    IProductHubService productHubService): IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
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
        await productHubService.ProductAddedMessageAsync(request.Name + " added");
        return new();
    }
}