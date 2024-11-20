namespace Engage.Domain.Entities;

public class CreditorStatus : BaseAuditableEntity
{
    public int CreditorStatusId { get; set; }
    public string Name { get; set; }
}