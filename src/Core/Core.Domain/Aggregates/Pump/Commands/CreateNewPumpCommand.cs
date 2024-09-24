using AutoMais.Ticket.Core.Domain.Aggregates.Pump.Events;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Pump.Commands;

public record CreateNewPumpCommand : ICommand<PumpCreated>
{
    public int Number { get; init; }
    public string Description { get; init; }
    public string SupplierType { get; init; }

}
