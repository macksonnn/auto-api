using System.Text.Json.Serialization;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events
{
    public class TicketUpdated : IDomainEvent
    {
        public string TicketId { get; private set; }
        public string Code { get; private set; }

        public DateTime CreatedDate { get; private set; }
        public DateTime LastUpdatedDate { get; private set; }

        public string Description { get; private set; }
        public decimal ProductsPrice { get; private set; }
        public decimal ProductsQuantity { get; private set; }

        public decimal FuelVolume { get; private set; }
        public decimal FuelPrice { get; private set; }
        public decimal TotalCost { get; private set; }

        public Attendant Attendant { get; private set; }
        public Driver Driver { get; private set; }
        public IEnumerable<Supply> Supplies { get; private set; }
        public IEnumerable<Product> Products { get; private set; }

        [JsonIgnore] //Make this property non-serializable
        public TicketAgg Ticket { get; private set; }

        public static TicketUpdated Create(TicketAgg ticket)
        {
            return new TicketUpdated
            {
                TicketId = ticket.Id,
                Code = ticket.Code,
                Description = ticket.Description,
                CreatedDate = ticket.CreatedDate,
                Ticket = ticket,
                Attendant = ticket.Attendant,
                Driver = ticket.Driver,
                Supplies = ticket.Supplies,
                Products = ticket.Products,
                ProductsPrice = ticket.ProductsTotalPrice,
                ProductsQuantity = ticket.ProductsTotalQuantity,
                FuelVolume = ticket.FuelTotalVolume,
                FuelPrice = ticket.FuelTotalPrice,
                TotalCost = ticket.TotalCost,
                LastUpdatedDate = ticket.LastUpdatedDate
            };
        }
    }
}
