

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler ( IApplicationDbContext context ) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{

    public async Task<CreateOrderResult> Handle ( CreateOrderCommand command, CancellationToken cancellationToken )
    {
        var order = CreateNewOrder ( command.Order );

        if (order is null) throw new ArgumentNullException ( nameof ( order ) );

        context.Orders.Add ( order );

        await context.SaveChangesAsync ( cancellationToken );

        return new CreateOrderResult ( order.Id.Value );

    }

    private static Order CreateNewOrder ( OrderDto order )
    {
        var shippingAddress = Address.Of (
            order.ShippingAddres.FirstName,
            order.ShippingAddres.LastName,
            order.ShippingAddres.EmailAddress,
            order.ShippingAddres.AdressLine,
            order.ShippingAddres.City,
            order.ShippingAddres.State,
            order.ShippingAddres.Country,
            order.ShippingAddres.ZipCode
            );

        var billingAddress = Address.Of (
           order.BillingAddress.FirstName,
           order.BillingAddress.LastName,
           order.BillingAddress.EmailAddress,
           order.BillingAddress.AdressLine,
           order.BillingAddress.City,
           order.BillingAddress.State,
           order.BillingAddress.Country,
           order.BillingAddress.ZipCode
           );

        var payment = Payment.Of (
           order.Payment.CardName,
           order.Payment.CardNumber,
           order.Payment.Expiration,
           order.Payment.Cvv,
           order.Payment.PaymentMethod );

        var newOrder = Order.Create (
            CustomerId.Of ( order.CustomerId ),
            OrderName.Of ( order.OrderName ),
            shippingAddress,
            billingAddress,
            payment
            );

        foreach (var orderItem in order.OrderItems)
        {
            newOrder.AddOrderItem ( ProductId.Of ( orderItem.ProductId ), orderItem.Quantity, orderItem.Price );
        }

        return newOrder;
    }

}
