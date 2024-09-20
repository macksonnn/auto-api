using Core.Domain.Aggregates.Ticket.Commands;
using Core.Domain.Aggregates.Ticket.Events;

namespace Core.Domain.Aggregates.Ticket;

public class TicketAgg : AggRoot
{
    public string Id { get; internal set; }
    public string Description { get; internal set; }

    public DateTime CreatedDate { get; internal set; }
    public DateTime? AbandonedDate { get; internal set; }
    public DateTime? ClosedDate { get; internal set; }

    //public IEnumerable<Product> { get; internal set; }

    /// <summary>
    /// Enable create a new Ticket only inside this namespace
    /// </summary>
    internal TicketAgg()
    {

    }

    /// <summary>
    /// Creates a new Aggregate with all business rules to be validated
    /// </summary>
    /// <param name="command"></param>
    private TicketAgg(CreateTicketCommand command)
    {
        Id = Guid.NewGuid().ToString();
        Description = $"Ticket for {command.Plate}";
        CreatedDate = DateTime.Now;
    }

    public static Result<TicketCreated> Create(CreateTicketCommand command)
    {
        var result = Result.Ok();
        //Add additional validations and return failures in Result if needed
        var agg = new TicketAgg(command);

        if (command.Plate == "Lucas")
            result.WithError("Plate cannot be equals Lucas");

        return result.ToResult(agg.Created());
    }

    public TicketCreated Created()
    {
        return TicketCreated.Create(this);
    }

    /// <summary>
    /// Creates a new Aggregate
    /// </summary>
    public static Result<TicketAgg> Create(string id, string description, DateTime createdDate, DateTime? abandonedDate, DateTime? closedDate)
    {
        var ticket = new TicketAgg();
        ticket.Id = id;
        ticket.Description = description;
        ticket.CreatedDate = createdDate;
        ticket.AbandonedDate = abandonedDate;
        ticket.ClosedDate = closedDate;

        return Result.Ok(ticket);
    }


    //public Result<ProductAdded> AddProduct(AddProductToTicket command)
    //{

    //}

    //public Result<ProductRemoved> RemoveProduct(RemoveProductFromTicket command)
    //{

    //}

    //public Result<RefuelingUpdated> UpdateRefueling(UpdateRefueling command)
    //{

    //}

    //public Result<RefuelingUpdated> StopFuelling(UpdateRefueling command)
    //{

    //}
}



