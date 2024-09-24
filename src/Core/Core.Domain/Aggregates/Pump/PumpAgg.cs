

namespace AutoMais.Ticket.Core.Domain.Aggregates.Pump;

public class PumpAgg : AggRoot
{
    public Guid Id { get; internal set; }
    public int Number { get; internal set; }
    public string Description { get; internal set; }
    public string SupplierType { get; internal set; }
    public IEnumerable<Nozzle> Nozzles { get; internal set; }
}