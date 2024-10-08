using AutoMais.Ticket.Core.Application.Ticket.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

namespace AutoMais.Ticket.Core.Application.Ticket.Queries;

public class TicketGetQueryHandler(ITicketState state) : IQueryHandler<TicketGetOne, TicketAgg>
{
    public async Task<Result<TicketAgg>> Handle(TicketGetOne request, CancellationToken cancellationToken)
    {
        //Validate if user can retrieve the desired information
        //Check if the information can be returned to the user...

        return await state.Get(request.Id);
    }
}
