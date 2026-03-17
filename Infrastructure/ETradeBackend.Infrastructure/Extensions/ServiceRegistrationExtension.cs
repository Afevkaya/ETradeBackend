using ETradeBackend.Application.Services;
using ETradeBackend.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeBackend.Infrastructure.Extensions;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
        return services;
    }
}