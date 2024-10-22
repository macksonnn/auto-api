using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;

namespace AutoMais.Ticket.Core.Application.Attendant.Queries;
public record AttendantGetAll : QueryManyBase<AttendantAgg>
{
    public AttendantGetAll()
    {
        
    }
 
}


