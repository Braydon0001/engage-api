namespace Engage.Application.Pagination;

public class PaginatedQuery
{
    public int StartRow { get; set; }
    public int PageSize { get; set; } = 100;
    public Dictionary<string, FilterModel> FilterModel { get; set; }
    public List<SortModel> SortModel { get; set; }
    public List<string> GroupKeys { get; set; }
    public List<RowGroupCol> RowGroupCols { get; set; }
}
