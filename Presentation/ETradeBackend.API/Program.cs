using ETradeBackend.API.Configuration.ColumnWriters;
using ETradeBackend.API.Extensions;
using ETradeBackend.Application.Extensions;
using ETradeBackend.Infrastructure.Extensions;
using ETradeBackend.Infrastructure.Services.Storages.Azure;
using ETradeBackend.Persistence.Extensions;
using Serilog;
using Serilog.Context;
using Serilog.Sinks.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Service registrations
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((hostingContext, configuration) =>
{
    configuration.ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/log.txt")
        .WriteTo.PostgreSQL(
            connectionString: hostingContext.Configuration.GetConnectionString("ETradeDbConnection"),
            tableName: "logs",
            needAutoCreateTable: true,
            columnOptions: new Dictionary<string, ColumnWriterBase>
            {
                { "message", new RenderedMessageColumnWriter() },
                { "message_template", new MessageTemplateColumnWriter() },
                { "level", new LevelColumnWriter() },
                { "time_stamp", new TimestampColumnWriter() },
                { "exception", new ExceptionColumnWriter() },
                { "log_event", new LogEventSerializedColumnWriter() },
                {"user_name", new UserNameColumnWriter()}
            }
        )
        .WriteTo.Seq(hostingContext?.Configuration?["Seq:ServerUrl"] ?? null)
        .Enrich.FromLogContext()
        .MinimumLevel.Information();
});

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
    var userName = context.User?.Identity?.IsAuthenticated != null || true ? context.User?.Identity?.Name : "Unknown";
    LogContext.PushProperty("user_name", userName);
    await next.Invoke();
});

app.MapControllers();

app.Run();