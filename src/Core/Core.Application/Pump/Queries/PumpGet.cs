using AutoMais.Ticket.Core.Application.Pump.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump;

namespace AutoMais.Ticket.Core.Application.Pump.Queries
{
    public record PumpGet(int Number) : IQuery<PumpAgg>;

    public record PumpGetAll() : QueryManyBase<PumpAgg>;



    public class PumpGetQueryHandler(IPumpState state) : 
        IQueryHandler<PumpGet, PumpAgg>,
        IQueryManyHandler<PumpGetAll, PumpAgg>
    {
        public async Task<Result<PumpAgg>> Handle(PumpGet request, CancellationToken cancellationToken)
        {
            //Validate if user can retrieve the desired information
            //Check if the information can be returned to the user...

            return await state.Get(x => x.Number == request.Number);
        }

        public async Task<Result<IEnumerable<PumpAgg>>> Handle(PumpGetAll request, CancellationToken cancellationToken)
        {
            //Validate if user can retrieve the desired information
            //Check if the information can be returned to the user...

            return Result.Ok(await state.GetMany(x => true));
        }
    }
}
