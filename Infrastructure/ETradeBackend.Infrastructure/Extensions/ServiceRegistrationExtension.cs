using ETradeBackend.Application.Abstractions.Storages;
using ETradeBackend.Infrastructure.Enums;
using ETradeBackend.Infrastructure.Services.Storages;
using ETradeBackend.Infrastructure.Services.Storages.Azure;
using ETradeBackend.Infrastructure.Services.Storages.Local;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeBackend.Infrastructure.Extensions;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, StorageService>();
        return services;
    }
    
    public static IServiceCollection AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
    {
        services.AddScoped<IStorage, T>();
        return services;
    }
    
    public static IServiceCollection AddStorage(this IServiceCollection services, StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Local:
                services.AddScoped<IStorage, LocalStorage>();
                return services;
            case StorageType.Azure:
                services.AddScoped<IStorage, AzureStorage>();
                return services;
            case StorageType.Aws:
                // AWS Storage için gerekli servis kaydı yapılacak
                return services;
            case StorageType.GoogleCloud:
                // Google Cloud Storage için gerekli servis kaydı yapılacak
                return services;
            default:
                services.AddScoped<IStorage, LocalStorage>();
                return services;
        }
    }
}