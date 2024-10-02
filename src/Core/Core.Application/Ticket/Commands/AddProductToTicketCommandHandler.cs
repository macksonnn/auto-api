using AutoMais.Ticket.Core.Application.Ticket.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Ticket.Events;


namespace AutoMais.Ticket.Core.Application.Ticket.Commands
{
    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class AddProductToTicketCommandValidator : AbstractValidator<AddProductToTicketCommand>
    {
        public AddProductToTicketCommandValidator(IProductState productState)
        {
            RuleFor(command => command.TicketId)
                .NotEmpty()
                .WithMessage("The TicketId can't be empty.");

            RuleFor(command => command.ProductId)
                .NotEmpty()
                .WithMessage("The ProductId can't be empty.")
                .MinimumLength(6)
                .WithMessage("Minimum length should be 6 characters");
                //.MustAsync(async (item, value, token) => {

                //    var product = await productState.Get(value);

                //    return product is not null;
                //})
                //.WithMessage("Product not found");

            RuleFor(command => command.Quantity)
                .GreaterThan(0);
        }
    }

    public class AddProductToTicketCommandHandler (ITicketState ticketState, IProductState productState, IMediator mediator) : IRequestHandler<AddProductToTicketCommand, Result<TicketProductsChanged>>
    {
        public async Task<Result<TicketProductsChanged>> Handle(AddProductToTicketCommand command, CancellationToken cancellationToken)
        {
            var result = Result.Ok();
            var ticket = await ticketState.Get(command.TicketId);

            if (ticket == null)
                return result.WithError("Ticket not found");

            var product = await productState.Get(command.ProductId);

            if (product == null)
                return result.WithError("Product not found");

            var addResult = ticket.AddProduct(command, product);

            if (addResult.IsSuccess)
            {
                var update = await ticketState.Update(command.TicketId, ticket);
                if (update.IsSuccess)
                    await mediator.Publish(addResult.Value, cancellationToken);
            }

            addResult.WithErrors(result.Errors);

            return addResult;
        }
    }
}
