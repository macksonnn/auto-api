using AutoMais.Ticket.Core.Application.Ticket.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Product;

namespace States.Mongo.Repositories.Ticket;

public class ProductRepository : MongoRepositoryBase<ProductAgg>, IProductState
{
    public ProductRepository(IMongoDatabase database) : base(database, "Products")
    {

    }
}
