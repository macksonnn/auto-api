using AutoMais.Ticket.Core.Application.Ticket.Stream;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;
using Becape.Core.Common.Stream;

namespace AutoMais.Ticket.Core.Application.Ticket.Events
{
    internal class TicketProductsChangedEventHandler(IStream stream) : INotificationHandler<TicketProductsChanged>
    {
        public Task Handle(TicketProductsChanged notification, CancellationToken cancellationToken)
        {
            return stream.SendEventAsync(notification, TicketStream.TicketUpdated);
        }
    }
}
