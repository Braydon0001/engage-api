namespace Engage.Application.Pagination;

public class PaginationProperty
{
    public PaginationProperty(string property)
    {
        Property = property;
    }

    public PaginationProperty(string property, string sortProperty)
    {
        Property = property;
        SortProperty = sortProperty;
    }

    public string Property { get; init; }
    public string SortProperty { get; init; }
}
