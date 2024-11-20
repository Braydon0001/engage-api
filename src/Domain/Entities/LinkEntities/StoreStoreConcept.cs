namespace Engage.Domain.Entities.LinkEntities
{
    public class StoreStoreConcept : BaseAuditableEntity
    {
        public int StoreStoreConceptId { get; set; }
        public int StoreId { get; set; }
        public int StoreConceptId { get; set; }
        public int Level { get; set; }
        public string ImageUrl { get; set; }

        // Navigation Properties
        public Store Store { get; set; }
        public StoreConcept StoreConcept { get; set; }
    }
}
