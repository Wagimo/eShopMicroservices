namespace Ordering.Domain.ValueObjects;

public record OrderId ( Guid Value )
{
    public static OrderId New ( ) => new ( Guid.NewGuid () );


    public static OrderId Of ( Guid value )
    {
        ArgumentNullException.ThrowIfNull ( value );
        if (value == Guid.Empty)
        {
            throw new DomainException ( "Customer Id cannot be empty" );
        }
        return new OrderId ( value );
    }
}
