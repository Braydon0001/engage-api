namespace Engage.Domain.Entities;

public class IncidentType : BaseAuditableEntity
{
    public int IncidentTypeId { get; set; }
    public string Name { get; set; }
}
