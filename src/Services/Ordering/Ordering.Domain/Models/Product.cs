
namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    internal Product ( ProductId id, string name, decimal price )
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public string Name { get; init; } = default!;
    public decimal Price { get; init; } = default!;

    public static Product Create ( string name, decimal price )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace ( name, nameof ( name ) );
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero ( price, nameof ( price ) );

        return new Product ( ProductId.New (), name, price );
    }
}
