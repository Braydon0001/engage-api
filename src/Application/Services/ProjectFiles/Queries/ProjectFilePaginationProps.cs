namespace Engage.Application.Services.ProjectFiles.Queries;

public static class ProjectFilePaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new Dictionary<string, PaginationProperty> {
            { "id", new PaginationProperty("ProjectFileId") },
            { "projectName", new PaginationProperty("Project.Name") },
            { "projectFileTypeName", new PaginationProperty("ProjectFileTypeId") },
        };
    }
}
