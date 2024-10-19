

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler ( IApplicationDbContext context ) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle ( UpdateOrderCommand command, CancellationToken cancellationToken )
    {
        var orderId = OrderId.Of ( command.Order.Id );

        var order = await context.Orders.FindAsync ( [orderId], cancellationToken: cancellationToken ) ?? throw new OrderNotFoundException ( "Order", orderId.Value );
        UpdateOrderWithNewValues ( order, command.Order );
        context.Orders.Update ( order );
        await context.SaveChangesAsync ( cancellationToken );
        return new UpdateOrderResult ( true );
    }


    private void UpdateOrderWithNewValues ( Order order, OrderDto orderDto )
    {

        var updatedShippingAddress = Address.Of (
           orderDto.ShippingAddres.FirstName,
           orderDto.ShippingAddres.LastName,
           orderDto.ShippingAddres.EmailAddress,
           orderDto.ShippingAddres.AdressLine,
           orderDto.ShippingAddres.City,
           orderDto.ShippingAddres.State,
           orderDto.ShippingAddres.Country,
           orderDto.ShippingAddres.ZipCode );

        var updatedBillingAddress = Address.Of (
           orderDto.BillingAddress.FirstName,
           orderDto.BillingAddress.LastName,
           orderDto.BillingAddress.EmailAddress,
           orderDto.BillingAddress.AdressLine,
           orderDto.BillingAddress.City,
           orderDto.BillingAddress.State,
           orderDto.BillingAddress.Country,
           orderDto.BillingAddress.ZipCode );

        var updatedPayment = Payment.Of (
           orderDto.Payment.CardName,
           orderDto.Payment.CardNumber,
           orderDto.Payment.Expiration,
           orderDto.Payment.Cvv,
           orderDto.Payment.PaymentMethod );

        order.Update (
            orderName: OrderName.Of ( orderDto.OrderName ),
            shippingAddres: updatedShippingAddress,
            billingAddress: updatedBillingAddress,
            payment: updatedPayment,
            status: orderDto.Status );
    }
}
