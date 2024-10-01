using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket
{
    public class Attendant : Entity
    {
        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public string CardId { get; internal set; }

        private Attendant(AttendantAgg attendant)
        {
            Id = attendant.Id;
            Name = attendant.Name;
            CardId = attendant.CardId;
        }

        public static Attendant Create(AttendantAgg attendant)
        {
            return new Attendant(attendant);
        }
    }
}
