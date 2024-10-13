


namespace Catalog.API.Products.GetProducbyId;

public record GetProductByIdQuery ( Guid Id ) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult ( Product Product );

internal class GetProductByIdHandler ( IDocumentSession session, ILogger<GetProductByIdHandler> logger ) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle ( GetProductByIdQuery query, CancellationToken cancellationToken )
    {
        logger.LogInformation ( "Getting product with id {Id}", query.Id );

        var product = await session.LoadAsync<Product> ( query.Id, cancellationToken );

        return product is null ? throw new ProductNotFoundException ( query.Id ) : new GetProductByIdResult ( product );
    }
}
