

using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;
using MediatR;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands; 

/// <summary>
/// Command to insert a new product in an existing ticket
/// </summary>
/// <param name="ProductId">The product unique identifier</param>
/// <param name="Quantity">The quantity of the product</param>
/// <param name="UnitPrice">The product Unit price</param>
public record AddProductToTicketCommand(string ProductId, decimal Quantity, decimal UnitPrice) : IRequest<Result<TicketProductsChanged>>
{
    public string TicketId { get; private set; }

    public void ChangeTicket(string ticketId)
    {
        TicketId = ticketId;
    }
}
