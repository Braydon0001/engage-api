namespace Engage.Domain.Entities;

public class IncidentStatus : BaseAuditableEntity
{
    public int IncidentStatusId { get; set; }
    public string Name { get; set; }

}
