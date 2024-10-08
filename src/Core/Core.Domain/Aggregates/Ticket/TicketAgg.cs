using AutoMais.Ticket.Core.Domain.Aggregates.Product;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;
using System.Security.Cryptography;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

public class TicketAgg : AggRoot
{
    public string Id { get; internal set; }
    public string Code { get; internal set; }
    public string Description { get; internal set; }
    public TicketStatusEnum Status { get; internal set; } = TicketStatusEnum.Opened;
    public Attendant Attendant { get; internal set; }

    public DateTime CreatedDate { get; internal set; }
    public DateTime? AbandonedDate { get; internal set; }
    public DateTime? ClosedDate { get; internal set; }

    public IEnumerable<Product> Products { get; internal set; } = new List<Product>();
    public IEnumerable<Supply> Supplies { get; internal set; } = new List<Supply>();

    public decimal ProductsTotalQuantity
    {
        get
        {
            return Products.Sum(p => p.Quantity);
        }
    }

    public decimal ProductsTotalPrice
    {
        get
        {
            return Products.Sum(p => p.Total);
        }
    }

    public decimal FuelTotalVolume
    {
        get
        {
            return Supplies.Sum(p => p.Quantity);
        }
    }

    public decimal FuelTotalPrice
    {
        get
        {
            return Supplies.Sum(p => p.Cost);
        }
    }

    public decimal TotalCost
    {
        get
        {
            return FuelTotalPrice + ProductsTotalPrice;
        }
    }

    /// <summary>
    /// Enable create a new Ticket only inside this namespace
    /// </summary>
    private TicketAgg()
    {

    }

    /// <summary>
    /// Creates a new Aggregate with all business rules to be validated
    /// </summary>
    /// <param name="command"></param>
    private TicketAgg(CreateTicketCommand command, Attendant attendant)
    {
        Id = Guid.NewGuid().ToString();
        Code = RandomNumberGenerator.GetString(
            choices: "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
            length: 6
        );
        Description = $"Ticket for {command.Plate}";
        CreatedDate = DateTime.Now;
        Attendant = attendant;
        Status = TicketStatusEnum.Opened;
    }


    /// <summary>
    /// Creates a new Aggregate with all business rules to be validated
    /// </summary>
    /// <param name="command"></param>
    private TicketAgg(CreateTicketForAttendantCommand command, Attendant attendant)
    {
        Id = Guid.NewGuid().ToString();
        Code = RandomNumberGenerator.GetString(
            choices: "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
            length: 6
        );
        Description = $"Ticket for Card {command.CardId}";
        CreatedDate = DateTime.Now;
        Attendant = attendant;
        Status = TicketStatusEnum.Opened;
    }

    public static Result<TicketCreated> Create(CreateTicketForAttendantCommand command, Attendant attendant)
    {
        var result = Result.Ok();
        //Add additional validations and return failures in Result if needed
        var agg = new TicketAgg(command, attendant);

        return result.ToResult(agg.Created());
    }

    public static Result<TicketCreated> Create(CreateTicketCommand command, Attendant attendant)
    {
        var result = Result.Ok();
        var agg = new TicketAgg(command, attendant);

        return result.ToResult(agg.Created());
    }

    public TicketCreated Created()
    {
        return TicketCreated.Create(this);
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

    public Result<TicketUpdated> AddOrUpdateSupply(Nozzle nozzle, AddFuelToTicketCommand command)
    {
        var list = this.Supplies.ToList();
        var index = list.FindIndex(x => x.Nozzle.Number == nozzle.Number);
        if (index >= 0)
        {
            list[index].ChangeQuantityAndCost(command.Quantity, command.Cost);
        }
        else
        {
            list.Add(Supply.Create(nozzle, command.Quantity, command.Cost));
        }

        Supplies = list;

        return this.Updated();
    }


    private Result<TicketUpdated> Updated()
    {
        return TicketUpdated.Create(this);
    }


    public Result<TicketUpdated> RemoveProduct(RemoveProductFromTicketCommand command)
    {
        var list = this.Products.ToList();
        var index = list.FindIndex(x => x.Id == command.ProductId);
        if (index < 0)
            return Result.Fail<TicketUpdated>("Product not found").WithValidationError("ProductId", "Ticket does not contain this product");

        list.RemoveAt(index);
        this.Products = list;

        return this.Updated();
    }

    //public Result<RefuelingUpdated> UpdateRefueling(UpdateRefueling command)
    //{

    //}

    //public Result<RefuelingUpdated> StopFuelling(UpdateRefueling command)
    //{

    //}
}



