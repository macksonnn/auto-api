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
    public sealed class AddFuelToTicketCommandValidator : AbstractValidator<AddFuelToTicketCommand>
    {
        public AddFuelToTicketCommandValidator(IPumpState pumpState, IAttendantState attendantState)
        {
            RuleFor(command => command.CardId)
                .NotEmpty()
                .MustAsync(async (instance, propValue, c) =>
                {
                    instance.Attendant = await attendantState.Get(x => x.CardId == propValue);
                    return instance.Attendant != null;
                });

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
        }
    }

    public class AddFuelToTicketCommandHandler(ITicketState ticketState, IMediator mediator) : ICommandHandler<AddFuelToTicketCommand, TicketUpdated>
    {
        public async Task<Result<TicketUpdated>> Handle(AddFuelToTicketCommand command, CancellationToken cancellationToken)
        {
            var openedTicket = await ticketState.Get(x => x.Attendant.CardId == command.CardId && x.Status == TicketStatusEnum.Opened);

            if (openedTicket == null)
            {
                var ticketCreated = await mediator.Send(new CreateTicketForAttendantCommand(command.CardId, command.PumpNumber, command.NozzleNumber));

                if (ticketCreated.IsSuccess)
                    openedTicket = ticketCreated.Value.Ticket;
                else
                    return Result.Fail<TicketUpdated>("Failure creating ticket").WithErrors(ticketCreated.Errors);
            }

            var result = Result.Ok();

            if (openedTicket != null)
            {
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
            }

            return result;
        }
    }
}
