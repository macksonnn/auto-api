using Core.Domain.Aggregates.Vehicle.Commands;
using Core.Domain.Aggregates.Vehicle.Events;

namespace Core.Application.Vehicle
{

    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(command => command.plate)
                .NotEmpty()
                .WithMessage("The Plate can't be empty.");
        }
    }

    public class CreateVehicleCommandHandler : MediatR.IRequestHandler<CreateVehicleCommand, Result<VehicleCreated>>
    {
        public Task<Result<VehicleCreated>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
