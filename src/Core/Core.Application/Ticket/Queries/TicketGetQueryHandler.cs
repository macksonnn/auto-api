using Core.Application.Ticket.State;
using Core.Domain.Aggregates.Ticket;

namespace Core.Application.Ticket.Queries;

public class TicketGetQueryHandler(ITicketState state) : IRequestHandler<TicketGetOne, Result<TicketAgg>>
{
    public async Task<Result<TicketAgg>> Handle(TicketGetOne request, CancellationToken cancellationToken)
    {
        //Validate if user can retrieve the desired information
        //Check if the information can be returned to the user...

        return state.GetTicket(request.TicketId);
    }
}
