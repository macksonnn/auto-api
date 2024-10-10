using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;

namespace AutoMais.Ticket.Core.Application.Attendant.Queries;
public record AttendantGetOne : IQuery<AttendantAgg>
{
    public AttendantGetOne(string id)
    {
        Id = id.ToLower();
    }
    public string Id { get; set; }
}

public record AttendantGetMany : QueryManyBase
{
}
