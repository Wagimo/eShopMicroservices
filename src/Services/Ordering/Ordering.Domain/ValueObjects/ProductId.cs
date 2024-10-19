namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    //public static ProductId New ( ) => new ( Guid.NewGuid () );

    public Guid Value { get; }

    private ProductId ( Guid value ) => Value = value;
    public static ProductId Of ( Guid value )
    {
        ArgumentNullException.ThrowIfNull ( value );
        if (value == Guid.Empty)
        {
            throw new DomainException ( "Customer Id cannot be empty" );
        }
        return new ProductId ( value );
    }
}
