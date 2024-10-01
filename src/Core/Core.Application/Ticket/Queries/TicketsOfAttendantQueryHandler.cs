using AutoMais.Ticket.Core.Application.Ticket.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

namespace AutoMais.Ticket.Core.Application.Ticket.Queries;

public class TicketsOfAttendantQueryHandler(ITicketState state) : IRequestHandler<TicketsOfAttendant, Result<IEnumerable<TicketAgg>>>
{
    public async Task<Result<IEnumerable<TicketAgg>>> Handle(TicketsOfAttendant request, CancellationToken cancellationToken)
    {
        var items = await state.GetPagedAsync(x => x.Attendant.CardId == request.CardId, request.PageNumber, request.PageSize);

        return Result.Ok(items);
    }
}
