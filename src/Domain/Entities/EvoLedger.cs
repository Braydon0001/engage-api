namespace Engage.Domain.Entities;

public class EvoLedger : BaseAuditableEntity
{
    public int EvoLedgerId { get; set; }
    public string LedgerCode { get; set; }
    public string Name { get; set; }
    public int AnalysisPillarSubGroupId { get; set; }
    public bool IsActive { get; set; }

    // Navigation Properties

    public AnalysisPillarSubGroup AnalysisPillarSubGroup { get; set; }
}