using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle.Events;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Vehicle.Commands
{
    public record CreateVehicleCommand(string plate, string brand, string model, string color, VehicleTypeEnum type, string ownerId) : MediatR.IRequest<Result<VehicleCreated>>;
}
