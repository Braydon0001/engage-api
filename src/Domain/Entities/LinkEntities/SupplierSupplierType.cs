namespace Engage.Domain.Entities.LinkEntities
{
    public class SupplierSupplierType
    {
        public int SupplierId { get; set; }
        public int SupplierTypeId { get; set; }

        // Navigation Properties
        public Supplier Supplier { get; set; }
        public SupplierType SupplierType { get; set; }
    }
}
