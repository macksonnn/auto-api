using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Events;
using MediatR;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Commands;

#pragma warning disable
public class CreateAttendantCommand : IRequest<Result<AttendantCreated>>
{
    public string CardId { get;  set; }
    public string Name { get; set; }
}
