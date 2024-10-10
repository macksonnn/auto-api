using AutoMais.Ticket.Core.Application.Product.Queries;
using AutoMais.Ticket.Core.Domain.Aggregates.Product.Commands;

namespace AutoMais.Ticket.Api.Controllers
{
    public class ProductEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(RouteGroupBuilder app)
        {
            var v1 = app.MapGroup("/api/v1/products");

            v1.MapGet("/{id}", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var query = new ProductGetOne(id);
                return await mediator.Send(query, cancellationToken);
            });

            v1.MapPost("/", async ([FromBody] CreateProductCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return await mediator.Send(command, cancellationToken);
            });

            v1.MapPut("/{id}", async ([FromRoute] string id, [FromBody] UpdateProductCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                command.Id = id;
                return await mediator.Send(command, cancellationToken);
            });

            v1.MapPatch("/{id}/disable", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return await mediator.Send(new DisableProductCommand(id), cancellationToken);
            });

            v1.MapPatch("/{id}/enable", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return await mediator.Send(new EnableProductCommand(id), cancellationToken);
            });

        }
    }
}
