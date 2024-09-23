
namespace AutoMais.Ticket.Core.Domain.Attendant
{
    public class AttendantAgg : AggRoot
    {
        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public string CardId { get; internal set; }
        public DateTime CreatedDate { get; internal set; }
        public DateTime? DisabledDate { get; internal set; }
        public bool IsEnabled { get { return DisabledDate == null; } }


        public AttendantAgg Disable()
        {
            DisabledDate = DateTime.Now;
            return this;
        }

        public AttendantAgg ChangeCard(string newCard)
        {
            CardId = newCard;
            return this;
        }

        public static AttendantAgg Create(string name, string cardId)
        {
            return new AttendantAgg() {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                CardId = cardId,
                CreatedDate = DateTime.Now
            };
        }
    }
}
