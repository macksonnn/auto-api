using AutoMais.Ticket.Core.Application.Attendant.Queries;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Commands;
using Becape.Core.Common.Startup;

namespace AutoMais.Ticket.Api.Controllers;
public class AttendantController : IEndpointDefinition
{
    public void RegisterEndpoints(RouteGroupBuilder app)
    {
        var v1 = app.MapGroup("/api/v2/attendant");

        v1.MapGet("/{id}", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var query = new AttendantGetOne(id);
            return await mediator.Send(query, cancellationToken);
        });

        v1.MapPost("/", async ([FromBody] CreateAttendantCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            return await mediator.Send(command, cancellationToken);
        });
       
    }
}
