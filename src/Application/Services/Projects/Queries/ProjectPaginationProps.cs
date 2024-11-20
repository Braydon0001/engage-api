namespace Engage.Application.Services.Projects.Queries;

public static class ProjectPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            { "id", new("ProjectId") },
            { "projectTypeName", new("ProjectTypeId", sortProperty: "ProjectType.Name") },
            { "projectSubTypeName", new("ProjectSubTypeId", sortProperty: "ProjectSubType.Name") },
            { "projectCategoryName", new("ProjectCategoryId", sortProperty: "ProjectCategory.Name") },
            { "projectSubCategoryName", new("ProjectSubCategoryId", sortProperty: "ProjectSubCategory.Name") },
            { "storeName", new("Store.Name", sortProperty: "Store.Name") },
            { "projectStatusName", new("ProjectStatusId", sortProperty: "ProjectStatus.Name") },
            { "projectPriorityName", new("ProjectPriorityId", sortProperty: "ProjectPriority.Name") },
            { "ownerName", new("Owner.Fullname", sortProperty: "ProjectPriority.Name") },
            { "startDate", new("startDate") },
            {"endDate", new("endDate") },
            { "name", new("Name") },
        };
    }
}
