namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string? EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string City { get; } = default!;
    public string State { get; } = default!;
    public string Country { get; } = default!;
    public string ZipCode { get; } = default!;

    public Address ( )
    {

    }

    public Address ( string firstName, string lastName, string? emailAddress, string addressLine, string city, string state, string country, string zipCode )
    {

        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;

    }


    public static Address Of ( string firstName, string lastName, string? emailAddress, string addressLine, string city, string state, string country, string zipCode )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace ( firstName );
        ArgumentException.ThrowIfNullOrWhiteSpace ( lastName );
        ArgumentException.ThrowIfNullOrWhiteSpace ( addressLine );
        ArgumentException.ThrowIfNullOrWhiteSpace ( city );

        return new Address ( firstName, lastName, emailAddress, addressLine, city, state, country, zipCode );
    }
}
