using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;

namespace AutoMais.Ticket.Core.Application.Attendant.State;
public interface IAttendantState : IState<AttendantAgg>
{
    public Task<AttendantAgg> GetByCard(string cardId);
}
