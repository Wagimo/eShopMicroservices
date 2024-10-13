
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoryResponse ( IEnumerable<Product> Products );

public class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes ( IEndpointRouteBuilder app )
    {
        app.MapGet ( "/products/category/{category}", async ( string category, ISender sender ) =>
        {
            var categories = await sender.Send ( new GetProductsByCategoryQuery ( category ) );
            var response = categories.Adapt<GetProductsByCategoryResponse> ();
            return Results.Ok ( response );
        } )
             .WithName ( "GetProductsByCategory" )
            .Produces<GetProductsByCategoryResponse> ( StatusCodes.Status200OK )
            .ProducesProblem ( StatusCodes.Status400BadRequest )
            .WithSummary ( "Get All Products" )
            .WithDescription ( "Get All Products filtered By Category" );
    }
}
