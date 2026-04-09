using ETradeBackend.API.Extensions;
using ETradeBackend.Application.Extensions;
using ETradeBackend.Infrastructure.Extensions;
using ETradeBackend.Infrastructure.Services.Storages.Azure;
using ETradeBackend.Persistence.Extensions;
using Serilog;
using Serilog.Context;

var builder = WebApplication.CreateBuilder(args);

// Service registrations
builder.Services.AddEndpointsApiExplorer();
builder.Host.ConfigureSeriLog();
builder.Services
    .AddApplication()
    // .AddStorage<LocalStorage>()
    .AddStorage<AzureStorage>()
    .AddInfrastructure()
    .AddPersistence(builder.Configuration)
    .AddPresentation(builder.Configuration);

var app = builder.Build();

// Dev-only tools
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());
app.UseSerilogRequestLogging();
// HTTP request pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
// CORS, endpoint seçildikten sonra policy uygulasın
app.UseCors();

// AuthN/AuthZ sırası önemli
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var userName = context.User?.Identity?.IsAuthenticated == true
        ? context.User.Identity!.Name
        : "Unknown";
    using (LogContext.PushProperty("user_name", userName))
    {
        await next();
    }
});

app.MapControllers();

app.Run();