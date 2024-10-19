
namespace Ordering.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler ( ILogger<OrderUpdatedEventHandler> logger ) : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle ( OrderUpdatedEvent notification, CancellationToken cancellationToken )
    {
        logger.LogInformation ( "[Domain Event] :: Order {OrderId} {OrderName} is successfully updated", notification.Order.Id, notification.Order.OrderName );
        return Task.CompletedTask;
    }
}
