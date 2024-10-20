

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler ( IApplicationDbContext context ) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle ( GetOrdersQuery query, CancellationToken cancellationToken )
    {

        var currentPage = query.Pagination.PageNumber ?? 0;
        var pageSize = query.Pagination.PageSize ?? 10;
        var total = await context.Orders.LongCountAsync ( cancellationToken: cancellationToken );

        var ordersPagination = await context.Orders
            .Include ( o => o.OrderItems )
            .OrderByDescending ( o => o.CreatedAt )
            .AsNoTracking ()
            //.ToPageAsync ( currentPage, pageSize );
            .Skip ( pageSize * currentPage )
            .Take ( pageSize )
            .ToListAsync ( cancellationToken: cancellationToken );

        return new GetOrdersResult ( new PaginationResult<OrderDto> ( currentPage, pageSize, total, ordersPagination.ToOrderDtoList () ) );
        //var pagerDto = ordersPagination.Adapt<PaginationResult<OrderDto>> ();
        //return new GetOrdersResult ( pagerDto );
    }
}
