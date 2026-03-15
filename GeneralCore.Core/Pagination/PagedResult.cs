namespace GeneralCore.Pagination;

public class PagedResult<T>
{
    public int TotalRows { get; init; }
    public int TotalPages { get; init; }
    public IReadOnlyList<T> Rows { get; init; }

    public PagedResult(int totalRows, int pageSize, IReadOnlyList<T> rows)
    {
        TotalRows = totalRows;
        TotalPages = (int)Math.Ceiling((double)totalRows / pageSize);
        Rows = rows;
    }
}
