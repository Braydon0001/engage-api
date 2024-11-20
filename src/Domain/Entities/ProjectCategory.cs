namespace Engage.Domain.Entities;

public class ProjectCategory : BaseAuditableEntity
{
    public ProjectCategory()
    {
        ProjectCategorySuppliers = new HashSet<ProjectCategorySupplier>();
    }
    public int ProjectCategoryId { get; set; }
    public string Name { get; set; }
    public ICollection<ProjectCategorySupplier> ProjectCategorySuppliers { get; private set; }
}