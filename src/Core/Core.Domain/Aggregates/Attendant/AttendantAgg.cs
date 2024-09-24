using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Events;
using System.Reflection.Metadata.Ecma335;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Attendant;
public class AttendantAgg
{
    public string Id { get; internal set; }
    public string RfId { get; internal set; }
    public string Nome { get; internal set; }
    public string CodigoProtheus { get; internal set; }
    public DateTime CreateDate { get; internal set; }

    /// <summary>
    /// Creates a new Attendant
    /// </summary>
    /// <param name="command"></param>
    private AttendantAgg(CreateAttendantCommand command)
    {
        this.RfId = command.RfId;
        this.Nome = command.Nome;
        this.CodigoProtheus = command.CodigoProtheus;
        this.CreateDate = DateTime.Now;
        Id = command.Id;
    }

    public static Result<AttendantCreated> Create(CreateAttendantCommand command)
    {
        Result result = Result.Ok();
        AttendantAgg agg = new AttendantAgg(command);
        return result.ToResult(agg.Created());
    }

    public AttendantCreated Created()
        => new(Id, RfId, Nome, CodigoProtheus) { Attendant = this };
}
