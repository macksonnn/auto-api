//using AutoMais.Core.Common.Stream;
//using AutoMais.Ticket.Core.Application.Ticket.Adapters.Stream;
//using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;

//namespace AutoMais.Ticket.Core.Application.Ticket.Events
//{
//    internal class TicketCreatedEventHandler(IStream stream) : INotificationHandler<TicketCreated>
//    {
//        public Task Handle(TicketCreated notification, CancellationToken cancellationToken)
//        {
//            return stream.SendEventAsync(notification, TicketStream.TicketCreated);
//        }
//    }
//}
