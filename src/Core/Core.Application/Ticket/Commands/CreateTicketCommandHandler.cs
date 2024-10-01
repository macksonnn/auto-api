using AutoMais.Ticket.Core.Application.Attendant.State;
using AutoMais.Ticket.Core.Application.Ticket.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Product;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;
using FluentResults;

namespace AutoMais.Ticket.Core.Application.Ticket.Commands
{
    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketCommandValidator()
        {
            RuleFor(command => command.CardId)
                .NotEmpty()
                .WithMessage("The AttendantId can't be empty.");

            RuleFor(command => command.CardId)
                .MinimumLength(6)
                .NotEqual("Lucas")
                .WithMessage("Text not equal Lucas");
            //TODO: Implement Attendant validator service

            RuleFor(command => command.Plate)
                .NotEmpty()
                .WithMessage("The Plate can't be empty.");
            //TODO: Implement place validator service

        }
    }

    /// <summary>
    /// The command handler is the Application class responsible for connect multiple adapters and run the business logic in the domain
    /// </summary>
    public class CreateTicketCommandHandler(ITicketState ticketState, IAttendantState attendantState, IMediator mediator) : IRequestHandler<CreateTicketCommand, Result<TicketCreated>>
    {
        public async Task<Result<TicketCreated>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var fail = Result.Fail<TicketCreated>("Ticket creation failed");

            var attendantAgg = await attendantState.GetByCard(request.CardId);
            if (attendantAgg == null)
                return fail.WithValidationError("CardId", $"Attendant not found");

            var ticketHasBeenCreated = TicketAgg.Create(request, Domain.Aggregates.Ticket.Attendant.Create(attendantAgg));

            if (ticketHasBeenCreated.IsSuccess)
            {
                var saveResult = await ticketState.Add(ticketHasBeenCreated.Value.Ticket);
                if (saveResult?.Value != null)
                {
                    var ticketCreated = saveResult?.Value?.Created() ?? fail;

                    await mediator.Publish(ticketCreated.Value);

                    return ticketCreated;
                }

                return fail.WithErrors(saveResult?.Errors);
            }

            return ticketHasBeenCreated;
        }
    }

}
