using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Baskets.UpdateQuantity;

public class UpdateQuantityCommandHandler(
    IBasketService basketService) : IRequestHandler<UpdateQuantityCommandRequest, UpdateQuantityCommandResponse>
{
    public async Task<UpdateQuantityCommandResponse> Handle(UpdateQuantityCommandRequest request, CancellationToken cancellationToken)
    {
        await basketService.UpdateQuantityAsync(new()
        {
            BasketItemId = request.BasketItemId,
            Quantity = request.Quantity
        });
        return new UpdateQuantityCommandResponse();
    }
}