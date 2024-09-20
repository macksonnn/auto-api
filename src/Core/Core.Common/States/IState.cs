
using FluentResults;

namespace Core.Common.States
{
    public interface IState
    {
    }

    ////TODO: Create the generic interface
    //public interface IState<T>
    //{
    //    Task<Result<T>> SaveAsync(T entity);
    //    Task DeleteAsync(Guid id);
    //    Task<T> GetOneAsync(Guid id);
    //    //Task<IEnumerable<T>> GetManyAsync(FilterDefinition<T> filter);
    //}
}
