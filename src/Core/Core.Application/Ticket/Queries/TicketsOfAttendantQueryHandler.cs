using AutoMais.Ticket.Core.Application.Ticket.Adapters;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

namespace AutoMais.Ticket.Core.Application.Ticket.Queries;

public class TicketsOfAttendantQueryHandler(ITicketState state) : IQueryManyHandler<TicketsOfAttendant, TicketAgg>
{
    public async Task<Result<IEnumerable<TicketAgg>>> Handle(TicketsOfAttendant request, CancellationToken cancellationToken)
    {
        var items = await state.GetPagedAsync(x => x.Attendant.CardId == request.CardId, request.Page, request.ItemsPerPage);

        return Result.Ok(items);
    }
}
