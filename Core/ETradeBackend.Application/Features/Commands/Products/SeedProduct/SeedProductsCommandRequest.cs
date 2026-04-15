using MediatR;

namespace ETradeBackend.Application.Features.Commands.Products.SeedProduct;

public record SeedProductsCommandRequest() : IRequest<string>;