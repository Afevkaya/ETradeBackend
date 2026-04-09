using ETradeBackend.API.Configuration.ColumnWriters;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace ETradeBackend.API.Extensions;

public static class ConfigureSeriLogExtension
{
    public static void ConfigureSeriLog(this ConfigureHostBuilder builder)
    {
        builder.UseSerilog((hostingContext, configuration) =>
        {
            configuration.ReadFrom.Configuration(hostingContext.Configuration)
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
                        { "user_name", new UserNameColumnWriter()}
                    }
                )
                .WriteTo.Seq(hostingContext.Configuration["Seq:ServerUrl"] ?? string.Empty)
                .Enrich.FromLogContext()
                .MinimumLevel.Information();
        });
    }
}