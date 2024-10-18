using AutoMais.Ticket.Core.Application.Attendant.Queries;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Commands;

namespace AutoMais.Ticket.Api.Controllers;
public class AttendantController : IEndpointDefinition
{
    public void RegisterEndpoints(RouteGroupBuilder app)
    {

        var v1 = app.MapGroup("/v1/attendants").WithTags("Attendants");

        v1.MapGet("/{cardId}", async ([FromRoute] string cardId, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var query = new AttendantGetByCard(cardId);
            return await mediator.Send(query, cancellationToken);
        }).WithOpenApi(o => new(o)
        {
            Summary = "Retrieve attendant information based on this CardId",
            Description = "The attendant data is retrieved based on his card id"
        });

        v1.MapPatch("/{cardId}/disable", async ([FromRoute] string cardId, IMediator mediator, CancellationToken cancellationToken) =>
        {
            return await mediator.Send(new DisableAttendantCommand(cardId), cancellationToken);
        }).WithOpenApi(o => new(o)
        {
            Summary = "Disable the attendant based on his CardId"
        });

        v1.MapPatch("/{cardId}/enable", async ([FromRoute] string cardId, IMediator mediator, CancellationToken cancellationToken) =>
        {
            return await mediator.Send(new EnableAttendantCommand(cardId), cancellationToken);
        }).WithOpenApi(o => new(o)
        {
            Summary = "Enable the attendant based on his CardId"
        });

        v1.MapPost("/", async ([FromBody] CreateAttendantCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            return await mediator.Send(command, cancellationToken);
        }).WithOpenApi(o => new(o)
        {
            Summary = "Creates a new record for attendant"
        });

    }
}
