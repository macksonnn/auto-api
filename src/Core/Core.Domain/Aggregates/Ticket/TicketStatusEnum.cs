
namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket
{
    public enum TicketStatusEnum
    {
        Opened = 1,
        InProgress = 2,
        Closed = 3,
        WaitingPayment = 4,
        Paid = 5,
        Abandoned = 6
    }
}
