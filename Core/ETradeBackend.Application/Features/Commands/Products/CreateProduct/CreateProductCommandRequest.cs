using MediatR;

namespace ETradeBackend.Application.Features.Commands.Products.CreateProduct;

public record CreateProductCommandRequest(string Name, decimal Price, int Stock) : IRequest<CreateProductCommandResponse>;