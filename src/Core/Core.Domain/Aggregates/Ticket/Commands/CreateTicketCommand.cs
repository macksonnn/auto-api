using Core.Domain.Aggregates.Ticket.Events;

namespace Core.Domain.Aggregates.Ticket.Commands;

/// <summary>
/// Definition of the Command to create a new ticket with the required properties
/// </summary>
/// <param name="AttendantId">The Attendant who is creating the ticket</param>
/// <param name="Plate">The plate number of the vehicle to be associated with the ticket</param>
public record CreateTicketCommand(string AttendantId, string Plate) : MediatR.IRequest<Result<TicketCreated>>;

