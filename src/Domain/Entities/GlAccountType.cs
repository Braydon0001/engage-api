namespace Engage.Domain.Entities;

public class GLAccountType : BaseAuditableEntity
{
    public int GLAccountTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public GLAccountType()
    {
        GLAccounts = new HashSet<GLAccount>();
    }

    // Navigation Properties
    public ICollection<GLAccount> GLAccounts { get; private set; }
}
