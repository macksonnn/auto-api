using AutoMais.Ticket.Core.Application.Ticket.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

namespace AutoMais.Ticket.Core.Application.Ticket.Queries;

public class TicketsOfAttendantQueryHandler(ITicketState state) : MediatR.IRequestHandler<TicketsOfAttendant, Result<IEnumerable<TicketAgg>>>
{
    public async Task<Result<IEnumerable<TicketAgg>>> Handle(TicketsOfAttendant request, CancellationToken cancellationToken)
    {
        var result = Result.Ok();

        var items = await state.GetPagedAsync(x => x.AttendantId == request.AttendantId, request.PageNumber, request.PageSize);

        return items;
    }
}
