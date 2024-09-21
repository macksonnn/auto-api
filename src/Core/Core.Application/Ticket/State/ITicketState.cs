using AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

namespace AutoMais.Ticket.Core.Application.Ticket.State
{
    public interface ITicketState : IState<TicketAgg>
    {
        ///// <summary>
        ///// Get one ticket with the specified Id
        ///// </summary>
        ///// <param name="id">The ticket Id</param>
        ///// <returns>An instance of a TicketAgg hydrated</returns>
        //Result<TicketAgg> GetTicket(string id);

        ///// <summary>
        ///// Add a new ticket to the state
        ///// </summary>
        ///// <param name="ticket">The ticket to be added</param>
        ///// <returns>The same instance of the recently added item</returns>
        //Task<Result<TicketAgg>> AddAsync(TicketAgg ticket);

        ///// <summary>
        ///// Get one ticket for the specified Attendant
        ///// </summary>
        ///// <param name="id">The ticket Id</param>
        ///// <param name="attendantId">The attendant id</param>
        ///// <returns>An instance of a TicketAgg hydrated</returns>
        //Result<TicketAgg> GetTicket(string id, string attendantId);

        ///// <summary>
        ///// Get a list of tickets based on the query parameter
        ///// </summary>
        ///// <param name="queryMany">Query object with parameters</param>
        ///// <returns>A list of TicketAgg hydrated</returns>
        //Result<IEnumerable<TicketAgg>> GetTickets(QueryManyBase queryMany);
    }
}
