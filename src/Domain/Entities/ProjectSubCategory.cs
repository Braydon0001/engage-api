namespace Engage.Domain.Entities;

public class ProjectSubCategory : BaseAuditableEntity
{
    public int ProjectSubCategoryId { get; set; }
    public int ProjectCategoryId { get; set; }
    public int? EngageSubGroupId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public ProjectCategory ProjectCategory { get; set; }
    public EngageSubGroup EngageSubGroup { get; set; }
}