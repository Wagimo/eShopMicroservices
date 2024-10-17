namespace Ordering.Domain.Abstractions;

public interface IAggegate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents ( );
}

public interface IAggegate<T> : IAggegate, IEntity<T>
{
}