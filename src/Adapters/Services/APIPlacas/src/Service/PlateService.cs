using AutoMais.Services.Vehicles.APIPlacas.Service.Model;
using AutoMais.Services.Vehicles.APIPlacas.Startup;
using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle;
using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle.Services;
using FluentResults;
using MediatR;
using System.Text.Json;

namespace AutoMais.Services.Vehicles.APIPlacas.Service
{
    /// <summary>
    /// This class is the implementation of the IStream to connect to the Azure Service Bus.
    /// </summary>
    public class PlateService(IMediator mediator, HttpClient client, APIPlacasSettings settings) : IPlateService
    {
        public async Task<Result<VehicleAgg>> GetPlate(string plate)
        {
            plate = plate.Replace("-", string.Empty).Trim();
            var response = await client.GetAsync($"consulta/{plate}/{settings.Token}");

            if (!response.IsSuccessStatusCode)
            {
                return Result.Fail("API Placas failed to respond").WithError(await response.Content.ReadAsStringAsync());
            }

            var result = JsonSerializer.Deserialize<ConsultaPlaca>(await response.Content.ReadAsStringAsync());

            if (result is not null)
            {
                await mediator.Publish(result);
            }

            return VehicleAgg.Create(
                result.placa,
                result.placa_alternativa,
                result.MARCA,
                result.MODELO,
                result.SUBMODELO,
                result.segmento,
                result.extra.tipo_veiculo,
                result.year,
                result.city,
                result.state,
                result.color,
                result.extra.combustivel,
                result.situacao,
                result.logo
            );
        }
    }
}
