

namespace AutoMais.Ticket.Core.Domain.Aggregates.Pump;
public class Nozzle
{
    public Guid Id { get; internal set; }
    public string Description { get; internal set; }
    public string Supplier { get; internal set; }
    public string Color { get; internal set; }
}
