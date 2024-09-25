using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMais.Ticket.Core.Application.Attendant.Queries;
public record AttendantGet : QueryOneBase<AttendantAgg>
{
    public AttendantGet(string id)
    {
        Id = id.ToLower();
    }
    public string Id { get; set; }
}

public record AttendantGetMany: QueryManyBase
{ }
