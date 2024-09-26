
namespace AutoMais.Ticket.Core.Domain.Aggregates.Pump.Events
{
    public class PumpCreated : PumpAgg, IDomainEvent
    {
        public PumpCreated(PumpAgg pumpAgg)
        {
            this.Number = pumpAgg.Number;
            this.Description = pumpAgg.Description;
            this.SupplierType = pumpAgg.SupplierType;
            this.Id = pumpAgg.Id;
            this.Nozzles = pumpAgg.Nozzles;
        }
    }
}
