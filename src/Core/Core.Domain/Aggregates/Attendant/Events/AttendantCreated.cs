﻿using System.Text.Json.Serialization;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Events;
#pragma warning disable 
public class AttendantCreated : IDomainEvent
{
    [JsonIgnore]
    public AttendantAgg Attendant { get; internal set; }

    public string AttendantId { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? DisableDate { get; set; }
    public string CardId { get; private set; }

    public static AttendantCreated Create(AttendantAgg attendantAgg)
        => new AttendantCreated
        {
            AttendantId = attendantAgg.Id,
            CardId = attendantAgg.CardId,
            CreatedDate = attendantAgg.CreatedDate,
            DisableDate = attendantAgg.DisabledDate,
        };

}
