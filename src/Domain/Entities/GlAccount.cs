namespace Engage.Domain.Entities;

public class GLAccount : BaseAuditableEntity
{
    public int GLAccountId { get; set; }
    public int GLAccountTypeId { get; set; }
    public int EngageRegionId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public GLAccount()
    {
        Budgets = new HashSet<Budget>();
    }

    // Navigation Properties
    public GLAccountType GLAccountType { get; set; }
    public EngageRegion EngageRegion { get; set; }

    public ICollection<Budget> Budgets { get; private set; }
}
