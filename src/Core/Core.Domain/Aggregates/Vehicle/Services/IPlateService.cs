
namespace AutoMais.Ticket.Core.Domain.Aggregates.Vehicle.Services
{
    public interface IPlateService
    {
        Task<Result<VehicleAgg>> GetPlate(string plate);
    }
}
