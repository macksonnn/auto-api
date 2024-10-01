using AutoMais.Ticket.Core.Application.Attendant.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Events;

namespace AutoMais.Ticket.Core.Application.Attendant.Commands;
#pragma warning disable

public sealed class DisableAttendantCommandValidator : AbstractValidator<DisableAttendantCommand>
{
    public DisableAttendantCommandValidator()
    {
        RuleFor(cmd => cmd.AttendantId)
            .NotEmpty()
            .WithMessage("The AttendantId can't be empty");
    }
}

public class DisableAttendantCommandHandler(IAttendantState state, IMediator mediator) : IRequestHandler<DisableAttendantCommand, Result<AttendantDisabled>>
{
    public async Task<Result<AttendantDisabled>> Handle(DisableAttendantCommand request, CancellationToken cancellationToken)
    {
        AttendantAgg? existing = await state.Get(request.AttendantId);
        if (existing != null)
        {
            var result = Result.Ok();
            return result.WithValidationError("AttendantId", "Attendant not found");
        }

        var hasBeenChanged = existing.Disable();

        if (hasBeenChanged.IsSuccess)
        {
            Result<AttendantAgg>? saveResult = await state.Update(hasBeenChanged.Value.Attendant.Id, hasBeenChanged.Value.Attendant);

            if (saveResult.IsSuccess && saveResult?.Value != null)
            {
                await mediator.Publish(hasBeenChanged.Value);
                return hasBeenChanged.Value;
            }
            return Result.Fail<AttendantDisabled>("Attendant update failed!").WithErrors(saveResult?.Errors);
        }
        return hasBeenChanged;
    }
}
