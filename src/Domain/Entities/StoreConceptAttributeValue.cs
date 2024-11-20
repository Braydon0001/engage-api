namespace Engage.Domain.Entities;

public class StoreConceptAttributeValue : BaseAuditableEntity
{
    public int StoreConceptAttributeValueId { get; set; }
    public int StoreId { get; set; }
    public int StoreConceptAttributeId { get; set; }
    public string Value { get; set; }
    public int? StoreConceptAttributeOptionId { get; set; }

    //Navigation Properties
    public Store Store { get; set; }
    public StoreConceptAttribute StoreConceptAttribute { get; set; }
    public StoreConceptAttributeOption StoreConceptAttributeOption { get; set; }
}
