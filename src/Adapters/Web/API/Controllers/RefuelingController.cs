using AutoMais.Ticket.Core.Application.Ticket.Queries;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;

namespace AutoMais.Ticket.Api.Controllers
{
    public class RefuelingController : IEndpointDefinition
    {
        public void RegisterEndpoints(RouteGroupBuilder app)
        {
            var v1 = app.MapGroup("/v1/refueling").WithTags("Refueling");

            v1.MapGet("/{ticketId}", async ([FromRoute] string ticketId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var query = new TicketGetOne(ticketId);
                return await mediator.Send(query, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Get one specific ticket based on it's id"
            });

            #region Refueling Management

            v1.MapPost("/attendant/{cardId}/pump/{pumpNumber}/nozzles/{nozzleNumber}/start", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] string cardId,
                [FromRoute] int pumpNumber,
                [FromRoute] int nozzleNumber) =>
            {
                var command = new CreateTicketForAttendantCommand(cardId, pumpNumber, nozzleNumber);
                return await mediator.Send(command, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Creates a new ticket associated with the informed Attendant and adding the Pump/Nozzle as an authorized refueling"
            });


            v1.MapPost("/ticket/{ticketId}/pump/{pumpNumber}/nozzles/{nozzleNumber}", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] Guid ticketId,
                [FromRoute] int pumpNumber,
                [FromRoute] int nozzleNumber) =>
            {
                var command = new AuthorizeRefuelingForTicketCommand(ticketId, pumpNumber, nozzleNumber);
                return await mediator.Send(command, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "Creates a new ticket associated with the informed Attendant and adding the Pump/Nozzle as an authorized refueling"
            });


            v1.MapPatch("/attendant/{cardId}/pump/{pumpNumber}/nozzles/{nozzleNumber}/quantity/{quantity}/cost/{cost}", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] string cardId,
                [FromRoute] int pumpNumber,
                [FromRoute] int nozzleNumber,
                [FromRoute] decimal quantity,
                [FromRoute] decimal cost) =>
            {
                var command = new AddFuelToTicketCommand()
                {
                    CardId = cardId,
                    PumpNumber = pumpNumber,
                    NozzleNumber = nozzleNumber,
                    Quantity = quantity,
                    Cost = cost
                };
                return await mediator.Send(command, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "This operation Adds the Quantity (volume) and sum the Cost (price) in the current ticket"
            });

            v1.MapPatch("pump/{pumpNumber}/nozzle/{nozzleNumber}/finish", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] int pumpNumber,
                [FromRoute] int nozzleNumber) =>
            {
                var command = new FinishSupply(pumpNumber, nozzleNumber);
                return await mediator.Send(command, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "This operation replaces the Quantity (volume) and Cost (price) with the informed values in the current in progress ticket"
            });

            v1.MapPatch("pump/{pumpNumber}/nozzle/{nozzleNumber}/quantity/{quantity}/cost/{cost}", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] int pumpNumber,
                [FromRoute] int nozzleNumber,
                [FromRoute] decimal quantity,
                [FromRoute] decimal cost) =>
            {
                var query = new UpdateFuelToTicketCommand()
                {
                    PumpNumber = pumpNumber,
                    NozzleNumber = nozzleNumber,
                    Quantity = quantity,
                    Cost = cost
                };
                return await mediator.Send(query, cancellationToken);
            }).WithOpenApi(o => new(o)
            {
                Summary = "This operation replaces the Quantity (volume) and Cost (price) with the informed values in the current in progress ticket"
            });

            #endregion
        }
    }
}
