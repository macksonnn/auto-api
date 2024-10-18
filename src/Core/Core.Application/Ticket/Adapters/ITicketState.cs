using AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

namespace AutoMais.Ticket.Core.Application.Ticket.Adapters
{
    public interface ITicketState : IState<TicketAgg>
    {
        Task<Result<TicketAgg>> GetOpenedTicket(string cardId, int pumpNumber, int nozzleNumber);
    }
}
