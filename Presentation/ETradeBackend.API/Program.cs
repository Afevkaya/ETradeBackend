using System.Text;
using ETradeBackend.API.Extensions;
using ETradeBackend.Application.Extensions;
using ETradeBackend.Infrastructure.Extensions;
using ETradeBackend.Infrastructure.Services.Storages.Azure;
using ETradeBackend.Persistence.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers(options =>
// {
//     options.Filters.Add<ValidationFilter>();
// }).ConfigureApiBehaviorOptions(options =>options.SuppressModelStateInvalidFilter = true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddApplication()
    // .AddStorage<LocalStorage>()
    .AddStorage<AzureStorage>()
    .AddInfrastructure()
    .AddPersistence(builder.Configuration)
    .AddPresentation();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Doğrulanacak/Kontrol edilecek parametreler
            ValidateAudience =
                true, // Oluşturulacak token değerini kimlerin/hangi originlerin/sitelerin kullanacağını belirlediğimiz değerdir --> www.bilmemne.com
            ValidateIssuer =
                true, // Oluşturulacak token değerini kimin dağıttığını belirlediğimiz alandır --> www.myapi.com
            ValidateLifetime = true, // Oluşturulacak token değerinin süresini kontrol edecek olan değerdir --> true
            ValidateIssuerSigningKey =
                true, // Oluşturulacak token değerini imzalarken kullanılan anahtarın doğruluğunu kontrol eden değerdir --> true

            // Doğrulama işlemi sırasında kullanılacak olan değerler
            ValidAudience =
                builder.Configuration
                    ["Token:Audience"], // Oluşturulacak token değerini kimlerin/hangi originlerin/sitelerin kullanacağını belirlediğimiz değerdir --> www.bilmemne.com
            ValidIssuer =
                builder.Configuration
                    ["Token:Issuer"], // Oluşturulacak token değerini kimin dağıttığını belirlediğimiz alandır --> www.myapi.com
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    builder.Configuration[
                        "Token:SecurityKey"]
                    ! // Oluşturulacak token değerini imzalarken kullanılan anahtarın doğruluğunu kontrol eden değerdir --> true
                ))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();