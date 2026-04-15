using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Baskets.AddItemToBasket;

public class AddItemToBasketCommandHandler(
    IBasketService basketService) : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
{
    public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
    {
        await basketService.AddItemToBasketAsync(new()
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity
        });
        return new AddItemToBasketCommandResponse();
    }
}