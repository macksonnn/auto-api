using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;
using System.Text.Json.Serialization;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;



/// <summary>
/// Definition of the Command to create a new ticket with the required properties
/// </summary>
/// <param name="cardId">The Attendant's CardId who is creating the ticket</param>
/// <param name="Plate">The plate number of the vehicle to be associated with the ticket</param>
public record CreateTicketForAttendantCommand(string CardId, int PumpNumber, int NozzleNumber) : ICommand<TicketCreated>
{
    [JsonIgnore]
    public PumpAgg Pump { get; set; }

    [JsonIgnore]
    public AttendantAgg Attendant { get; set; }
}