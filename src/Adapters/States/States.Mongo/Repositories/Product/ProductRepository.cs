using AutoMais.Ticket.Core.Application.Product.Adapters;
using AutoMais.Ticket.Core.Domain.Aggregates.Product;

namespace States.Mongo.Repositories.Ticket;

public class ProductRepository : MongoRepositoryBase<ProductAgg>, IProductState
{
    public ProductRepository(IMongoDatabase database) : base(database, "Products")
    {

    }
}
