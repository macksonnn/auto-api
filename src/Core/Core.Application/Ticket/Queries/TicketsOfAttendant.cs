using AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

namespace AutoMais.Ticket.Core.Application.Ticket.Queries
{
    public record TicketsOfAttendant : QueryManyBase<TicketAgg>
    {
        public TicketsOfAttendant(string attendantId)
        {
            CardId = attendantId;
        }

        public string CardId { get; set; }
    }

    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class TicketsOfAttendantValidator : AbstractValidator<TicketsOfAttendant>
    {
        public TicketsOfAttendantValidator()
        {
            RuleFor(command => command.CardId)
                .NotEmpty()
                .WithMessage("The CardId can't be empty.");
        }
    }
}
