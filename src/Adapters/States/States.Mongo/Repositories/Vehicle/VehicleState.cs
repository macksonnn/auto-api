using AutoMais.Ticket.Core.Application.Vehicle.Adapters.States;
using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle;
using AutoMais.Ticket.States.Mongo;

namespace States.Mongo.Repositories.Vehicle
{
    public class VehicleState : MongoRepositoryBase<VehicleAgg>, IVehicleState
    {
        public VehicleState(IMongoDatabase database) : base(database, "Vehicles")
        {

        }
    }
}
