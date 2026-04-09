using ETradeBackend.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace ETradeBackend.SignalR.Extensions;

public static class HubRegistration
{
    public static void MapHubs(this WebApplication app)
    {
        app.MapHub<ProductHub>("/products-hub");
    }
}