

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery ( PaginationRequest Pagination, string? Search = "" ) : IQuery<GetOrdersResult>;

public record GetOrdersResult ( PaginationResult<OrderDto> Orders );

