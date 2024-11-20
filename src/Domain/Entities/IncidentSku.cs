namespace Engage.Domain.Entities;

public class IncidentSku : BaseAuditableEntity
{
    public int IncidentSkuId { get; set; }
    public int IncidentId { get; set; }
    public int IncidentSkuTypeId { get; set; }
    public int IncidentSkuStatusId { get; set; }
    public int DCProductId { get; set; }
    public string Note { get; set; }

    //Navigation Props
    public Incident Incident { get; set; }
    public IncidentSkuType IncidentSkuType { get; set; }
    public IncidentSkuStatus IncidentSkuStatus { get; set; }
    public DCProduct DCProduct { get; set; }
}
