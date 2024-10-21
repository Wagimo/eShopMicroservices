using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler (
    IPublishEndpoint publishEndpoint,
    IFeatureManager featureManager,
    ILogger<OrderCreatedEventHandler> logger ) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle ( OrderCreatedEvent domainEvent, CancellationToken cancellationToken )
    {
        logger.LogInformation ( "[Domain Event] :: Order {OrderId} {Ordername} is successfully created", domainEvent.Order.Id, domainEvent.Order.OrderName );
        // Verifica que la bandera de característica esté habilitada. esta bandera se puede habilitar o deshabilitar dinamicamente a conveniencia del negocio. Por ejemplo, para la siembra de datos en la tabla de pedidos, no es necesario lanzar el evento de integracion, por tal motivo ese valor estara deshabilitado o false
        if (await featureManager.IsEnabledAsync ( "OrderFullfilment" ))
        {
            var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto ();

            await publishEndpoint.Publish ( orderCreatedIntegrationEvent, cancellationToken );

        }

    }
}
