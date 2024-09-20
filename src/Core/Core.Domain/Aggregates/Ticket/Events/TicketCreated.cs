using System.Text.Json.Serialization;

namespace Core.Domain.Aggregates.Ticket.Events
{
    public class TicketCreated
    {
        public string TicketId { get; private set; }

        public DateTime CreatedDate { get; private set; }

        [JsonIgnore] //Make this property non-serializable
        public TicketAgg Ticket { get; private set; }
        public string Description { get; private set; }

        //TODO: Add the possible payment types

        public static TicketCreated Create(TicketAgg ticket)
        {
            return new TicketCreated
            {
                TicketId = ticket.Id,
                Description = ticket.Description,
                CreatedDate = ticket.CreatedDate,
                Ticket = ticket
            };
        }
    }
}
