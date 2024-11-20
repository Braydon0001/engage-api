namespace Engage.Application.Services.TrainingFiles.Queries;

public static class TrainingFilePaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new Dictionary<string, PaginationProperty> {
            { "id", new PaginationProperty("TrainingFileId") },
            { "trainingName", new PaginationProperty("Training.Name") },
            { "trainingFileTypeName", new PaginationProperty("TrainingFileTypeId") },
        };
    }
}
