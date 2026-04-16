using ETradeBackend.Application.Abstractions.Hubs;
using ETradeBackend.SignalR.Constant;
using ETradeBackend.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ETradeBackend.SignalR.HubServices;

public class OrderHubService(IHubContext<OrderHub> hubContext) : IOrderHubService
{
    public async Task OrderAddedMessageAsync(string message)
    {
        await hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ReceiveOrderAddedMessage, message);
    }
}