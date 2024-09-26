using AutoMais.Ticket.Core.Application.Pump.Queries;
using AutoMais.Ticket.Core.Application.Ticket.Queries;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;
using Becape.Core.Common.Startup;

namespace AutoMais.Ticket.Api.Controllers
{
    public class PumpEndpoints : IEndpointDefinition
    {
        //TODO: Find a way to register a global endpoint filter to manipulare the result
        //TODO: view https://khalidabuhakmeh.com/global-endpoint-filters-with-aspnet-core-minimal-apis
        public void RegisterEndpoints(RouteGroupBuilder app)
        {
            var v1 = app.MapGroup("/api/v1/pumps");

            v1.MapGet("/{id}", async (IMediator mediator, CancellationToken cancellationToken, 
                [FromRoute] int id) =>
            {
                return await mediator.Send(new PumpGet(id), cancellationToken);
            });

            v1.MapGet("/{id}/nozzles", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] int id,
                [FromQuery] int pageSize = 10,
                [FromQuery] int pageNumber = 1) =>
            {
                return await mediator.Send(new NozzlesOfPump(id, pageSize, pageNumber), cancellationToken);
            });

            v1.MapDelete("/{pumpNumber}/nozzles/{nozzleId}", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] int pumpNumber,
                [FromRoute] int nozzleId) =>
            {
                return await mediator.Send(new RemoveNozzleCommand(pumpNumber, nozzleId), cancellationToken);
            });

            v1.MapPost("/", async (IMediator mediator, CancellationToken cancellationToken,
                [FromBody] CreateNewPumpCommand command) =>
            {
                return await mediator.Send(command, cancellationToken);
            });

            v1.MapPost("/{pumpNumber}/nozzles", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] int pumpNumber, 
                [FromBody] AddNozzleCommand command) =>
            {
                command.ChangePumpNumber(pumpNumber);
                return await mediator.Send(command, cancellationToken);
            });
        }
    }
}
