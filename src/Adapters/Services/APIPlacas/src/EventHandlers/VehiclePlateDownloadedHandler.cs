using AutoMais.Core.Common;
using AutoMais.Services.Vehicles.APIPlacas.Adapters.States;
using AutoMais.Services.Vehicles.APIPlacas.Service.Model;


namespace AutoMais.Services.Vehicles.APIPlacas.EventHandlers
{
    public class VehiclePlateDownloadedHandler(IVehiclePlateState state) : IDomainEventHandler<VehiclePlateDownloaded>
    {
        public async Task Handle(VehiclePlateDownloaded notification, CancellationToken cancellationToken)
        {
            await state.AddOrUpdate(notification.placa, notification);
        }
    }
}
