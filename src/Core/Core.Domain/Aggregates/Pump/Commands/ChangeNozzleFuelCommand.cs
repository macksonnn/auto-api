using AutoMais.Ticket.Core.Domain.Aggregates.Pump.Events;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Pump.Commands;

public record ChangeNozzleFuelCommand : ICommand<FuelChanged>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public int PumpNumber { get; set; }
    public int NozzleId { get; set; }
}
