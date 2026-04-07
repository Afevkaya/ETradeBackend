using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;

namespace ETradeBackend.API.Extensions;

public static class ConfigureExceptionHandlerExtension
{
    public static void ConfigureExceptionHandler(this WebApplication app, ILogger logger)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                if (feature is not null)
                {
                    logger.LogError(
                        feature.Error,
                        "Unhandled exception. Path: {Path}, TraceId: {TraceId}",
                        context.Request.Path,
                        context.TraceIdentifier
                    );

                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "An unexpected error occurred.",
                        Details = feature.Error.Message
                    });
                }
            });
        });
    }
}