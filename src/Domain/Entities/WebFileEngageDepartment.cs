namespace Engage.Domain.Entities;

public class WebFileEngageDepartment : WebFileTarget
{
    public int EngageDepartmentId { get; set; }

    // Navigation Properties

    public EngageDepartment EngageDepartment { get; set; }
}
