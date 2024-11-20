namespace Engage.Domain.Entities;

public class AnalysisPillarSubGroup : BaseAuditableEntity
{
    public int AnalysisPillarSubGroupId { get; set; }
    public int AnalysisPillarGroupId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public AnalysisPillarGroup AnalysisPillarGroup { get; set; }
}