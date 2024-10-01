using AutoMais.Ticket.Core.Application.Attendant.State;
using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;

namespace States.Mongo.Repositories.Ticket;
public class AttendantRepository : MongoRepositoryBase<AttendantAgg>, IAttendantState
{
    public AttendantRepository(IMongoDatabase database) : base(database, "Attendants")
    {
    }

    public async Task<AttendantAgg> GetByCard(string cardId)
    {
        var filter = Builders<AttendantAgg>.Filter.Eq("CardId", cardId);
        return await db.Find(filter).FirstOrDefaultAsync();
    }
}
