


namespace Basket.API.Basket.CheckOutBasket;

public record CheckOutBasketRequest ( BasketCheckoutDto BasketCheckOut );

public record CheckOutBasketResponse ( bool IsSuccess );
public class CheckOutBasketEndPoint : ICarterModule
{
    public void AddRoutes ( IEndpointRouteBuilder app )
    {
        app.MapPost ( "/basket/checkout", async ( CheckOutBasketRequest request, ISender sender ) =>
        {

            var command = request.Adapt<CheckOutBasketCommand> ();
            var result = await sender.Send ( command );
            var response = result.Adapt<CheckOutBasketResponse> ();
            return Results.Ok ( response );
        } )
            .WithName ( "CheckOutBasket" )
            .Produces<CheckOutBasketResponse> ( StatusCodes.Status200OK )
            .ProducesProblem ( StatusCodes.Status400BadRequest )
            .WithSummary ( "Check out the basket" )
            .WithDescription ( "Check out the basket" );

    }
}
