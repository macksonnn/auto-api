using System.Text.Json.Serialization;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Events;
#pragma warning disable 
public class AttendantDisabled : IDomainEvent
{
    [JsonIgnore]
    public AttendantAgg Attendant { get; private set; }

    public string AttendantId { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? DisableDate { get; set; }
    public string CardId { get; private set; }

    public static AttendantDisabled Create(AttendantAgg attendantAgg)
        => new AttendantDisabled
        {
            AttendantId = attendantAgg.Id,
            CardId = attendantAgg.CardId,
            Name = attendantAgg.Name,
            CreatedDate = attendantAgg.CreatedDate,
            DisableDate = attendantAgg.DisabledDate,
            Attendant = attendantAgg
        };

}

public class AttendantEnabled : IDomainEvent
{
    [JsonIgnore]
    public AttendantAgg Attendant { get; private set; }

    public string AttendantId { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? DisableDate { get; set; }
    public string CardId { get; private set; }

    public static AttendantEnabled Create(AttendantAgg attendantAgg)
        => new AttendantEnabled
        {
            AttendantId = attendantAgg.Id,
            CardId = attendantAgg.CardId,
            Name = attendantAgg.Name,
            CreatedDate = attendantAgg.CreatedDate,
            DisableDate = attendantAgg.DisabledDate,
            Attendant = attendantAgg
        };

}
