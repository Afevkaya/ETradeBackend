using ETradeBackend.Application.Repositories.Products;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Products.DeleteProduct;

public class DeleteProductCommandHandler(
    IProductWriteRepository productWriteRepository) : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
{
    public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        productWriteRepository.Delete(request.Id);
        await productWriteRepository.SaveChangesAsync();
        return new();
    }
}