using ETradeBackend.Application.Abstractions.Services;
using ETradeBackend.Application.Abstractions.Services.Authentications;
using ETradeBackend.Application.Repositories.BasketItems;
using ETradeBackend.Application.Repositories.Baskets;
using ETradeBackend.Application.Repositories.CompletedOrders;
using ETradeBackend.Application.Repositories.Files;
using ETradeBackend.Application.Repositories.InvoiceFiles;
using ETradeBackend.Application.Repositories.Orders;
using ETradeBackend.Application.Repositories.ProductImageFiles;
using ETradeBackend.Application.Repositories.Products;
using ETradeBackend.Domain.Entities.Identities;
using ETradeBackend.Persistence.Contexts;
using ETradeBackend.Persistence.Repositories.BasketItems;
using ETradeBackend.Persistence.Repositories.Baskets;
using ETradeBackend.Persistence.Repositories.CompletedOrders;
using ETradeBackend.Persistence.Repositories.Files;
using ETradeBackend.Persistence.Repositories.InvoiceFiles;
using ETradeBackend.Persistence.Repositories.Orders;
using ETradeBackend.Persistence.Repositories.ProductImageFiles;
using ETradeBackend.Persistence.Repositories.Products;
using ETradeBackend.Persistence.Services;
using Microsoft.AspNetCore.Identity;
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
        services.AddIdentity<AppUser,AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        }).AddEntityFrameworkStores<ETradeDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();

        services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

        services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
        
        services.AddScoped<IBasketReadRepository,BasketReadRepository>();
        services.AddScoped<IBasketWriteRepository,BasketWriteRepository>();
        services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
        services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
        
        services.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
        services.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IInternalAuthentication, AuthService>();
        services.AddScoped<IExternalAuthentication, AuthService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}