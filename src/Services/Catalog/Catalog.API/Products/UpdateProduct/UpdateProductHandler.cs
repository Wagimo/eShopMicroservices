

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand ( JObject Product, Guid Id )
    : ICommand<UpdateProductResult>;

public record UpdateProductResult ( Guid Id );

internal class UpdateProductHandler ( IDocumentSession session, ILogger<UpdateProductHandler> logger ) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle ( UpdateProductCommand request, CancellationToken cancellationToken )
    {
        logger.LogInformation ( "Updating product {Id}", request.Id );

        var product = await session.LoadAsync<Product> ( request.Id, cancellationToken ) ?? throw new ProductNotFoundException ( request.Id );

        //Actualiza las propiedades del producto con las propiedades del request
        AtomicModifier.PatchObject ( product, request.Product );

        session.Update ( product );

        await session.SaveChangesAsync ( cancellationToken );

        return new UpdateProductResult ( product.Id );

    }
}
