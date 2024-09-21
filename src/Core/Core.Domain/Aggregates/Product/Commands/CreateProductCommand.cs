
using AutoMais.Ticket.Core.Domain.Aggregates.Product.Events;
using MediatR;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Product.Commands
{
    public class CreateProductCommand : IRequest<Result<ProductCreated>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal MaxItemsPerCart { get; set; }
    }
}
