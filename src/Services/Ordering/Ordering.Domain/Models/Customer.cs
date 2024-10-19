namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    internal Customer ( CustomerId id, string name, string email, string phone )
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
    }
    public string Name { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Phone { get; init; } = default!;

    public static Customer Create ( CustomerId id, string name, string email, string phone )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace ( name, nameof ( name ) );
        ArgumentException.ThrowIfNullOrWhiteSpace ( email, nameof ( email ) );
        ArgumentException.ThrowIfNullOrWhiteSpace ( phone, nameof ( phone ) );

        return new Customer ( id, name, email, phone );
    }


}
