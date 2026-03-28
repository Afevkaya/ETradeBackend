using MediatR;

namespace ETradeBackend.Application.Features.Commands.Products.UpdateProduct;

public record UpdateProductCommandRequest(Guid Id, string Name, decimal Price, int Stock) : IRequest<UpdateProductCommandResponse>;