using AutoMais.Ticket.Core.Application.Pump.State;
using AutoMais.Ticket.Core.Application.Ticket.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;


namespace AutoMais.Ticket.Core.Application.Ticket.Commands
{
    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class AddFuelToTicketCommandValidator : AbstractValidator<AddFuelToTicketCommand>
    {
        public AddFuelToTicketCommandValidator(IPumpState pumpState, ITicketState ticketState)
        {
            RuleFor(command => command.TicketId)
                .NotEmpty()
                .MustAsync(async (obj, prop, c) =>
                {
                    obj.Ticket = await ticketState.Get(x => x.Code == prop || x.Id == prop);
                    return obj.Ticket != null;
                });

            RuleFor(command => command.PumpNumber)
                .NotEmpty()
                .MustAsync(async (obj, prop, c) =>
                {
                    obj.Pump = await pumpState.Get(x => x.Number == prop);
                    return obj.Pump != null;
                }).DependentRules(() =>
                {
                    RuleFor(command => command.NozzleNumber)
                        .NotEmpty()
                        .MustAsync(async (instance, property, token) =>
                        {
                            var nozzle = instance?.Pump?.Nozzles?.FirstOrDefault(x => x.Number == property);
                            return nozzle != null;
                        });
                });

            RuleFor(command => command.Quantity)
                .NotEmpty();
        }
    }

    public class AddFuelToTicketCommandHandler(ITicketState ticketState, IPumpState pumpState, IMediator mediator) : IRequestHandler<AddFuelToTicketCommand, Result<TicketUpdated>>
    {
        public async Task<Result<TicketUpdated>> Handle(AddFuelToTicketCommand command, CancellationToken cancellationToken)
        {
            if (command.Pump == null)
                return Result.Fail<TicketUpdated>("Pump not found");

            if (command.Ticket == null)
                return Result.Fail<TicketUpdated>("Ticket not found");

            var nozzle = command.Pump.Nozzles.FirstOrDefault(x => x.Number == command.NozzleNumber);

            if (nozzle == null)
                return Result.Fail<TicketUpdated>("Nozzle not found");

            var updated = command.Ticket.AddOrUpdateSupply(nozzle, command.Quantity);

            if (updated.IsSuccess)
            {
                var saved = await ticketState.Update(command.Ticket.Id, command.Ticket);
                if (saved.IsSuccess)
                    await mediator.Publish(updated.Value);

                updated.WithErrors(saved.Errors);
            }

            return updated;
        }
    }
}
