

namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler ( ILogger<OrderCreatedEventHandler> logger ) : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle ( OrderCreatedEvent notification, CancellationToken cancellationToken )
    {
        logger.LogInformation ( "[Domain Event] :: Order {OrderId} {Ordername} is successfully created", notification.Order.Id, notification.Order.OrderName );
        return Task.CompletedTask;
    }
}
