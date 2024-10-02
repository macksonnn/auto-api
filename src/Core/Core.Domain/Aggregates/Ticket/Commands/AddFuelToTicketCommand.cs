using AutoMais.Ticket.Core.Domain.Aggregates.Pump;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;

/// <summary>
/// Command to insert a new product in an existing ticket
/// </summary>
/// <param name="TicketId">The ticket unique Id</param>
/// <param name="PumpNumber">The Pump Number</param>
/// <param name="NozzleNumber">The Nozzle Number</param>
/// <param name="Quantity">The quantity of fuel</param>
public record AddFuelToTicketCommand() : ICommand<TicketUpdated>
{
    public string TicketId { get; init; }
    public int PumpNumber { get; init; }
    public int NozzleNumber { get; init; }
    public decimal Quantity { get; init; }

    [JsonIgnore]
    public PumpAgg Pump { get; set; }
    public TicketAgg Ticket { get; set; }
}
