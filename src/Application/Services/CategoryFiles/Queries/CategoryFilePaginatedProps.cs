namespace Engage.Application.Services.CategoryFiles.Queries;

public static class CategoryFilePaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new() {

            { "id", new PaginationProperty("CategoryFileId") },
            { "categoryFileTypeId", new PaginationProperty("CategoryFileTypeId", sortProperty: "CategoryFileType.Name") },
            { "storeId", new PaginationProperty("Store.Name") },
            { "categoryGroupId", new PaginationProperty("CategoryGroupId", sortProperty: "CategoryGroup.Name") },
            { "categorySubGroupId", new PaginationProperty("CategorySubGroupId", sortProperty: "CategorySubGroup.Name") },
            { "name", new PaginationProperty("Name") },
            { "note", new PaginationProperty("Note") },
            { "startDate", new PaginationProperty("StartDate.Date") },
            { "endDate", new PaginationProperty("EndDate.Value.Date") }    
 
       };
    }
}