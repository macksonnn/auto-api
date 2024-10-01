using AutoMais.Ticket.Core.Application.Product.Queries;
using AutoMais.Ticket.Core.Domain.Aggregates.Product.Commands;

namespace AutoMais.Ticket.Api.Controllers
{
    public class ProductEndpoints : IEndpointDefinition
    {
        //TODO: Find a way to register a global endpoint filter to manipulare the result
        //TODO: view https://khalidabuhakmeh.com/global-endpoint-filters-with-aspnet-core-minimal-apis
        public void RegisterEndpoints(RouteGroupBuilder app)
        {
            var v1 = app.MapGroup("/api/v1");

            v1.MapGet("/product/{id}", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var query = new ProductGetOne(id);
                return await mediator.Send(query, cancellationToken);
            });

            v1.MapPost("/product/", async ([FromBody] CreateProductCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return await mediator.Send(command, cancellationToken);
            });


            //v1.MapPut("/product/{id}", async ([FromRoute] string id, [FromBody] UpdateProductCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            //{
            //    command.ChangeTicket(id);                
            //    return await mediator.Send(command, cancellationToken);
            //});
        }
    }
}
