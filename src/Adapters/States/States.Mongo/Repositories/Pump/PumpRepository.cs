using AutoMais.Ticket.Core.Application.Pump.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump;

namespace AutoMais.Ticket.States.Mongo.Repositories.Pump
{
    public class PumpRepository : MongoRepositoryBase<PumpAgg>, IPumpState
    {
        public PumpRepository(IMongoDatabase database) : base(database, "Pumps")
        {

        }
    }
}
