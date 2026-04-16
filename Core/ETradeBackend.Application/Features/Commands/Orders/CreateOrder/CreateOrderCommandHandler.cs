using ETradeBackend.Application.Abstractions.Hubs;
using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Orders.CreateOrder;

public class CreateOrderCommandHandler(
    IOrderService orderService,
    IBasketService basketService,
    IOrderHubService orderHubService) : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
{
    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        if (basketService.GetUserActiveBasket == null) return new CreateOrderCommandResponse();
        await orderService.CreateOrderAsync(new DTOs.Orders.CreateOrder(request.Address, request.Description,
            basketService.GetUserActiveBasket.Id));
        await orderHubService.OrderAddedMessageAsync("Yeni bir sipariş oluşturuldu.");

        return new CreateOrderCommandResponse();
    }
}