using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Events;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Commands;

#pragma warning disable
public class DisableAttendantCommand : ICommand<AttendantDisabled>
{
    public string AttendantId { get; private set; }

    public DisableAttendantCommand(string attendantId)
    {
        AttendantId = attendantId;
    }
}

public class EnableAttendantCommand : ICommand<AttendantDisabled>
{
    public string AttendantId { get; private set; }

    public EnableAttendantCommand(string attendantId)
    {
        AttendantId = attendantId;
    }
}
