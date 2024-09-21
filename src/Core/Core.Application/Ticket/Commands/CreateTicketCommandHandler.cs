using Becape.Core.Common.Stream;
using Core.Application.Ticket.State;
using Core.Application.Ticket.Stream;
using Core.Domain.Aggregates.Ticket;
using Core.Domain.Aggregates.Ticket.Commands;
using Core.Domain.Aggregates.Ticket.Events;

namespace Core.Application.Ticket.Commands
{
    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketCommandValidator()
        {
            RuleFor(command => command.AttendantId)
                .NotEmpty()
                .WithMessage("The AttendantId can't be empty.");

            RuleFor(command => command.AttendantId)
                .MinimumLength(6)
                .WithMessage("Minimum length should be {MinimumLength} characters");

            RuleFor(command => command.AttendantId)
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
    public class CreateTicketCommandHandler(IStream stream, ITicketState state) : IRequestHandler<CreateTicketCommand, Result<TicketCreated>>
    {
        public async Task<Result<TicketCreated>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            //Ir no banco
            //chamr serviço de api externo 

            var ticketHasBeenCreated = TicketAgg.Create(request);
            var fail = Result.Fail<TicketCreated>("Ticket creation failed");

            if (ticketHasBeenCreated.IsSuccess)
            {
                var saveResult = await state.AddAsync(ticketHasBeenCreated.Value.Ticket);
                if (saveResult?.Value != null)
                {
                    var ticketCreated = saveResult?.Value?.Created() ?? fail;
                    await stream.SendEventAsync(ticketCreated.Value, TicketStream.TicketCreated);
                    return ticketCreated;
                }

                return fail.WithErrors(saveResult?.Errors);
            }

            return ticketHasBeenCreated;
        }
    }

}
