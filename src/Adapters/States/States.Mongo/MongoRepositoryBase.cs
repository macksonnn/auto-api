
using Becape.Core.Common;

namespace States.Mongo;

public abstract class MongoRepositoryBase<T> : IState<T> where T : class
{
    protected readonly IMongoCollection<T> db;

    protected MongoRepositoryBase(IMongoDatabase database)
    {
        db = database.GetCollection<T>((typeof(T).Name));
    }

    protected MongoRepositoryBase(IMongoDatabase database, string collectionName)
    {
        db = database.GetCollection<T>(collectionName);
    }

    public async Task<Result<T>> Add(T entity)
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

    public async Task<Result> Delete(string id)
    {
        try
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var deleteResult = db.DeleteOneAsync(filter);

            if (deleteResult.IsCompletedSuccessfully)
                return Result.Ok();
            else
                return Result.Fail(deleteResult?.Exception?.Message ?? "Delete failed");
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }

    public async Task<T> Get(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return await db.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Result> Update(string id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);

        var updateResult = await db.ReplaceOneAsync(filter, entity);

        if (updateResult.ModifiedCount > 0)
            return Result.Ok();
        else
            return Result.Fail("Update failed");
    }
}
