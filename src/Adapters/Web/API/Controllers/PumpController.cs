using AutoMais.Ticket.Core.Application.Pump.Queries;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump.Commands;

namespace AutoMais.Ticket.Api.Controllers
{
    public class PumpEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(RouteGroupBuilder app)
        {
            var v1 = app.MapGroup("/v1/pumps").WithTags("Pumps");

            v1.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
            {
                return await mediator.Send(new PumpGetAll(), cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "List all pumps registered"
            });

            v1.MapGet("/{pumpNumber}", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] int pumpNumber) =>
            {
                return await mediator.Send(new PumpGetbyNumber(pumpNumber), cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Get the pump record based on it's Number"
            });

            v1.MapGet("/{pumpNumber}/nozzles", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] int pumpNumber,
                [FromQuery] int pageSize = 10,
                [FromQuery] int pageNumber = 1) =>
            {
                return await mediator.Send(new NozzlesOfPump(pumpNumber, pageSize, pageNumber), cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Get all the nozzles from a pump based on it's Nmber"
            });

            v1.MapDelete("/{pumpNumber}/nozzles/{nozzleNumber}", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] int pumpNumber,
                [FromRoute] int nozzleNumber) =>
            {
                return await mediator.Send(new RemoveNozzleCommand(pumpNumber, nozzleNumber), cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Delete the specific nozzle record from the specified pump"
            });

            v1.MapPatch("/{pumpNumber}/nozzles/{nozzleNumber}/fuel", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] int pumpNumber,
                [FromRoute] int nozzleNumber,
                [FromBody] ChangeNozzleFuelCommand command) =>
            {
                command.NozzleId = nozzleNumber;
                command.PumpNumber = pumpNumber;

                return await mediator.Send(command, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Change the fuel type of the specified nozzle"
            });

            v1.MapPost("/", async (IMediator mediator, CancellationToken cancellationToken,
                [FromBody] CreateNewPumpCommand command) =>
            {
                return await mediator.Send(command, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Creates a new Pump record"
            });

            v1.MapPost("/{pumpNumber}/nozzles", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] int pumpNumber,
                [FromBody] CreateNozzleCommand command) =>
            {
                command.ChangePumpNumber(pumpNumber);
                return await mediator.Send(command, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Creates a new nozzle in the specified Pump"
            });
        }
    }
}
