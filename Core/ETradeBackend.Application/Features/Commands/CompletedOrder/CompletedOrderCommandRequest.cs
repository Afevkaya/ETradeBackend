using MediatR;

namespace ETradeBackend.Application.Features.Commands.CompletedOrder;

public record CompletedOrderCommandRequest(Guid Id) : IRequest<CompletedOrderCommandResponse>;