namespace BuildingBlock.Messaging.Events;

public record IntegrationEvent
{
    public Guid Id { get; init; }
    public static DateTime OcurredOn => DateTime.UtcNow;
    public string EventType => GetType ().AssemblyQualifiedName!;
}
