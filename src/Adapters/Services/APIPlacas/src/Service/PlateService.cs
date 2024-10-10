using AutoMais.Services.Vehicles.APIPlacas.Service.Model;
using AutoMais.Services.Vehicles.APIPlacas.Startup;
using AutoMais.Ticket.Core.Application.Adapters.Services;
using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle;
using FluentResults;
using MediatR;
using System.Text.Json;

namespace AutoMais.Services.Vehicles.APIPlacas.Service
{
    /// <summary>
    /// This class is the implementation of the IStream to connect to the Azure Service Bus.
    /// </summary>
    public class PlateService : IPlateService
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;
        private readonly APIPlacasSettings _settings;
        private readonly IMediator _mediator;

        public PlateService(ILogger logger, IMediator mediator, HttpClient client, APIPlacasSettings settings)
        {
            _client = client;
            _logger = logger;
            _settings = settings;
            _mediator = mediator;
        }

        public async Task<Result<VehicleAgg>> GetPlate(string plate)
        {
            plate = plate.Replace("-", string.Empty).Trim();
            var response = await _client.GetAsync($"consulta/{plate}/{_settings.Token}");

            if (!response.IsSuccessStatusCode)
            {
                return Result.Fail("API Placas failed to respond").WithError(await response.Content.ReadAsStringAsync());
            }

            var result = JsonSerializer.Deserialize<ConsultaPlaca>(await response.Content.ReadAsStringAsync());

            if (result is not null)
            {
                await _mediator.Publish(result);
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
