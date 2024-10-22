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
    public sealed class CreateTicketForAttendantCommandValidator : AbstractValidator<CreateTicketForAttendantCommand>
    {
        public CreateTicketForAttendantCommandValidator(IPumpState pumpState, IAttendantState attendantState)
        {
            RuleFor(command => command.PumpNumber)
                .NotEmpty()
                .MustAsync(async (instance, prop, c) =>
                {
                    instance.Pump = await pumpState.Get(x => x.Number == prop);
                    return instance.Pump != null;
                }).DependentRules(() =>
                {
                    RuleFor(command => command.NozzleNumber)
                        .NotEmpty()
                        .Must((instance, property, token) =>
                        {
                            instance.Nozzle = instance?.Pump?.Nozzles?.FirstOrDefault(x => x.Number == property);

                            return instance?.Nozzle != null;
                        })
                        .WithMessage("Pump does not contain this Nozzle");
                });

            RuleFor(command => command.CardId)
                .NotEmpty()
                .MustAsync(async (instance, propValue, c) =>
                {
                    instance.Attendant = await attendantState.Get(x => x.CardId == propValue);
                    return instance.Attendant != null;
                });
        }
    }

    public class CreateTicketForAttendantCommandHandler(ITicketState ticketState, IMediator mediator) : ICommandHandler<CreateTicketForAttendantCommand, TicketCreated>
    {
        public async Task<Result<TicketCreated>> Handle(CreateTicketForAttendantCommand command, CancellationToken cancellationToken)
        {
            var ticket = await ticketState.GetOpenedTicket(command.CardId, command.PumpNumber, command.NozzleNumber);

            if (ticket.IsSuccess)
                return ticket.Value.Created();

            if (ticket.IsFailed)
                ticket = TicketAgg.Create(command, Domain.Aggregates.Ticket.Attendant.Create(command.Attendant!));

            if (ticket.IsSuccess)
            {
                var saved = await ticketState.Add(ticket.Value);
                if (saved.IsSuccess)
                {
                    await mediator.Publish(ticket.Value.Created());
                    return ticket.Value.Created();
                }
            }

            return Result.Fail<TicketCreated>("Ticket creation failed").WithErrors(ticket.Errors);
        }
    }
}
