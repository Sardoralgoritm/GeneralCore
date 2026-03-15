namespace GeneralCore.Pagination;

public class SortFilterPageOptions : IPageOptions
{
    public const string ASC = "ASC";
    public const string DESC = "DESC";

    private string _orderType = ASC;

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    public virtual string? Search { get; set; }
    public virtual string? SortBy { get; set; }

    public virtual string OrderType
    {
        get => _orderType;
        set => _orderType = value?.ToUpper() is ASC or DESC
            ? value.ToUpper()
            : ASC;
    }

    public bool IsDescending => OrderType == DESC;

    public virtual bool HasSort() => !string.IsNullOrEmpty(SortBy);
    public virtual bool HasSearch() => !string.IsNullOrEmpty(Search);
}
