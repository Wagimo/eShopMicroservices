
namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest ( int? PageSize = 10, int? PageNumber = 1 );
public record GetProductsResponse ( IEnumerable<Product> Products );

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes ( IEndpointRouteBuilder app )
    {
        app.MapGet ( "/products", async ( [AsParameters] GetProductsRequest request, ISender sender ) =>
        {
            var queryParams = request.Adapt<GetProductsQuery> ();

            var result = await sender.Send ( queryParams );

            var response = result.Adapt<GetProductsResponse> ();

            return Results.Ok ( response );
        } )
            .WithName ( "GetProducts" )
            .Produces<GetProductsResponse> ( StatusCodes.Status200OK )
            .ProducesProblem ( StatusCodes.Status400BadRequest )
            .WithSummary ( "Get All Products" )
            .WithDescription ( "Get All Products Paging" );
    }
}
