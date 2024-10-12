

namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest ( string Name, List<string> Category, string Description, string Imagefile, decimal Price );

public record CreateProductResponse ( Guid Id );

public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes ( IEndpointRouteBuilder app )
    {
        app.MapPost ( "/products", async ( CreateProductRequest request, ISender sender ) =>
        {
            var command = request.Adapt<CreateProductCommand> (); //Napster is used to map the request to the command

            var result = await sender.Send ( command );

            var response = result.Adapt<CreateProductResponse> (); //Napster is used to map the result to the response

            return Results.Created ( $"/products/{response.Id}", response );

        } )
            .WithName ( "CreateProduct" )
            .Produces<CreateProductResponse> ( StatusCodes.Status201Created )
            .ProducesProblem ( StatusCodes.Status400BadRequest )
            .WithSummary ( " Crate Product" )
            .WithDescription ( "Create a new product" );
    }
}
