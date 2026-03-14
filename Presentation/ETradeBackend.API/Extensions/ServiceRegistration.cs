namespace ETradeBackend.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddCors(options => options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins("http://localhost:3000","https://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        
        services.AddSwaggerGen();
        
        return services;
    }
}