using AutoMais.Ticket.Core.Application.Ticket.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Product;

namespace AutoMais.Ticket.Core.Application.Product.Queries;

public class ProductGetOneQueryHandler(IProductState state) : IRequestHandler<ProductGetOne, Result<ProductAgg>>
{
    public async Task<Result<ProductAgg>> Handle(ProductGetOne request, CancellationToken cancellationToken)
    {
        //Validate if user can retrieve the desired information
        //Check if the information can be returned to the user...

        return await state.Get(request.Id);
    }
}