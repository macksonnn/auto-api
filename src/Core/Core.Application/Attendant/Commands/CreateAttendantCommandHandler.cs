using AutoMais.Ticket.Core.Application.Attendant.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant.Events;

namespace AutoMais.Ticket.Core.Application.Attendant.Commands;
#pragma warning disable

public sealed class CreateAttendantValidator : AbstractValidator<CreateAttendantCommand>
{
    public CreateAttendantValidator()
    {
        RuleFor(cmd => cmd.CardId)
            .NotEmpty()
            .WithMessage("The Attendant' CardId can't be empty");

        RuleFor(cmd => cmd.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("The Name can't be empty");

        RuleFor(cmd => cmd.Name)
        .MinimumLength(2)
        .WithMessage("Minimum length should be {MinimumLength} characters");

        RuleFor(cmd => cmd.Name)
        .MaximumLength(75)
        .WithMessage("Maximum length should be {MaximumLength} characters");

    }
}
public class CreateAttendantCommandHandler(IAttendantState state, IMediator mediator) : IRequestHandler<CreateAttendantCommand, Result<AttendantCreated>>
{
    public async Task<Result<AttendantCreated>> Handle(CreateAttendantCommand request, CancellationToken cancellationToken)
    {
        AttendantAgg? existing = await state.Get(request.CardId);
        if (existing != null)
        {
            var result = Result.Ok();
            return result.WithError("This CardId is already in use");
        }

        Result<AttendantCreated>? attendantHasBeenCreated = AttendantAgg.Create(request);
        Result<AttendantCreated>? fail = Result.Fail<AttendantCreated>("Attendant creation failed!");

        if (attendantHasBeenCreated.IsSuccess)
        {
            Result<AttendantAgg>? saveResult = await state.Add(attendantHasBeenCreated.Value.Attendant);

            if (saveResult?.Value != null)
            {
                var attendantCreated = saveResult?.Value?.Created() ?? fail;
                mediator.Publish(attendantCreated.Value).ConfigureAwait(false);
                return attendantCreated;
            }
            return fail.WithErrors(saveResult?.Errors);
        }
        return attendantHasBeenCreated;
    }
}
