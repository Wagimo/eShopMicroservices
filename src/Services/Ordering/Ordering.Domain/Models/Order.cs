

namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new ();

    internal Order ( )
    {

    }

    internal Order ( OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddres, Address billingAddress, Payment payment, OrderStatus staus )
    {
        Id = id;
        CustomerId = customerId;
        OrderName = orderName;
        ShippingAddress = shippingAddres;
        BillingAddress = billingAddress;
        Payment = payment;
        OrderStatus = staus;

    }

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly ();

    public CustomerId CustomerId { get; private set; } = default!;

    public OrderName OrderName { get; private set; } = default!;

    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItems.Sum ( x => x.Price * x.Quantity );
        private set { }
    }

    public static Order Create ( CustomerId customerId, OrderName orderName, Address shippingAddres, Address billingAddress, Payment payment )
    {

        var order = new Order ( OrderId.New (), customerId, orderName, shippingAddres, billingAddress, payment, OrderStatus.Pending );
        order.AddDomainEvent ( new OrderCreatedEvent ( order ) );
        return order;

    }

    public void Update ( OrderName orderName, Address shippingAddres, Address billingAddress, Payment payment, OrderStatus status )
    {
        OrderName = orderName;
        ShippingAddress = shippingAddres;
        BillingAddress = billingAddress;
        Payment = payment;
        OrderStatus = status;

        AddDomainEvent ( new OrderUpdatedEvent ( this ) );
    }

    public void AddOrderItem ( ProductId productId, int quantity, decimal price )
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero ( quantity, nameof ( quantity ) );
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero ( price, nameof ( price ) );
        var orderItem = OrderItem.Create ( Id, productId, quantity, price );

        var existingOrderForProduct = _orderItems.FirstOrDefault ( x => x.ProductId == productId );
        if (existingOrderForProduct != null)
        {
            existingOrderForProduct.AddQuantity ( quantity );
        }
        else
        {
            _orderItems.Add ( orderItem );
        }

    }

    public void RemoveOrderItem ( ProductId productId )
    {
        var orderItem = _orderItems.FirstOrDefault ( x => x.ProductId == productId );
        if (orderItem != null)
        {
            _orderItems.Remove ( orderItem );
        }
    }
}
