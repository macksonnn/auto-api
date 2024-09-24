using System.Text.Json.Serialization;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Events;
#pragma warning disable 
public record AttendantCreated(
    string id  ,    
        string rfId,
        string nome,
        string codigoProtheus) : IDomainEvent
{
    [JsonIgnore]
    public AttendantAgg Attendant { get; internal set; }

    public static AttendantCreated Create(AttendantAgg attendantAgg) 
        => new AttendantCreated(
            attendantAgg.Id,
            attendantAgg.RfId,
            attendantAgg.Nome,
            attendantAgg.CodigoProtheus);

}
