namespace Engage.Domain.Entities;

public class CostType : BaseAuditableEntity
{
    public int CostTypeId { get; set; }
    public string Name { get; set; }
}