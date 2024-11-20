namespace Engage.Application.Services.ProductSubCategories.Queries;

public record ProductSubCategoryHierarchyDto
    (
    int Id, int Level, string[] Hierarchy
    );