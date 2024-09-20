using AutoMais.Core.Common;
using Core.Application.Vehicle.States;
using Core.Domain.Aggregates.Vehicle;
using FluentResults;

namespace States.Mongo.Repositories.Vehicle
{
    internal class VehicleState : IVehicleState
    {
        public Task<Result<VehicleAgg>> AddAsync(VehicleAgg vehicle)
        {
            throw new NotImplementedException();
        }

        public Result<VehicleAgg> GetVehicle(string id)
        {
            throw new NotImplementedException();
        }

        public Result<VehicleAgg> GetVehicle(string id, string attendantId)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<VehicleAgg>> GetVehicles(QueryManyBase queryMany)
        {
            throw new NotImplementedException();
        }
    }
}
