namespace Engage.Domain.Entities;

public class SecurityOrganization : BaseAuditableEntity
{
    public int SecurityOrganizationId { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string ExternalId { get; set; }
    public int OwnerId { get; set; }
}