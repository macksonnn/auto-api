using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Events;
using MediatR;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Commands;

#pragma warning disable
public class CreateAttendantCommand : IRequest<Result<AttendantCreated>>
{
    public string Id { get;  set; }
    public string RfId { get;  set; }
    public string Nome { get; set; }
    public string CodigoProtheus { get; set; }
    public DateTime CreateDate { get; set; }
}
