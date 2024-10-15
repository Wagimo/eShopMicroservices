

namespace Basket.API.Models;

public class ShoppingCartItem
{
    public ShoppingCartItem ( )
    {

    }

    public ShoppingCartItem ( int quantity, string color, decimal price, Guid productId, string productName )
    {
        Quantity = quantity;
        Color = color;
        Price = price;
        ProductId = productId;
        ProductName = productName;

    }

    public int Quantity { get; set; } = default;
    public string Color { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
}
