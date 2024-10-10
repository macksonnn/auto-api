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
                        .MustAsync(async (instance, property, token) =>
                        {
                            return instance?.Pump?.Nozzles?.FirstOrDefault(x => x.Number == property) != null;
                        });
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
            var nozzle = command.Pump.Nozzles.FirstOrDefault(x => x.Number == command.NozzleNumber);

            if (nozzle == null)
                return Result.Fail<TicketCreated>("Nozzle not found");

            var existingTicket = ticketState.Get(x => x.Attendant.CardId == command.CardId && (x.Status == TicketStatusEnum.Opened || x.Status == TicketStatusEnum.InProgress));

            //TODO: Implementar uma lógica para reutilizar um ticket em aberto

            var ticketCreated = TicketAgg.Create(command, Domain.Aggregates.Ticket.Attendant.Create(command.Attendant));

            if (ticketCreated.IsSuccess)
            {
                var saved = await ticketState.Add(ticketCreated.Value.Ticket);
                if (saved.IsSuccess)
                    await mediator.Publish(ticketCreated.Value);

                ticketCreated.WithErrors(saved.Errors);
            }

            return ticketCreated;
        }
    }
}
