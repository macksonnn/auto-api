using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Events;
using MediatR;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Commands;

#pragma warning disable
public class DisableAttendantCommand : IRequest<Result<AttendantDisabled>>
{
    public string AttendantId { get; private set; }

    public DisableAttendantCommand(string attendantId)
    {
        AttendantId = attendantId;
    }
}

public class EnableAttendantCommand : IRequest<Result<AttendantDisabled>>
{
    public string AttendantId { get; private set; }

    public EnableAttendantCommand(string attendantId)
    {
        AttendantId = attendantId;
    }
}
