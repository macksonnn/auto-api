using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Core.Common.Middlewares;

/// <summary>
/// This Middleware is responsible for ensure a default formatting in exception case
/// </summary>
public class ErrorMiddleware(RequestDelegate next, ILogger _logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);            
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = StatusCodes.Status500InternalServerError;

        var response = new
        {
            title = exception.Message,
            status = statusCode,
            detail = exception.InnerException?.Message,
            traceId = httpContext.TraceIdentifier
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

public static class ErrorMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorMiddleware>();
    }
}