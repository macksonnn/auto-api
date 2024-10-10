using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;
using System.Text.Json.Serialization;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;

/// <summary>
/// Command to insert a new product in an existing ticket
/// </summary>
/// <param name="CardId">The Attendant card unique Id</param>
/// <param name="PumpNumber">The Pump Number</param>
/// <param name="NozzleNumber">The Nozzle Number</param>
/// <param name="Quantity">The quantity of fuel</param>
public record AddFuelToTicketCommand() : ICommand<TicketUpdated>
{
    public required string CardId { get; init; }
    public required int PumpNumber { get; init; }
    public required int NozzleNumber { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal Cost { get; init; }

    [JsonIgnore]
    public PumpAgg? Pump { get; set; }

    [JsonIgnore]
    public AttendantAgg? Attendant { get; set; }
    public Nozzle? Nozzle { get; set; }
}
