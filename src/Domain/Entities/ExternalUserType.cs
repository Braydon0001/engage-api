namespace Engage.Domain.Entities;

public class ExternalUserType : BaseAuditableEntity
{
    public int ExternalUserTypeId { get; set; }
    public string Name { get; set; }
}