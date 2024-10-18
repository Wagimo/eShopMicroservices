namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public string PaymentMethod { get; } = default!;

    protected Payment ( )
    {

    }

    protected Payment ( string cardName, string cardNumber, string expiration, string cvv, string paymentMethod )
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }


    public static Payment Of ( string cardName, string cardNumber, string expiration, string cvv, string paymentMethod )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace ( cardName );
        ArgumentException.ThrowIfNullOrWhiteSpace ( cardNumber );
        ArgumentException.ThrowIfNullOrWhiteSpace ( expiration );
        ArgumentException.ThrowIfNullOrWhiteSpace ( cvv );
        ArgumentOutOfRangeException.ThrowIfGreaterThan ( cvv.Length, 3, "CVV must be 3 characters" );

        return new Payment ( cardName, cardNumber, expiration, cvv, paymentMethod );
    }
}
