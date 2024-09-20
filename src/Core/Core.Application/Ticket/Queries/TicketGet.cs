

using Core.Domain.Aggregates.Ticket;

namespace Core.Application.Ticket.Queries
{
    public record TicketGetOne : QueryOneBase<TicketAgg>
    {
        public TicketGetOne(string id)
        {
            TicketId = id;
        }

        public string TicketId { get; set; }
    }


    public record TicketGetMany : QueryManyBase
    {
    }
}
