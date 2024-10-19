namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler ( IApplicationDbContext context ) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle ( DeleteOrderCommand command, CancellationToken cancellationToken )
    {
        var orderId = OrderId.Of ( command.Id );

        var order = await context.Orders.FindAsync ( [orderId], cancellationToken: cancellationToken )
                    ?? throw new OrderNotFoundException ( "Order", orderId.Value );
        context.Orders.Remove ( order );
        await context.SaveChangesAsync ( cancellationToken );
        return new DeleteOrderResult ( true );
    }
}
