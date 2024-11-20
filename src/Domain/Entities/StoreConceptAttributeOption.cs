namespace Engage.Domain.Entities;

public class StoreConceptAttributeOption : BaseAuditableEntity
{
    public int StoreConceptAttributeOptionId { get; set; }
    public int StoreConceptAttributeId { get; set; }
    public string Option { get; set; }

    // Navigation Properties 
    public StoreConceptAttribute StoreConceptAttribute { get; set; }
}
