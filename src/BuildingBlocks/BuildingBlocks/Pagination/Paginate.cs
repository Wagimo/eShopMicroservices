
namespace BuildingBlocks.Pagination;

//public static class Paginate
//{
//    public static async Task<PaginationResult<TEntity>> ToPageAsync<TEntity> ( this IQueryable<TEntity> query, int currentPage, int pageSize ) where TEntity : class
//    {
//        if (currentPage < 1 || pageSize < 1)
//            return new PaginationResult<TEntity> ( currentPage, pageSize, 0, [] );

//        var totalItems = await query.CountAsync ();
//        var skip = (currentPage - 1) * pageSize;
//        var data = await query
//                           .Skip ( skip ).Take ( pageSize ).ToListAsync ();
//        return new PaginationResult<TEntity> ( currentPage, pageSize, totalItems, data );
//    }
//}
