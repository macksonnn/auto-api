using AutoMais.Ticket.Core.Application.Adapters.Services;

namespace AutoMais.Ticket.Api.Controllers
{
    public class VehicleController : IEndpointDefinition
    {
        //TODO: Find a way to register a global endpoint filter to manipulare the result
        //TODO: view https://khalidabuhakmeh.com/global-endpoint-filters-with-aspnet-core-minimal-apis
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
