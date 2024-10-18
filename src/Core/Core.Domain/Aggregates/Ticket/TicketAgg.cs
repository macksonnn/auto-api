﻿using AutoMais.Ticket.Core.Domain.Aggregates.Product;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;
using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle;
using System.Security.Cryptography;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket;

public class TicketAgg : AggRoot
{
    public string Id { get; internal set; }
    public string Code { get; internal set; }
    public string Description { get; internal set; }
    public TicketStatusEnum Status { get; internal set; } = TicketStatusEnum.Opened;
    public Attendant Attendant { get; internal set; }
    public Driver Driver { get; internal set; }
    public Vehicle Vehicle { get; internal set; }

    public DateTime CreatedDate { get; internal set; }
    public DateTime LastUpdatedDate { get; internal set; }
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
        private set
        {

        }
    }

    public decimal ProductsTotalPrice
    {
        get
        {
            return Products.Sum(p => p.Total);
        }
        private set
        {

        }
    }

    public decimal FuelTotalVolume
    {
        get
        {
            return Supplies.Sum(p => p.Quantity);
        }
        private set
        {

        }
    }

    public decimal FuelTotalPrice
    {
        get
        {
            return Supplies.Sum(p => p.Cost);
        }
        private set
        {

        }
    }

    public decimal TotalCost
    {
        get
        {
            return FuelTotalPrice + ProductsTotalPrice;
        }
        private set
        {

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
        LastUpdatedDate = DateTime.Now;
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
        LastUpdatedDate = DateTime.Now;
        Attendant = attendant;
        Status = TicketStatusEnum.Opened;

        Supplies = new List<Supply>() {
            Supply.Create(command.Pump, command.Nozzle)
        };
    }

    public static Result<TicketAgg> Create(CreateTicketForAttendantCommand command, Attendant attendant)
    {
        var result = Result.Ok();
        //Add additional validations and return failures in Result if needed
        var agg = new TicketAgg(command, attendant);

        return result.ToResult(agg);
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

        this.LastUpdatedDate = DateTime.Now;

        return result.ToResult(TicketProductsChanged.Create(this));
    }

    public Result<TicketUpdated> AddOrUpdateSupply(AddFuelToTicketCommand command)
    {
        return UpdateSupply(command.PumpNumber, command.NozzleNumber, command.Quantity, command.Cost, command.Pump, command.Nozzle);
    }

    public Result<TicketUpdated> AddOrUpdateSupply(UpdateFuelToTicketCommand command)
    {
        return UpdateSupply(command.PumpNumber, command.NozzleNumber, command.Quantity, command.Cost, command.Pump, command.Nozzle);
    }

    private Result<TicketUpdated> UpdateSupply(int pumpNumber, int nozzleNumber, decimal quantity, decimal cost, PumpAgg pump, Nozzle nozzle)
    {
        var list = this.Supplies.ToList();
        var index = list.FindIndex(x => x.Pump.Number == pumpNumber && x.Pump.Nozzle.Number == nozzleNumber);
        if (index >= 0)
        {
            list[index].ChangeQuantityAndCost(quantity, cost);
        }
        else
        {
            list.Add(Supply.Create(pump, nozzle, quantity, cost));
        }

        Supplies = list;
        this.LastUpdatedDate = DateTime.Now;

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
        this.LastUpdatedDate = DateTime.Now;

        return this.Updated();
    }


    public Result<TicketUpdated> ChangeDriver(ChangeTicketDriverCommand command)
    {
        this.Driver = Driver.Create(command.CPF);
        this.LastUpdatedDate = DateTime.Now;

        return this.Updated();
    }

    public Result<TicketUpdated> ChangeVehicle(VehicleAgg vehicle)
    {
        this.Vehicle = Vehicle.Create(vehicle);
        this.LastUpdatedDate = DateTime.Now;

        return this.Updated();
    }

    public Result<TicketUpdated> ChangeProduct(ProductAgg product, decimal quantity)
    {
        var list = this.Products.ToList();
        var index = list.FindIndex(x => x.Id == product.Id);
        if (index < 0)
            return Result.Fail<TicketUpdated>("Product not found").WithValidationError("ProductId", "Ticket does not contain this product");

        list[index].ChangeQuantity(quantity);

        this.Products = list;
        this.LastUpdatedDate = DateTime.Now;

        return this.Updated();
    }
}
