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
        RuleFor(cmd => cmd.Id)
            .NotEmpty()
            .WithMessage("The AttendantId can't be empty");

        RuleFor(cmd => cmd.RfId)
            .NotEmpty()
            .WithMessage("The RfId can't be empty");

        RuleFor(cmd => cmd.CodigoProtheus)
            .NotEmpty()
            .WithMessage("The Protheus's Code can't be empty");

        RuleFor(cmd => cmd.Nome)
            .NotEmpty()
            .WithMessage("The Protheus's Code can't be empty");

        RuleFor(cmd => cmd.Nome)
        .MinimumLength(6)
        .WithMessage("Minimum length should be {MinimumLength} characters");



    }
}
public class CreateAttendantCommandHandler(IAttendantState state, IMediator mediator) : IRequestHandler<CreateAttendantCommand, Result<AttendantCreated>>
{
    public async Task<Result<AttendantCreated>> Handle(CreateAttendantCommand request, CancellationToken cancellationToken)
    {
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
