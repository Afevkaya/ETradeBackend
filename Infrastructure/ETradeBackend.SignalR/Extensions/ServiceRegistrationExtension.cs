using ETradeBackend.Application.Abstractions.Hubs;
using ETradeBackend.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeBackend.SignalR.Extensions;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddSignalRServices(this IServiceCollection services)
    {
        services.AddTransient<IProductHubService, ProductHubService>();
        services.AddTransient<IOrderHubService, OrderHubService>();
        services.AddSignalR();
        return services;
    }
}