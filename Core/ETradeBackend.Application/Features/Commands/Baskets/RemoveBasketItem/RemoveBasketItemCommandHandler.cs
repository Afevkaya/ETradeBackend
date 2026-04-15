using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Baskets.RemoveBasketItem;

public class RemoveBasketItemCommandHandler(
    IBasketService basketService) : IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResponse>
{
    public async Task<RemoveBasketItemCommandResponse> Handle(RemoveBasketItemCommandRequest request,
        CancellationToken cancellationToken)
    {
        await basketService.RemoveBasketItemAsync(request.BasketItemId);
        return new RemoveBasketItemCommandResponse();
    }
}