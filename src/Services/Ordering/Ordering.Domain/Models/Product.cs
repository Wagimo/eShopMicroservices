namespace Ordering.Domain.Models;

public class Product : Entity<Guid>
{
    internal Product ( string name, decimal price )
    {
        Name = name;
        Price = price;
    }
    public string Name { get; init; } = default!;
    public decimal Price { get; init; } = default!;
}
