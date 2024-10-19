using AutoMais.Ticket.Core.Application.Attendant.State;
using AutoMais.Ticket.Core.Application.Pump.State;
using AutoMais.Ticket.Core.Application.Ticket.Adapters;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;


namespace AutoMais.Ticket.Core.Application.Ticket.Commands
{

    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class FinishRefuelingCommandValidator : AbstractValidator<FinishSupply>
    {
        public FinishRefuelingCommandValidator(IPumpState pumpState)
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
        }
    }

    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class UpdateFuelToTicketCommandValidator : AbstractValidator<UpdateFuelToTicketCommand>
    {
        public UpdateFuelToTicketCommandValidator(IPumpState pumpState)
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

    public class UpdateFuelToTicketCommandHandler(ITicketState ticketState, IMediator mediator) : 
        ICommandHandler<FinishSupply, TicketUpdated>,
        ICommandHandler<UpdateFuelToTicketCommand, TicketUpdated>
    {
        public async Task<Result<TicketUpdated>> Handle(UpdateFuelToTicketCommand command, CancellationToken cancellationToken)
        {
            var openedTicket = await ticketState.GetOpenedTicket(command);

            if (openedTicket.IsFailed)
            {
                return Result.Fail<TicketUpdated>("No ticket Opened or In Progress found");
            }

            var result = Result.Ok();

            var updated = openedTicket.Value.AddOrUpdateSupply(command);

            if (updated.IsSuccess)
            {
                var saved = await ticketState.Update(openedTicket.Value.Id, updated.Value.Ticket);
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

        public async Task<Result<TicketUpdated>> Handle(FinishSupply command, CancellationToken cancellationToken)
        {
            var openedTicket = await ticketState.GetOpenedTicket(command);

            if (openedTicket.IsFailed)
            {
                return Result.Fail<TicketUpdated>("No ticket Opened or In Progress found");
            }

            var result = Result.Ok();

            var updated = openedTicket.Value.FinishSupply(command);

            if (updated.IsSuccess)
            {
                var saved = await ticketState.Update(openedTicket.Value.Id, updated.Value.Ticket);
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
