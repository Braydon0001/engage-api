using Z.EntityFramework.Plus;

namespace Engage.Application.Models;

public class AuditEntityIdEntry : AuditEntry
{
    public int EntityID { get; set; }
}

public class AuditEntityId : Audit
{
    //public List<EntityIdAuditEntry> Entries { get; set; }
    public int EntityID { get; set; }
}
