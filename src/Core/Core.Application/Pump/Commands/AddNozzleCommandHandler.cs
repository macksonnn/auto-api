using AutoMais.Ticket.Core.Application.Pump.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Pump.Events;

namespace AutoMais.Ticket.Core.Application.Pump.Commands
{
    /// <summary>
    /// The command validator contains the rules to ensure the object is valid
    /// </summary>
    public sealed class AddNozzleCommandValidator : AbstractValidator<CreateNozzleCommand>
    {
        public AddNozzleCommandValidator()
        {
            RuleFor(command => command.PumpNumber)
                .NotEmpty();

            RuleFor(command => command.Number)
                .NotEmpty();

            RuleFor(command => command.Color)
                .NotEmpty();

            RuleFor(command => command.Description)
                .NotEmpty();

        }
    }

    public class AddNozzleCommandHandler(IPumpState state, IMediator mediator) : IRequestHandler<CreateNozzleCommand, Result<NozzleCreated>>
    {
        public async Task<Result<NozzleCreated>> Handle(CreateNozzleCommand request, CancellationToken cancellationToken)
        {
            var pump = await state.Get(x => x.Number == request.PumpNumber);

            if (pump == null)
                return Result.Ok().WithValidationError("Number", $"Pump not found");

            var addedResult = pump.AddNozzle(request);

            if (addedResult != null && addedResult.IsSuccess)
            {
                var saveResult = await state.Update(pump.Id.ToString(), pump);
                if (saveResult.IsSuccess)
                {
                    await mediator.Publish(addedResult.Value);

                    return addedResult.Value;
                }
            }

            return addedResult ?? Result.Ok().WithValidationError("Nozzle", $"Error adding nozzle");
        }
    }
}
