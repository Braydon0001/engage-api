namespace Engage.Application.Services.WebFiles.Queries;

public static class WebFilePaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new() {
            { "id", new ("WebFileId") },
            { "webFileCategoryName", new ("WebFileCategoryId", sortProperty:  "WebFileCategory.Name") },
            { "fileTypeName", new ("FileTypeId", sortProperty: "FileType.Name") },
            { "name", new ("Name") },
            { "displayName", new ("DisplayName") },
            { "startDate", new ("StartDate.Date") },
            { "endDate", new ("EndDate.Value.Date") }
        };
    }
}
