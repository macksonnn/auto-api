using AutoMais.Ticket.Core.Application.Product.Adapters;
using AutoMais.Ticket.Core.Domain.Aggregates.Product;
using AutoMais.Ticket.States.Mongo;

namespace AutoMais.Ticket.States.Mongo.Repositories.Product;

public class ProductRepository : MongoRepositoryBase<ProductAgg>, IProductState
{
    public ProductRepository(IMongoDatabase database) : base(database, "Products")
    {

    }
}
