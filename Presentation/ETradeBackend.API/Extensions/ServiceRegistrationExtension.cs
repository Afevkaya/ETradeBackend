using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ETradeBackend.API.Extensions;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        services.AddControllers();
        services.AddSwaggerGenExtension();
        services.AddLoggingExtension();
        services.AddAuthenticationExtension(configuration);
        services.AddAuthorization();

        return services;
    }
    
    private static IServiceCollection AddSwaggerGenExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT token'ı şu formatta gir: Bearer {token}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
    private static IServiceCollection AddControllersExtension(this IServiceCollection services)
    {
        // services.AddControllers(options =>
        // {
        //     options.Filters.Add<ValidationFilter>();
        // }).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
        
        services.AddControllers();
        return services;
    }
    private static IServiceCollection AddAuthenticationExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Admin";
                options.DefaultChallengeScheme = "Admin";
            })
            .AddJwtBearer("Admin", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = configuration["Token:Audience"],
                    ValidIssuer = configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!)
                    ),
                    NameClaimType = ClaimTypes.Name,

                    // Token süresi bittiği anda geçersiz olsun (varsayılan 5 dk toleransı kaldırır)
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
    private static IServiceCollection AddLoggingExtension(this IServiceCollection services)
    {
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.RequestHeaders.Add("sec-ch-ua");
            logging.MediaTypeOptions.AddText("application/javascript");
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
            logging.CombineLogs = true;
        });
        
        return services;
    }
}