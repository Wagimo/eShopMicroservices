
namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public class GetOrderByCustomerHandler ( IApplicationDbContext contextDb ) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle ( GetOrdersByCustomerQuery query, CancellationToken cancellationToken )
    {

        var order = await contextDb.Orders
        .Include ( o => o.OrderItems )
        .AsNoTracking ()
            .Where ( o => o.CustomerId == CustomerId.Of ( query.CustomerId ) )
            .OrderBy ( o => o.OrderName.Value )
            .ToListAsync ( cancellationToken );


        return new GetOrdersByCustomerResult ( order.ToOrderDtoList () );
    }
}
