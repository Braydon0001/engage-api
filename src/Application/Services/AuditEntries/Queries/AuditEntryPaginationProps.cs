namespace Engage.Application.Services.AuditEntries.Queries;

public static class AuditEntryPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new() {

            { "id", new PaginationProperty("AuditEntryID") },
            { "entitySetName", new PaginationProperty("EntitySetName") },
            { "entityTypeName", new PaginationProperty("EntityTypeName") },
            { "stateName", new PaginationProperty("StateName") },
            { "createdBy", new PaginationProperty("CreatedBy") },
            { "createdDate", new PaginationProperty("CreatedDate.Date") },
       };
    }
}
