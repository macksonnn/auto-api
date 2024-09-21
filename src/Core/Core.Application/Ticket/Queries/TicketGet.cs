using AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

namespace AutoMais.Ticket.Core.Application.Ticket.Queries
{
    public record TicketGetOne : QueryOneBase<TicketAgg>
    {
        public TicketGetOne(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }


    public record TicketGetMany : QueryManyBase
    {
    }
}
