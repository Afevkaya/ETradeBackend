using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.CompletedOrder;

public class CompletedOrderCommandHandler(
    IOrderService orderService,
    IMailService mailService) : IRequestHandler<CompletedOrderCommandRequest, CompletedOrderCommandResponse>
{
    public async Task<CompletedOrderCommandResponse> Handle(CompletedOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var (success, data) = await orderService.CompleteOrderAsync(request.Id);
        if (success && data is not null)
            await mailService.SendCompletedOrderMailAsync(data.Email, data.OrderCode, data.OrderDate, data.NameSurname);
        
        return new CompletedOrderCommandResponse();
    }
}