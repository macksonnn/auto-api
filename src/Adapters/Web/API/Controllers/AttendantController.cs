﻿using AutoMais.Ticket.Core.Application.Attendant.Queries;
using AutoMais.Ticket.Core.Application.Attendant.Queries.Get;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Commands;

namespace AutoMais.Ticket.Api.Controllers;
public class AttendantController : IEndpointDefinition
{
    public void RegisterEndpoints(RouteGroupBuilder app)
    {
        
        var v1 = app.MapGroup("/v1/attendants").WithTags("Attendants");

        v1.MapGet("/{id}", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var query = new AttendantGetOne(id);
            return await mediator.Send(query, cancellationToken);
        });

        v1.MapPatch("/{id}/disable", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            return await mediator.Send(new DisableAttendantCommand(id), cancellationToken);
        });

        v1.MapPatch("/{id}/enable", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            return await mediator.Send(new EnableAttendantCommand(id), cancellationToken);
        });

        v1.MapPost("/", async ([FromBody] CreateAttendantCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            return await mediator.Send(command, cancellationToken);
        });
        v1.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var query = new AttendantGetAll();
            return await mediator.Send(query, cancellationToken);
        });

    }
}
