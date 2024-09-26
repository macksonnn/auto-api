using AutoMais.Ticket.Core.Application.Pump.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump;
using Becape.Core.Common.Startup;

namespace AutoMais.Ticket.Core.Application.Pump.Queries
{
    public record NozzlesOfPump : IQueryMany<Nozzle>
    {
        public NozzlesOfPump(int number, int pageSize, int pageNumber)
        {
            Number = number;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public int Number { get; set; }
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 20;
    }

    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class NozzelsOfPumpValidator : AbstractValidator<NozzlesOfPump>
    {
        public NozzelsOfPumpValidator()
        {
            RuleFor(command => command.Number)
                .NotEmpty()
                .WithMessage("The Pump Number can't be empty.");
        }
    }

    public class NozzlesOfPumpQueryHandler(IPumpState state) : IRequestHandler<NozzlesOfPump, Result<IEnumerable<Nozzle>>>
    {
        public async Task<Result<IEnumerable<Nozzle>>> Handle(NozzlesOfPump request, CancellationToken cancellationToken)
        {
            var items = await state.Get(x => x.Number == request.Number);

            return Result.Ok(items?.Nozzles);
        }
    }
}
