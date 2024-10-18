using AutoMais.Ticket.Core.Application.Attendant.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;

namespace AutoMais.Ticket.Core.Application.Attendant.Queries;

public class AttendantGetByCardQueryHandler(IAttendantState state) : IQueryHandler<AttendantGetByCard, AttendantAgg>
{
    public async Task<Result<AttendantAgg>> Handle(AttendantGetByCard request, CancellationToken cancellationToken)
    {
        return await state.GetByCard(request.CardId);
    }
}


