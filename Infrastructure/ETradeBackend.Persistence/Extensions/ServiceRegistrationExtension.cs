using ETradeBackend.Application.Repositories.Customers;
using ETradeBackend.Application.Repositories.Orders;
using ETradeBackend.Application.Repositories.Products;
using ETradeBackend.Persistence.Contexts;
using ETradeBackend.Persistence.Repositories.Customers;
using ETradeBackend.Persistence.Repositories.Orders;
using ETradeBackend.Persistence.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeBackend.Persistence.Extensions;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ETradeDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("ETradeDbConnection"));
        });
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        
        return services;
    }
}