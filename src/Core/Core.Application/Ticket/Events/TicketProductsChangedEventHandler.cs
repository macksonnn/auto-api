//using AutoMais.Core.Common.Stream;
//using AutoMais.Ticket.Core.Application.Ticket.Adapters.Stream;
//using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;

//namespace AutoMais.Ticket.Core.Application.Ticket.Events
//{
//    internal class TicketProductsChangedEventHandler(IStream stream) : INotificationHandler<TicketProductsChanged>
//    {
//        public Task Handle(TicketProductsChanged notification, CancellationToken cancellationToken)
//        {
//            return stream.SendEventAsync(notification, TicketStream.TicketUpdated);
//        }
//    }
//}
