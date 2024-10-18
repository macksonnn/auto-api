﻿using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;
using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle;
using System.Text.Json.Serialization;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;

/// <summary>
/// Definition of the Command to create a new ticket with the required properties
/// </summary>
/// <param name="cardId">The Attendant's CardId who is creating the ticket</param>
/// <param name="Plate">The plate number of the vehicle to be associated with the ticket</param>
public record CreateTicketCommand(string CardId, string Plate) : ICommand<TicketCreated>
{
    [JsonIgnore]
    public AttendantAgg Attendant { get; set; }

    [JsonIgnore]
    public VehicleAgg Vehicle { get; set; }
}