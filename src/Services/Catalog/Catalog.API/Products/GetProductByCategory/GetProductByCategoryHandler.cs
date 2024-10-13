namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoryQuery ( string CategoryName ) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult ( IEnumerable<Product> Products );

internal class GetProductsByCategoryQueryHandler ( IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> logger ) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{

    public async Task<GetProductsByCategoryResult> Handle ( GetProductsByCategoryQuery query, CancellationToken cancellationToken )
    {
        logger.LogInformation ( "Querying products by category: {CategoryName}", query.CategoryName );

        var products = await session.Query<Product> ()
            .Where ( p => p.Category.Contains ( query.CategoryName ) )
            .ToListAsync ( cancellationToken );

        return new GetProductsByCategoryResult ( products );
    }
}
