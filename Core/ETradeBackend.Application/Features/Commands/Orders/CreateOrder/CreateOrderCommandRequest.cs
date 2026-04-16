using MediatR;

namespace ETradeBackend.Application.Features.Commands.Orders.CreateOrder;

public record CreateOrderCommandRequest(string Address, string Description) : IRequest<CreateOrderCommandResponse>;