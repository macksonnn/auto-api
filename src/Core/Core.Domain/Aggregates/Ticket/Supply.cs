using AutoMais.Ticket.Core.Domain.Aggregates.Pump;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket
{
    public class Supply : Entity
    {
        public DateTime StartedDate { get; private set; }
        public DateTime? FinishedDate { get; private set; }
        public DateTime? LastUpdatedDate { get; private set; }
        public SupplyStatus Status { get; private set; }
        public decimal Cost { get; private set; }
        public decimal Quantity { get; private set; }
        public Nozzle Nozzle { get; private set; }

        public Result ChangeQuantityAndCost(decimal quantity, decimal cost)
        {
            Quantity = quantity;
            Cost = cost;
            Status = SupplyStatus.InProgress;
            return Result.Ok();
        }

        public Result IncreaseQuantity(decimal quantity)
        {
            Quantity += quantity;
            Status = SupplyStatus.InProgress;
            return Result.Ok();
        }

        public Result Finish()
        {
            Status = SupplyStatus.Finished;
            return Result.Ok();
        }

        internal static Supply Create(Nozzle nozzle, decimal quantity, decimal cost)
        {
            return new Supply()
            {
                StartedDate = DateTime.Now,
                Status = SupplyStatus.NotStarted,
                Nozzle = nozzle,
                Quantity = quantity,
                Cost = cost
            };
        }
    }

    public enum SupplyStatus
    {
        NotStarted,
        Started,
        InProgress,
        Finished,
        Aborted
    }
}
