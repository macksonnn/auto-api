using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Core.Common.Middlewares;

public class LoggingMiddleware(RequestDelegate next, ILogger logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (string.IsNullOrEmpty(context.TraceIdentifier))
            context.TraceIdentifier = Guid.NewGuid().ToString();

        var state = new Dictionary<string, object>
        {
            ["TraceId"] = context.TraceIdentifier
        };

        //if (context.Request.BindingData.TryGetValue("MessageId", out var messageId) && messageId != null)
        //    state.Add("MessageId", messageId);

        //if (context.BindingContext.BindingData.TryGetValue("CorrelationId", out var correlationId) && correlationId != null)
        //    state.Add("CorrelationId", correlationId);

        using (logger.BeginScope(state))
        {
            foreach (var item in state)
            {
                System.Diagnostics.Activity.Current?.AddBaggage(item.Key, item.Value.ToString());
                System.Diagnostics.Activity.Current?.AddTag(item.Key, item.Value);
            }

            context.Response.Headers.Add("TraceId", context.TraceIdentifier);
            await next(context);
        }
    }
}

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}
