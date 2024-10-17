using AutoMais.Ticket.Core.Application.Attendant.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;

namespace AutoMais.Ticket.Core.Application.Attendant.Queries.Get;

public class AttendantGetQueryHandler(IAttendantState state) : IQueryHandler<AttendantGetOne, AttendantAgg>
{
    public async Task<Result<AttendantAgg>> Handle(AttendantGetOne request, CancellationToken cancellationToken)
    {
        return await state.GetByCard(request.Id);
    }
}


