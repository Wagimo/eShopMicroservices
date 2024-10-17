namespace Ordering.Domain.Models;

public class Customer : Entity<Guid>
{
    internal Customer ( string name, string email, string phone )
    {
        Name = name;
        Email = email;
        Phone = phone;
    }
    public string Name { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Phone { get; init; } = default!;

}
