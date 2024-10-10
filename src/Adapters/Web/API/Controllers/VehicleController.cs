using AutoMais.Ticket.Core.Application.Vehicle.Adapters.Services;

namespace AutoMais.Ticket.Api.Controllers
{
    public class VehicleController : IEndpointDefinition
    {
        public void RegisterEndpoints(RouteGroupBuilder app)
        {
            var v1 = app.MapGroup("/api/v1/vehicles");

            v1.MapGet("/plate/{plate}", async ([FromRoute] string plate, IPlateService plateService, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await plateService.GetPlate(plate);

                return result;
            });
        }
    }
}
