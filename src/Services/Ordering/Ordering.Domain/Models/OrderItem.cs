namespace Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    internal OrderItem ( OrderItemId id, OrderId orderId, ProductId productId, int quantity, decimal price )
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public static OrderItem Create ( OrderId orderId, ProductId productId, int quantity, decimal price )
    {
        ArgumentNullException.ThrowIfNull ( orderId );
        ArgumentNullException.ThrowIfNull ( productId );
        if (quantity <= 0)
        {
            throw new DomainException ( "Quantity must be greater than 0" );
        }
        if (price <= 0)
        {
            throw new DomainException ( "Price must be greater than 0" );
        }

        return new OrderItem ( OrderItemId.New (), orderId, productId, quantity, price );
    }

    internal void AddQuantity ( int quantity )
    {
        if (quantity <= 0)
        {
            throw new DomainException ( "Quantity must be greater than 0" );
        }
        Quantity += quantity;
    }
}
