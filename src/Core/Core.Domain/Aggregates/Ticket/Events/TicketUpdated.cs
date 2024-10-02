using System.Text.Json.Serialization;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events
{
    public class TicketUpdated : IDomainEvent
    {
        public string TicketId { get; private set; }
        public string Code { get; private set; }

        public DateTime CreatedDate { get; private set; }

        [JsonIgnore] //Make this property non-serializable
        public TicketAgg Ticket { get; private set; }
        public string Description { get; private set; }

        public Attendant Attendant { get; private set; }

        //TODO: Add the possible payment types

        public static TicketUpdated Create(TicketAgg ticket)
        {
            return new TicketUpdated
            {
                TicketId = ticket.Id,
                Code = ticket.Code,
                Description = ticket.Description,
                CreatedDate = ticket.CreatedDate,
                Ticket = ticket,
                Attendant = ticket.Attendant
            };
        }
    }
}
