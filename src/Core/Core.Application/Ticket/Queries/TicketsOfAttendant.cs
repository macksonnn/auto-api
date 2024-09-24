using AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

namespace AutoMais.Ticket.Core.Application.Ticket.Queries
{
    public record TicketsOfAttendant : IQueryMany<TicketAgg>
    {
        public TicketsOfAttendant(string attendantId, int pageSize, int pageNumber)
        {
            AttendantId = attendantId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public string AttendantId { get; set; }
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 20;
    }

    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class TicketsOfAttendantValidator : AbstractValidator<TicketsOfAttendant>
    {
        public TicketsOfAttendantValidator()
        {
            RuleFor(command => command.AttendantId)
                .NotEmpty()
                .WithMessage("The AttendantId can't be empty.");
        }
    }
}
