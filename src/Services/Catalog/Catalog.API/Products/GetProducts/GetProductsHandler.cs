namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery ( int? PageSize = 10, int? PageNumber = 1 ) : IQuery<GetProductsResult>;
public record GetProductsResult ( IEnumerable<Product> Products );

internal class GetProductsQueryHandler ( IDocumentSession session, ILogger<GetProductsQueryHandler> logger ) : IQueryHandler<GetProductsQuery, GetProductsResult>
{

    public async Task<GetProductsResult> Handle ( GetProductsQuery query, CancellationToken cancellationToken )
    {
        logger.LogInformation ( "GetProductsQueryHandler.Handler called with {@Query}", query );
        var products = await session.Query<Product> ()
              .ToPagedListAsync ( query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken );

        return new GetProductsResult ( products );
    }
}
