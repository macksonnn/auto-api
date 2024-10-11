using AutoMais.Ticket.Core.Application.Vehicle.Adapters.Services;

namespace AutoMais.Ticket.Api.Controllers
{
    public class VehicleController : IEndpointDefinition
    {
        public void RegisterEndpoints(RouteGroupBuilder app)
        {
            var v1 = app.MapGroup("/v1/vehicles").WithTags("Vehicles");

            v1.MapGet("/plate/{plate}", async ([FromRoute] string plate, IPlateService plateService, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await plateService.GetPlate(plate);

                return result;
            });
        }
    }
}
