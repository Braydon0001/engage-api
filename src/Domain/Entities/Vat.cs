namespace Engage.Domain.Entities;

public class Vat : BaseAuditableEntity
{
    public int VatId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}
