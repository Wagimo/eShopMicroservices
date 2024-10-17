

namespace Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid ();
    public DateTime OcurredOn => DateTime.UtcNow;
    public string EventType => GetType ().AssemblyQualifiedName!;
}
