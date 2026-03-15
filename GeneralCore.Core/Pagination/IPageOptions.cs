namespace GeneralCore.Pagination;

public interface IPageOptions
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
}
