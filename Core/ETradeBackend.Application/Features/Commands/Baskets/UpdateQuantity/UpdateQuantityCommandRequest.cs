using MediatR;

namespace ETradeBackend.Application.Features.Commands.Baskets.UpdateQuantity;

public record UpdateQuantityCommandRequest(Guid BasketItemId, int Quantity) : IRequest<UpdateQuantityCommandResponse>;