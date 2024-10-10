using AutoMais.Services.Vehicles.APIPlacas.Adapters.States;
using AutoMais.Services.Vehicles.APIPlacas.Service.Model;

namespace AutoMais.Ticket.States.Mongo.Repositories.Vehicle
{
    public class VehiclePlateState : MongoRepositoryBase<VehiclePlateDownloaded>, IVehiclePlateState
    {
        public VehiclePlateState(IMongoDatabase database) : base(database, "Plates")
        {

        }
    }
}
