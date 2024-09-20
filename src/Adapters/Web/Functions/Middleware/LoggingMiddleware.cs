
namespace AutoMais.Functions.Middleware
{
    internal sealed class LoggingMiddleware : IFunctionsWorkerMiddleware
    {
        ILogger<LoggingMiddleware> _logger;
        public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var state = new Dictionary<string, object>
            {
                ["InvocationId"] = context.InvocationId,
            };

            if (context.BindingContext.BindingData.TryGetValue("MessageId", out var messageId) && messageId != null)
                state.Add("MessageId", messageId);

            if (context.BindingContext.BindingData.TryGetValue("CorrelationId", out var correlationId) && correlationId != null)
                state.Add("CorrelationId", correlationId);

            using (_logger.BeginScope(state))
            {
                foreach (var item in state)
                {
                    System.Diagnostics.Activity.Current?.AddBaggage(item.Key, item.Value.ToString());
                    System.Diagnostics.Activity.Current?.AddTag(item.Key, item.Value);
                }

                await next(context);
            }

            context.GetHttpResponseData()?.Headers.Add("InvocationId", context.InvocationId);
        }

    }
}
