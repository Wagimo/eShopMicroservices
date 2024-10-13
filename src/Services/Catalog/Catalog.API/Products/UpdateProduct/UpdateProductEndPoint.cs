

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductResponse ( Guid Id );

public class UpdateProductEndPoint : ICarterModule
{
    public void AddRoutes ( IEndpointRouteBuilder app )
    {
        app.MapPut ( "/products/{id}", async ( HttpContext ctx, Guid id, ISender sender ) =>
        {
            // Obtiene el contenido del body de la peticion 
            var request = await HttpRequestRawData.RawData ( ctx.Request.Body );

            var command = new UpdateProductCommand ( request, id );

            var result = await sender.Send ( command );

            var response = result.Adapt<UpdateProductResponse> ();

            return Results.Ok ( response );

        } )
            .WithName ( "UpdateProduct" )
            .Produces<UpdateProductResponse> ( StatusCodes.Status201Created )
            .ProducesProblem ( StatusCodes.Status400BadRequest )
            .WithSummary ( " Update Product" )
            .WithDescription ( "Update a product" );
    }
}
