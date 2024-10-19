namespace Ordering.Domain.ValueObjects;

public record OrderName
{

    private const int DefaultLength = 5;
    public string Value { get; }

    private OrderName ( string value ) => Value = value;

    public static OrderName Of ( string value )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace ( value );
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual ( value.Length, DefaultLength, "Order Name must be 5 characters" );
        return new OrderName ( value );
    }
};
