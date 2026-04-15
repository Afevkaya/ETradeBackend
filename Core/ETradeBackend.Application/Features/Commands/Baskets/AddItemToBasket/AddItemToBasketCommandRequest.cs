using MediatR;

namespace ETradeBackend.Application.Features.Commands.Baskets.AddItemToBasket;

public record AddItemToBasketCommandRequest(Guid ProductId, int Quantity) : IRequest<AddItemToBasketCommandResponse>;