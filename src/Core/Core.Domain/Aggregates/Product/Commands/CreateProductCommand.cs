using AutoMais.Ticket.Core.Domain.Aggregates.Product.Events;
using MediatR;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Product.Commands
{
    public record CreateProductCommand : IRequest<Result<ProductCreated>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal MaxItemsPerCart { get; set; }
    }

    public record UpdateProductCommand : IRequest<Result<ProductUpdated>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal MaxItemsPerCart { get; set; }
        public bool IsEnabled { get; set; }
    }

    public record DisableProductCommand (string Id) : IRequest<Result<ProductUpdated>> { }
    public record EnableProductCommand(string Id) : IRequest<Result<ProductUpdated>> { }

}
