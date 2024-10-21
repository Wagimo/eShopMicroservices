
using BuildingBlock.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckOutBasket;

public record CheckOutBasketCommand ( BasketCheckoutDto BasketCheckOut ) : ICommand<CheckOutBasketResult>;

public record CheckOutBasketResult ( bool IsSuccess );

public class CheckOutBasketValidator : AbstractValidator<CheckOutBasketCommand>
{
    public CheckOutBasketValidator ( )
    {
        RuleFor ( x => x.BasketCheckOut ).NotNull ().WithMessage ( "BasketChecOut Is required!! " );
        RuleFor ( x => x.BasketCheckOut.UserName ).NotNull ().WithMessage ( "UserName Is required!! " );
    }
}

public class CheckOutBasketHandler ( IBasketRepository repository, IPublishEndpoint publishEndpoint ) : ICommandHandler<CheckOutBasketCommand, CheckOutBasketResult>
{
    public async Task<CheckOutBasketResult> Handle ( CheckOutBasketCommand command, CancellationToken cancellationToken )
    {
        var basket = await repository.GetBasket ( command.BasketCheckOut.UserName, cancellationToken );
        if (basket is null)
        {
            return new CheckOutBasketResult ( false );
        }

        var eventMessage = command.BasketCheckOut.Adapt<BasketCheckoutEvent> ();
        eventMessage.TotalPrice = basket.TotalPrice;
        await publishEndpoint.Publish ( eventMessage, cancellationToken );
        await repository.DeleteBasket ( command.BasketCheckOut.UserName, cancellationToken );
        return new CheckOutBasketResult ( true );
    }
}
