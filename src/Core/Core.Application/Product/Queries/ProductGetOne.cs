using AutoMais.Ticket.Core.Domain.Aggregates.Product;

namespace AutoMais.Ticket.Core.Application.Product.Queries
{
    public record ProductGetOne : QueryOneBase<ProductAgg>
    {
        public ProductGetOne(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }


    public record ProductGetMany : QueryManyBase
    {
    }
}
