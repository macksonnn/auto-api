using AutoMais.Ticket.Core.Application.Attendant.State;
using AutoMais.Ticket.Core.Application.Pump.State;
using AutoMais.Ticket.Core.Application.Ticket.Adapters;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;


namespace AutoMais.Ticket.Core.Application.Ticket.Commands
{
    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class UpdateFuelToTicketCommandValidator : AbstractValidator<UpdateFuelToTicketCommand>
    {
        public UpdateFuelToTicketCommandValidator(IPumpState pumpState, IAttendantState attendantState)
        {
            RuleFor(command => command.PumpNumber)
                .NotEmpty()
                .MustAsync(async (instance, propValue, cancellationToken) =>
                {
                    instance.Pump = await pumpState.Get(x => x.Number == propValue);
                    return instance.Pump != null;
                }).DependentRules(() =>
                {
                    RuleFor(command => command.NozzleNumber)
                        .NotEmpty()
                        .Must((instance, property, token) =>
                        {
                            instance.Nozzle = instance?.Pump?.Nozzles?.FirstOrDefault(x => x.Number == property);
                            return instance?.Nozzle != null;
                        });
                });

            RuleFor(command => command.Quantity)
                .NotEmpty();

            RuleFor(command => command.Cost)
                .NotEmpty();
        }
    }

    public class UpdateFuelToTicketCommandHandler(ITicketState ticketState, IMediator mediator) : ICommandHandler<UpdateFuelToTicketCommand, TicketUpdated>
    {
        public async Task<Result<TicketUpdated>> Handle(UpdateFuelToTicketCommand command, CancellationToken cancellationToken)
        {
            var openedTicket = await ticketState.Get(x => 
                (x.Status == TicketStatusEnum.Opened || x.Status == TicketStatusEnum.InProgress) && 
                x.Supplies.Any(x => x.Pump.Number == command.PumpNumber && x.Pump.Nozzle.Number == command.NozzleNumber));

            if (openedTicket == null)
            {
                //var ticketCreated = await mediator.Send(new Createt(command.CardId, command.PumpNumber, command.NozzleNumber));

                //if (ticketCreated.IsSuccess)
                //    openedTicket = ticketCreated.Value.Ticket;
                //else
                    return Result.Fail<TicketUpdated>("Failure creating ticket")
                        .WithValidationError("PumpNumber", "No ticket in progress found")
                        .WithValidationError("NozzleNumber", "No ticket in progress found");
            }

            var result = Result.Ok();

            var updated = openedTicket.AddOrUpdateSupply(command);

            if (updated.IsSuccess)
            {
                var saved = await ticketState.Update(openedTicket.Id, updated.Value.Ticket);
                if (saved.IsSuccess)
                {
                    await mediator.Publish(updated.Value);
                    return updated.Value;
                }
                else
                    result.WithErrors(saved.Errors);
            }

            result.WithErrors(updated.Errors);

            return result;
        }
    }
}
