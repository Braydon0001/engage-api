namespace Engage.Domain.Entities.LinkEntities
{
    public class EngageSubGroupSupplier
    {
        public int EngageSubGroupId { get; set; }
        public int SupplierId { get; set; }

        // Navigation Properties
        public EngageSubGroup EngageSubGroup { get; set; }
        public Supplier Supplier { get; set; }
    }
}
