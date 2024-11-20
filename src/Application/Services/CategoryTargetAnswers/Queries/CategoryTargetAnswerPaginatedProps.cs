namespace Engage.Application.Services.CategoryTargetAnswers.Queries;

public static class CategoryTargetAnswerPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new() {

            { "id", new PaginationProperty("CategoryTargetAnswerId") },
            { "categoryTargetId", new PaginationProperty("CategoryTargetId") },
            { "target", new PaginationProperty("Target") },
            { "available", new PaginationProperty("Available") },
            { "occupied", new PaginationProperty("Occupied") }    
 
       };
    }
}