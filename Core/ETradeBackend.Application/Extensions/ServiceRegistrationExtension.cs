using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeBackend.Application.Extensions;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceRegistrationExtension).Assembly);
        return services;
    }
}