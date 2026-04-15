using MediatR;

namespace ETradeBackend.Application.Features.Commands.Baskets.RemoveBasketItem;

public record RemoveBasketItemCommandRequest(Guid BasketItemId) : IRequest<RemoveBasketItemCommandResponse>;