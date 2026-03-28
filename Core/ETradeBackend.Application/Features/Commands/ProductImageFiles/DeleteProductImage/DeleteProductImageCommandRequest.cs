using MediatR;

namespace ETradeBackend.Application.Features.Commands.ProductImageFiles.DeleteProductImage;

public class DeleteProductImageCommandRequest : IRequest<DeleteProductImageCommandResponse>
{
    public Guid ProductId { get; set; }
    public Guid? ImageId { get; set; }
}