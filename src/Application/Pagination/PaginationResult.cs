namespace Engage.Application.Pagination;

public class PaginationResult
{
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public int RowCount { get; set; }
    public int FirstRowOnPage { get; set; }
    public int LastRowOnPage { get; set; }

    public static PaginationResult Create(int pageNo, int pageSize, int rowCount)
    {
        var doublePageCount = (double)rowCount / pageSize;
        var pageCount = (int)Math.Ceiling(doublePageCount);
        var firstRowOnPage = (pageNo - 1) * pageSize + 1;
        var lastRowOnPage = Math.Min(pageNo * pageSize, rowCount);

        return new PaginationResult()
        {
            PageNo = pageNo,
            PageSize = pageSize,
            PageCount = pageCount,
            RowCount = rowCount,
            FirstRowOnPage = firstRowOnPage,
            LastRowOnPage = lastRowOnPage
        };
    }
}
