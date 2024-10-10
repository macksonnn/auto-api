using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle;

namespace AutoMais.Ticket.Core.Application.Adapters.Services
{
    public interface IPlateService
    {
        Task<Result<VehicleAgg>> GetPlate(string plate);
    }
}
