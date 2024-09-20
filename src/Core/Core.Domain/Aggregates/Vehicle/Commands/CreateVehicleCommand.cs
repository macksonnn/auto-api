using Core.Domain.Aggregates.Vehicle.Events;

namespace Core.Domain.Aggregates.Vehicle.Commands
{
    public record CreateVehicleCommand(string place, string plate, string model, string color, string brand, string type, string ownerId) : MediatR.IRequest<Result<VehicleCreated>>;
}
