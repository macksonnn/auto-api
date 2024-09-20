
using FluentResults;

namespace AutoMais.Core.Common
{
    /// <summary>
    /// Represents an Aggregate Root
    /// </summary>
    public abstract class AggRoot
    {
        public Guid UID { get; private set; }

        public AggRoot()
        {
            UID = Guid.NewGuid();
        }
    }

    /// <summary>
    /// Represents an Entity
    /// </summary>
    public abstract class Entity
    {
        public Guid UID { get; private set; }

        public Entity()
        {
            UID = Guid.NewGuid();
        }
    }

    public interface IDomainEvent
    {
    }

    public abstract class CommandBase
    {
        public bool IgnoreWarnings { get; } = false;
    }


    public abstract record QueryOneBase<T> : MediatR.IRequest<Result<T>>
    {

    }

    public abstract record QueryManyBase
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public bool MustSort { get; set; }
        public bool MultiSort { get; set; }

        public IEnumerable<string> SortBy { get; set; }
        public IEnumerable<bool> SortDesc { get; set; }
        public IEnumerable<string> GroupBy { get; set; }
        public IEnumerable<string> GroupDesc { get; set; }
        public IEnumerable<Header> Headers { get; set; }

        public class Header
        {
            //Display text
            public string Text { get; set; }
            //Data value field, same as ViewModel
            public string Value { get; set; }
            //Text to be filtered
            public string Filter { get; set; }
        }
    }
}
