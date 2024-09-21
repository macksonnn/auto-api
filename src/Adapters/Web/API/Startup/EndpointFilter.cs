namespace AutoMais.Ticket.Api.Startup
{
    public class ScreamingFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var result = await next(context);
            return result is string s
                ? $"{s}!!!!"
                : result;
        }
    }
}
