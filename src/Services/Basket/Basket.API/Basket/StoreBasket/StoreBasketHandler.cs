




namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand ( ShoppingCart Cart ) : ICommand<StoreBasketResult>;

public record StoreBasketResult ( string UserName );

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator ( )
    {
        RuleFor ( x => x.Cart )
            .NotNull ().WithMessage ( "Cart can not be null" );
        RuleFor ( x => x.Cart.UserName )
            .NotNull ().WithMessage ( "Username is required" );
    }
}
public class StoreBasketCommandHandler ( IBasketRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountProto ) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle ( StoreBasketCommand command, CancellationToken cancellationToken )
    {
        var cart = command.Cart;

        await DeductDiscount ( cart, cancellationToken );

        //TODO: store basket in database (use marten upsert. If exists update, if not insert)
        await repository.StoreBasket ( cart, cancellationToken );
        //TODO: update cache

        return new StoreBasketResult ( cart.UserName );
    }

    private async Task DeductDiscount ( ShoppingCart cart, CancellationToken cancellationToken )
    {
        foreach (var item in cart.Items)
        {
            var cupon = await discountProto.GetDiscountAsync ( new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken );
            item.Price -= (decimal)cupon.Amount;
        }
    }
}
