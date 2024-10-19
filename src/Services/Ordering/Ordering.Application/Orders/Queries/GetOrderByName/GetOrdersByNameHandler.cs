

namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrdersByNameHandler ( IApplicationDbContext contextDb ) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
{
    public async Task<GetOrderByNameResult> Handle ( GetOrderByNameQuery query, CancellationToken cancellationToken )
    {
        var orders = await contextDb.Orders
            .Include ( o => o.OrderItems )
            .AsNoTracking ()
            .Where ( o => o.OrderName.Value.Contains ( query.Name ) )
            .OrderBy ( o => o.OrderName )
            .ToListAsync ( cancellationToken );

        return new GetOrderByNameResult ( orders.ToOrderDtoList () );
    }
}
