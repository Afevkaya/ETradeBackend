using ETradeBackend.Application.Abstractions.Services;
using ETradeBackend.Application.Abstractions.Services.Configurations;
using ETradeBackend.Application.Abstractions.Storages;
using ETradeBackend.Application.Abstractions.Tokens;
using ETradeBackend.Infrastructure.Enums;
using ETradeBackend.Infrastructure.Services.Configurations;
using ETradeBackend.Infrastructure.Services.Mails;
using ETradeBackend.Infrastructure.Services.Storages;
using ETradeBackend.Infrastructure.Services.Storages.Azure;
using ETradeBackend.Infrastructure.Services.Storages.Local;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokenHandler = ETradeBackend.Infrastructure.Services.Tokens.TokenHandler;

namespace ETradeBackend.Infrastructure.Extensions;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<IApplicationService, ApplicationService>();
            // .AddStorage<LocalStorage>()
        services.AddStorage<AzureStorage>();
        services.AddMailService(configuration);
        return services;
    }
    
    private static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        => services.AddScoped<IStorage, T>();
    
    private static void AddMailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMailService, MailService>();
        services.Configure<EmailSettings>(
            configuration.GetSection("EmailSettings"));
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