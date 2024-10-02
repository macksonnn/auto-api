using AutoMais.Ticket.Core.Domain.Aggregates.Pump.Events;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Pump.Commands
{
    public record CreateNozzleCommand() : ICommand<NozzleCreated>
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int PumpNumber { get; internal set; }

        public void ChangePumpNumber(int pumpNumber)
        {
            PumpNumber = pumpNumber;
        }
    }
}
