namespace Engage.Domain.Entities;

public class OrderStaging : BaseAuditableEntity
{
    public int OrderStagingId { get; set; }
    public string Region { get; set; }
    public string StoreName { get; set; }
    public string AccountNumber { get; set; }
    public string OrderNumber { get; set; }
    public string OrderContactName { get; set; }
    public string OrderContactEmail { get; set; }
    public string VatNumber { get; set; }
    public string Date { get; set; }
    public string Reference { get; set; }
}