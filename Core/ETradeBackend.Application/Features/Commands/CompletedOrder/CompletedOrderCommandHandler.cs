using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.CompletedOrder;

public class CompletedOrderCommandHandler(
    IOrderService orderService) : IRequestHandler<CompletedOrderCommandRequest, CompletedOrderCommandResponse>
{
    public async Task<CompletedOrderCommandResponse> Handle(CompletedOrderCommandRequest request, CancellationToken cancellationToken)
    {
        await orderService.CompleteOrderAsync(request.Id);
        return new CompletedOrderCommandResponse();
    }
}