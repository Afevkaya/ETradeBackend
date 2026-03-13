using ETradeBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeBackend.Persistence.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ETradeDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("ETradeDbConnection"));
        });
        
        return services;
    }
}