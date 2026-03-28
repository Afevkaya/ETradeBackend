using MediatR;

namespace ETradeBackend.Application.Features.Commands.Products.DeleteProduct;

public record DeleteProductCommandRequest(Guid Id) : IRequest<DeleteProductCommandResponse>;