namespace Engage.Application.Services.Shared.Models;

public class PaginatedListResult<T> where T : class
{
    public PaginatedListResult(List<T> data, PaginationResult pagination)
    {
        Data = data;

        Pagination = new PaginationResult
        {
            PageNo = pagination.PageNo,
            PageSize = pagination.PageSize,
            PageCount = pagination.PageCount,
            RowCount = pagination.RowCount,
            FirstRowOnPage = pagination.FirstRowOnPage,
            LastRowOnPage = pagination.LastRowOnPage
        };
    }

    public List<T> Data { get; private set; }
    public PaginationResult Pagination { get; private set; }

}
