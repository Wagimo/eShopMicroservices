namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoryQuery ( string CategoryName ) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult ( IEnumerable<Product> Products );

internal class GetProductsByCategoryQueryHandler ( IDocumentSession session ) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{

    public async Task<GetProductsByCategoryResult> Handle ( GetProductsByCategoryQuery query, CancellationToken cancellationToken )
    {

        var products = await session.Query<Product> ()
            .Where ( p => p.Category.Contains ( query.CategoryName ) )
            .ToListAsync ( cancellationToken );

        return new GetProductsByCategoryResult ( products );
    }
}
