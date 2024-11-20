namespace Engage.Domain.Entities;

public class Incident : BaseAuditableEntity
{
    public Incident()
    {
        IncidentSkus = new HashSet<IncidentSku>();
    }
    public int IncidentId { get; set; }
    public int ClientTypeId { get; set; }
    public int IncidentTypeId { get; set; }
    public int IncidentStatusId { get; set; }
    public int SupplierId { get; set; }
    public int StoreId { get; set; }
    public string IncidentNumber { get; set; }
    public DateTime IncidentDate { get; set; }
    public string Note { get; set; }

    //Navigation Props
    public ClientType ClientType { get; set; }
    public IncidentType IncidentType { get; set; }
    public IncidentStatus IncidentStatus { get; set; }
    public Supplier Supplier { get; set; }
    public Store Store { get; set; }
    public ICollection<IncidentSku> IncidentSkus { get; set; }
}
