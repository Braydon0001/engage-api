namespace Engage.Domain.Entities;

public class ApiKey : BaseAuditableEntity
{
    public int ApiKeyId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public string AssignedTo { get; set; }
}