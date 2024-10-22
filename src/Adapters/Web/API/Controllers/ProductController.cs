using AutoMais.Ticket.Core.Application.Product.Queries;
using AutoMais.Ticket.Core.Domain.Aggregates.Product.Commands;

namespace AutoMais.Ticket.Api.Controllers
{
    public class ProductEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(RouteGroupBuilder app)
        {
            var v1 = app.MapGroup("/v1/products").WithTags("Products");

            v1.MapGet("/", async ([FromQuery] string q, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var query = new ProductSearch(q);
                return await mediator.Send(query, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Search for active products based on the Query parameter"
            });

            v1.MapGet("/{id}", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var query = new ProductGetOne(id);
                return await mediator.Send(query, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Get on specific product based on it's unique id"
            });

            v1.MapPost("/", async ([FromBody] CreateProductCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return await mediator.Send(command, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Creates a new product record"
            });

            v1.MapPut("/{id}", async ([FromRoute] string id, [FromBody] UpdateProductCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                command.Id = id;
                return await mediator.Send(command, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Updates the product record"
            });

            v1.MapPatch("/{id}/disable", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return await mediator.Send(new DisableProductCommand(id), cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Disables a product based on it's unique id"
            });

            v1.MapPatch("/{id}/enable", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return await mediator.Send(new EnableProductCommand(id), cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Enalbe a product based on it's unique id"
            });
        }
    }
}
