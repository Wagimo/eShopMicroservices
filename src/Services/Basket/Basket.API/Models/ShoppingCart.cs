namespace Basket.API.Models;

public class ShoppingCart
{
    public ShoppingCart ( )
    {
    }

    public ShoppingCart ( string userName )
    {
        UserName = userName;
    }

    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice
    {
        get
        {
            return Items.Sum ( i => i.Price * i.Quantity );
        }
    }

}
