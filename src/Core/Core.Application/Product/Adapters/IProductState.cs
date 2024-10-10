using AutoMais.Ticket.Core.Domain.Aggregates.Product;

namespace AutoMais.Ticket.Core.Application.Product.Adapters
{
    public interface IProductState : IState<ProductAgg>;
}
