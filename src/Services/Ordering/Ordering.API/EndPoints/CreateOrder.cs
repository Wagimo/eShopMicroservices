

namespace Ordering.API.EndPoints;

public record CreateOrderRequest ( OrderDto Order );
public record CreateOrderResponse ( Guid OrderId );


public class CreateOrder : ICarterModule
{
    public void AddRoutes ( IEndpointRouteBuilder app )
    {
        app.MapPost ( "/orders", async ( CreateOrderRequest request, ISender sender ) =>
        {

            var command = request.Adapt<CreateOrderCommand> ();

            var result = await sender.Send ( command );

            var response = result.Adapt<CreateOrderResponse> ();

            return Results.Created ( $"/orders/{response.OrderId}", response );

        } )
            .WithName ( "CreateOrder" )
            .Produces<CreateOrderRequest> ( StatusCodes.Status201Created )
            .ProducesProblem ( StatusCodes.Status400BadRequest )
            .WithSummary ( "Creates a new order" )
            .WithDescription ( "Creates a new order" );
    }
}
