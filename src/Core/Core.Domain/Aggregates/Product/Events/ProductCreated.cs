
using System.Text.Json.Serialization;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Product.Events
{
    public record ProductCreated (string ProductId, string Name, decimal Price, decimal Quantity) : IDomainEvent
    {
        [JsonIgnore]
        public ProductAgg Product { get; internal set; }

        public static ProductCreated Create(ProductAgg product)
        {
            return new ProductCreated(product.Id, product.Name, product.Price, product.Quantity);
        }
    }
}
