using AutoMais.Ticket.Core.Application.Attendant.Stream;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Events;
using Becape.Core.Common.Stream;

namespace AutoMais.Ticket.Core.Application.Attendant.Events;
internal class AttendantCreatedEventHandler(IStream stream) : INotificationHandler<AttendantCreated>
{
    public Task Handle(AttendantCreated notification, CancellationToken cancellationToken)
     =>   stream.SendEventAsync(notification, AttendantStream.AttendantCreated) ;
}
