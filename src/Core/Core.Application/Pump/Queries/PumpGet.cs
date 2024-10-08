using AutoMais.Ticket.Core.Application.Pump.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump;

namespace AutoMais.Ticket.Core.Application.Pump.Queries
{
    public record PumpGet : IQuery<PumpAgg>
    {
        public PumpGet(int number)
        {
            Number = number;
        }

        public int Number { get; set; }
    }

    public class PumpGetQueryHandler(IPumpState state) : IQueryHandler<PumpGet, PumpAgg>
    {
        public async Task<Result<PumpAgg>> Handle(PumpGet request, CancellationToken cancellationToken)
        {
            //Validate if user can retrieve the desired information
            //Check if the information can be returned to the user...

            return await state.Get(x => x.Number == request.Number);
        }
    }
}
