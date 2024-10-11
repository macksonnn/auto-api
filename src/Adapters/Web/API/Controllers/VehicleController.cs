using AutoMais.Ticket.Core.Application.Vehicle.Adapters.Services;
using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle;
using FluentResults;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

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
            }).WithOpenApi(o => new(o)
            {
                Summary = "Retrieve vehicle details based on the plate.",
                Description = "If the document does not exists in our records, then retrieve from an external service and this operation has extra costs."
            }).Produces<VehicleAgg>();
        }
    }
}
