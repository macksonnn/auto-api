
namespace AutoMais.Ticket.Core.Domain.Aggregates.Pump.Events
{
    public class NozzleCreated : Nozzle, IDomainEvent
    {
        public NozzleCreated(Nozzle nozzle)
        {
            this.Color = nozzle.Color;
            this.Description = nozzle.Description;
            this.Number = nozzle.Number;
        }
    }
}
