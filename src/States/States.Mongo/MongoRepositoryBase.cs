using Core.Common.States;
using FluentResults;
using MongoDB.Driver;

namespace States.Mongo;

public abstract class MongoRepositoryBase<T> : IState where T : class
{
    protected readonly IMongoCollection<T> db;

    protected MongoRepositoryBase(IMongoDatabase database, string collectionName)
    {
        db = database.GetCollection<T>(collectionName);
    }

    public async Task<Result<T>> SaveAsync(T entity)
    {
        try
        {
            await db.InsertOneAsync(entity);
            return Result.Ok(entity);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message).WithError(e.Message);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        await db.DeleteOneAsync(filter);
    }

    public async Task<T> GetOneAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return await db.Find(filter).FirstOrDefaultAsync();
    }

    //TODO: implement query many
    //protected async Task<IEnumerable<T>> GetManyAsync(FilterDefinition<T> filter)
    //{
    //    return await db.Find(filter).ToListAsync();
    //}
}
