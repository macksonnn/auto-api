

namespace AutoMais.Ticket.Core.Domain.Aggregates.Pump;
public class Nozzle
{
    public int Number { get; internal set; }
    public string Description { get; internal set; }
    public string Color { get; internal set; }




    public static Nozzle Create(int number ,string description, string color)
    {
        return new Nozzle
        {
            Number = number,
            Description = description,
            Color = color
        };
    }
}
