namespace Engage.Domain.Entities
{
    public class StoreConceptLevel : BaseAuditableEntity
    {
        public int StoreConceptLevelId { get; set; }
        public int StoreId { get; set; }
        public int StoreConceptId { get; set; }
        public int Level { get; set; }
        public List<JsonFile> Files { get; set; }
        public List<JsonStoreConcept> Concepts { get; set; }
        public string BlobUrl { get; set; }
        public string BlobName { get; set; }
        public int Target { get; set; }
        public int Actual { get; set; }
        public double Score { get; set; }

        // Navigation Properties
        public Store Store { get; set; }
        public StoreConcept StoreConcept { get; set; }
    }
}
