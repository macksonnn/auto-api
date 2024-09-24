using AutoMais.Ticket.Core.Domain.Aggregates.Product;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump.Events;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Pump;

public class PumpAgg : AggRoot
{
    public Guid Id { get; internal set; }
    public int Number { get; internal set; }
    public string Description { get; internal set; }
    public string SupplierType { get; internal set; }
    public IEnumerable<Nozzle> Nozzles { get; internal set; } = new List<Nozzle>();



    public Result<NozzleCreated> AddNozzle(AddNozzleCommand command)
    {
        var result = Result.Ok();

        var nozzles = Nozzles.ToList();

        if (nozzles.Any(x => x.Number == command.Number))
            return result.WithValidationError("Nozzle", $"Nozzle No.{command.Number} already exists");

        var nozzle = Nozzle.Create(command.Number, command.Description, command.Color);
        nozzles.Add(nozzle);

        Nozzles = nozzles;

        return Result.Ok(new NozzleCreated(nozzle));
    }

    public Result<PumpCreated>? Created()
    {
        return Result.Ok(new PumpCreated(this));
    }


    public static Result<PumpAgg> Create(int number, string description, string supplierType)
    {
        var result = Result.Ok();

        var pump = new PumpAgg
        {
            Id = Guid.NewGuid(),
            Number = number,
            Description = description,
            SupplierType = supplierType
        };

        return result.ToResult(pump);
    }

}