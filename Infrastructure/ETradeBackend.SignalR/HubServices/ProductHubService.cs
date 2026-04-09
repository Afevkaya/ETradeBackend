using ETradeBackend.Application.Abstractions.Hubs;
using ETradeBackend.SignalR.Constant;
using ETradeBackend.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ETradeBackend.SignalR.HubServices;

public class ProductHubService(IHubContext<ProductHub> hubContext) : IProductHubService
{
    public Task ProductAddedMessageAsync(string message) 
        => hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ReceiveProductAddedMessage, message);
}