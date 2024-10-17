using AutoMais.Ticket.Core.Application.Ticket.Adapters;
using AutoMais.Ticket.Core.Application.Vehicle.Queries;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;

namespace AutoMais.Ticket.Core.Application.Ticket.Commands
{
    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class ChangeVehicleCommandValidator : AbstractValidator<ChangeVehicleCommand>
    {
        public ChangeVehicleCommandValidator(ITicketState ticketState, IMediator mediator)
        {
            RuleFor(command => command.TicketId)
                .NotEmpty()
                .MustAsync(async (instance, value, c) =>
                {
                    instance.Ticket = await ticketState.Get(value);
                    return instance.Ticket != null;
                });

            RuleFor(command => command.Plate)
                .NotEmpty()
                .MustAsync(async (instance, value, c) =>
                {
                    var result = await mediator.Send(new VehicleGetByPlate(value));
                    if (result.IsSuccess)
                        instance.Vehicle = result.Value;

                    return instance.Vehicle != null;
                });
        }
    }

    public class ChangeVehicleCommandHandler(ITicketState ticketState, IMediator mediator) : ICommandHandler<ChangeVehicleCommand, TicketUpdated>
    {
        public async Task<Result<TicketUpdated>> Handle(ChangeVehicleCommand command, CancellationToken cancellationToken)
        {
            var changed = command.Ticket.ChangeVehicle(command.Vehicle);

            if (changed.IsSuccess)
            {
                var saved = await ticketState.Update(command.TicketId, changed.Value.Ticket);

                if (saved.IsSuccess)
                    await mediator.Publish(changed.Value);

                changed.WithErrors(saved.Errors);
            }

            return changed;
        }
    }
}
