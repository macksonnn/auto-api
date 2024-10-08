using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle.Commands;
using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle.Events;

namespace AutoMais.Ticket.Core.Application.Vehicle
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

    public class CreateVehicleCommandHandler : ICommandHandler<CreateVehicleCommand, VehicleCreated>
    {
        public Task<Result<VehicleCreated>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
