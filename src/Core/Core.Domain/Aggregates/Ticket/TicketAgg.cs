using AutoMais.Ticket.Core.Domain.Aggregates.Product;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

public class TicketAgg : AggRoot
{
    public string Id { get; internal set; }
    public string Description { get; internal set; }
    public DateTime CreatedDate { get; internal set; }
    public DateTime? AbandonedDate { get; internal set; }
    public DateTime? ClosedDate { get; internal set; }
    public IEnumerable<Product> Products { get; internal set; } = new List<Product>();

    public decimal TotalItems { 
        get
        {
            return Products.Sum(p => p.Quantity);
        } 
    }

    public decimal TotalPrice
    {
        get
        {
            return Products.Sum(p => p.Total);
        }
    }

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


    public Result<TicketProductsChanged> AddProduct(AddProductToTicketCommand command, ProductAgg productAgg)
    {
        var result = Result.Ok();

        if (productAgg == null)
        {
            result.WithError("Product cannot be null or invalid");
            return result;
        }

        var productList = Products.ToList();

        var existing = productList.FirstOrDefault(x => x.Id == command.ProductId);
        var productQuantity = (existing?.Quantity ?? 0) + command.Quantity;

        if (productAgg.MaxItemsPerCart > 0 && productQuantity > productAgg.MaxItemsPerCart)
            result.WithValidationError("Quantity", $"Ticket exceeds maximum quantity of {productAgg.MaxItemsPerCart} of this product");

        if (existing != null)
            existing.ChangeQuantity(existing.Quantity + command.Quantity);
        else
            productList.Add(Product.Create(command, productAgg));

        Products = productList;

        return result.ToResult(TicketProductsChanged.Create(this));

    }

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



