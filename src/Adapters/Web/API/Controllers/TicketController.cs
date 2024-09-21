using AutoMais.Ticket.Core.Application.Ticket.Queries;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;
using Becape.Core.Common.Startup;

namespace AutoMais.Ticket.Api.Controllers
{
    public class TicketEndpoints : IEndpointDefinition
    {
        //TODO: Find a way to register a global endpoint filter to manipulare the result
        //TODO: view https://khalidabuhakmeh.com/global-endpoint-filters-with-aspnet-core-minimal-apis
        public void RegisterEndpoints(RouteGroupBuilder app)
        {
            var v1 = app.MapGroup("/api/v1");

            v1.MapGet("/ticket/{id}", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var query = new TicketGetOne(id);
                return await mediator.Send(query, cancellationToken);
            });

            v1.MapPost("/ticket/", async ([FromBody] CreateTicketCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return await mediator.Send(command, cancellationToken);
            });


            v1.MapPost("/ticket/{id}/product", async ([FromRoute] string id, [FromBody] AddProductToTicketCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                command.ChangeTicket(id);                
                return await mediator.Send(command, cancellationToken);
            });
        }
    }
}
