namespace Engage.Domain.Entities;

public class Targeting : BaseAuditableEntity
{
    public int TargetingId { get; set; }
    public string Criteria { get; set; }
}
