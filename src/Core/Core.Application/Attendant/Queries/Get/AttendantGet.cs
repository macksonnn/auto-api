using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;

namespace AutoMais.Ticket.Core.Application.Attendant.Queries;
public record AttendantGetByCard : IQuery<AttendantAgg>
{
    public AttendantGetByCard(string id)
    {
        CardId = id.ToLower();
    }
    public string CardId { get; set; }
}

