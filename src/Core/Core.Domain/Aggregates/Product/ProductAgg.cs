using AutoMais.Ticket.Core.Domain.Aggregates.Product.Events;
using AutoMais.Ticket.Core.Domain.Aggregates.Product.Commands;
using System.Text.Json.Serialization;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Product
{
    public class ProductAgg
    {
        public string Id { get; internal set; }
        public string Description { get; internal set; }
        public string Name { get; internal set; }
        public decimal Price { get; internal set; }
        public int Quantity { get; internal set; }
        public decimal MaxItemsPerCart { get; internal set; }
        public DateTime CreatedDate { get; internal set; }

        [JsonIgnore]
        public ProductAgg Product { get; private set; }

        /// <summary>
        /// Creates a new Aggregate with all business rules to be validated
        /// </summary>
        /// <param name="command"></param>
        private ProductAgg(CreateProductCommand command)
        {
            Id = Guid.NewGuid().ToString();
            Description = command.Description;
            Name = command.Name;
            Price = command.Price;
            Quantity = command.Quantity;
            MaxItemsPerCart = command.MaxItemsPerCart;
            CreatedDate = DateTime.Now;
        }

        public static Result<ProductCreated> Create(CreateProductCommand command)
        {
            var result = Result.Ok();
            //Add additional validations and return failures in Result if needed
            var agg = new ProductAgg(command);

            //if (command.Plate == "Lucas")
            //    result.WithError("Plate cannot be equals Lucas");

            return result.ToResult(agg.Created());
        }

        public ProductCreated Created()
        {
            return new ProductCreated(Id, Name, Price, Quantity) { Product = this };
        }
    }
}
