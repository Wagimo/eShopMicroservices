﻿

namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<T> : Entity<T>, IAggegate<T>
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly ();

    protected void AddDomainEvent ( IDomainEvent domainEvent )
    {
        _domainEvents.Add ( domainEvent );
    }

    public IDomainEvent[] ClearDomainEvents ( )
    {
        IDomainEvent[] dequeuedEvents = [.. _domainEvents];
        _domainEvents.Clear ();
        return dequeuedEvents;
    }
}
