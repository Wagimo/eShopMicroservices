namespace BuildingBlocks.Pagination;

public class PaginationResult<TEntity> ( int currentPage, int pageSize, long count, IEnumerable<TEntity> data ) where TEntity : class
{
    public int CurrentPage { get; set; } = currentPage;
    public int PageSize { get; set; } = pageSize;
    public long Count { get; set; } = count;
    public IEnumerable<TEntity> Data { get; set; } = data;


}
