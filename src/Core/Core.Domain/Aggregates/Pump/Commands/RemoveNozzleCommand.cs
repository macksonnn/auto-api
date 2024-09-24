using AutoMais.Ticket.Core.Domain.Aggregates.Pump.Events;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Pump.Commands
{
    public record RemoveNozzleCommand(int PumpNumber, int NozzleNumber) : ICommand<NozzleRemoved>
    {
    }
}
