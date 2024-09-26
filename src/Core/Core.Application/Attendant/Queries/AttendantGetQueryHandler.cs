using AutoMais.Ticket.Core.Application.Attendant.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;

namespace AutoMais.Ticket.Core.Application.Attendant.Queries;
public class AttendantGetQueryHandler(IAttendantState state) : IRequestHandler<AttendantGetOne, Result<AttendantAgg>>
{
    public async Task<Result<AttendantAgg>> Handle(AttendantGetOne request, CancellationToken cancellationToken)
    {
        return await state.Get(request.Id);
    }
}

     
