namespace Engage.Domain.Entities;

public class ProjectDcProduct : BaseAuditableEntity
{
    public int ProjectDcProductId { get; set; }
    public int ProjectId { get; set; }
    public int DcProductId { get; set; }

    // Navigation Properties

    public Project Project { get; set; }
    public DCProduct DcProduct { get; set; }
}