using AutoMais.Ticket.Core.Application.Ticket.Queries;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;

namespace AutoMais.Ticket.Api.Controllers
{
    public class TicketEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(RouteGroupBuilder app)
        {
            var v1 = app.MapGroup("/v1/tickets").WithTags("Tickets");

            v1.MapGet("/{id}", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var query = new TicketGetOne(id);
                return await mediator.Send(query, cancellationToken);
            });

            v1.MapGet("/{id}/payments", async ([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return new List<dynamic>() {
                    new {
                        Id = "5d7dbee5-b97e-4d96-8a31-b690033954af",
                        Name = "Cartão de crédito",
                        Type = "Credit"
                    },
                    new {
                        Id = "689b4159-7f7d-47f1-aced-1a229eaefedd",
                        Name = "Cartão de débito",
                        Type = "Debit"
                    },
                    new {
                        Id = "7468e53b-8f59-4bcb-a391-09b0ac9a7d8f",
                        Name = "Pix",
                        Type = "Money"
                    },
                    new {
                        Id = "f5bdfbbe-a0e9-4a50-89cf-35aaddc2ef98",
                        Name = "Money",
                        Type = "Money"
                    }
                };
            });


            v1.MapGet("/attendant/{attendantId}", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] string attendantId,
                [FromQuery] int pageSize = 20,
                [FromQuery] int pageNumber = 1) =>
            {
                var query = new TicketsOfAttendant(attendantId);
                return await mediator.Send(query, cancellationToken);
            });



            v1.MapPost("/attendant/{attendantId}/pump/{pumpNumber}/nozzles/{nozzleNumber}", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] string attendantId,
                [FromRoute] int pumpNumber,
                [FromRoute] int nozzleNumber) =>
            {
                var query = new CreateTicketForAttendantCommand(attendantId, pumpNumber, nozzleNumber);
                return await mediator.Send(query, cancellationToken);
            })
            .WithDescription("Created a new ticket associated with the informed Attendant and adding the Pump/Nozzle as an authorized refueling");


            v1.MapPatch("fuel/pump/{pumpNumber}/nozzle/{nozzleNumber}/quantity/{quantity}/cost/{cost}", async (IMediator mediator, CancellationToken cancellationToken,
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
            })
            .WithDescription("This operation replaces the Quantity (volume) and Cost (price) with the informed values in the current in progress ticket .");



            v1.MapPatch("/attendant/{attendantId}/pump/{pumpNumber}/nozzles/{nozzleNumber}/quantity/{quantity}/cost/{cost}", async (IMediator mediator, CancellationToken cancellationToken,
                [FromRoute] string attendantId,
                [FromRoute] int pumpNumber,
                [FromRoute] int nozzleNumber,
                [FromRoute] decimal quantity,
                [FromRoute] decimal cost) =>
            {
                var query = new AddFuelToTicketCommand()
                {
                    CardId = attendantId,
                    PumpNumber = pumpNumber,
                    NozzleNumber = nozzleNumber,
                    Quantity = quantity,
                    Cost = cost
                };
                return await mediator.Send(query, cancellationToken);
            });


            v1.MapPost("/", async ([FromBody] CreateTicketCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return await mediator.Send(command, cancellationToken);
            });


            v1.MapPost("/{ticketId}/product", async (
                [FromRoute] string ticketId,
                [FromBody] AddProductToTicketCommand command,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                command.TicketId = ticketId;
                return await mediator.Send(command, cancellationToken);
            });

            v1.MapDelete("/{ticketId}/product/{productId}", async (
                [FromRoute] string ticketId,
                [FromRoute] string productId,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new RemoveProductFromTicketCommand(ticketId, productId);
                return await mediator.Send(command, cancellationToken);
            });


            v1.MapPatch("/{ticketId}/product/{productId}/quantity/{quantity}", async (
                [FromRoute] string ticketId,
                [FromRoute] string productId,
                [FromRoute] decimal quantity,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new ChangeProductQuantityOnTicketCommand(ticketId, productId, quantity);
                return await mediator.Send(command, cancellationToken);
            });


            v1.MapPatch("/{ticketId}/driver/{CPF}", async (
                [FromRoute] string ticketId,
                [FromRoute] string CPF,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new ChangeTicketDriverCommand(ticketId, CPF);
                return await mediator.Send(command, cancellationToken);
            });

            v1.MapPatch("/{ticketId}/vehicle/{Plate}", async (
                [FromRoute] string ticketId,
                [FromRoute] string Plate,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new ChangeVehicleCommand(ticketId, Plate);
                return await mediator.Send(command, cancellationToken);
            });
        }
    }
}
