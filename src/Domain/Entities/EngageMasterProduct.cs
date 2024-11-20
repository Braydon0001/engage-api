namespace Engage.Domain.Entities
{
    public class EngageMasterProduct : BaseMasterEntity
    {
        public EngageMasterProduct()
        {
            EngageVariantProducts = new HashSet<EngageVariantProduct>();
            EngageProductTags = new HashSet<EngageProductTag>();
            SupplierProducts = new HashSet<SupplierProduct>();
        }
        public int EngageMasterProductId { get; set; }
        public int SupplierId { get; set; }
        public int ProductClassificationId { get; set; }
        public int EngageDepartmentId { get; set; }
        public int EngageBrandId { get; set; }
        public int EngageSubCategoryId { get; set; }
        public int VatId { get; set; }
        public bool IsVATProduct { get; set; }
        public bool IsDairyProduct { get; set; }
        public bool IsAllSuppliersProduct { get; set; }
        public bool IsFreshProduct { get; set; }
        public bool IsDropShipment { get; set; }
        public bool IsCatalogue { get; set; }
        public List<JsonFile> Files { get; set; }

        // Navigation Properties
        public Supplier Supplier { get; set; }
        public ProductClassification ProductClassification { get; set; }
        public EngageDepartment EngageDepartment { get; set; }
        public EngageBrand EngageBrand { get; set; }
        public EngageSubCategory EngageSubCategory { get; set; }
        public Vat Vat { get; set; }
        public ICollection<EngageVariantProduct> EngageVariantProducts { get; set; }
        // Many to Many
        public ICollection<EngageProductTag> EngageProductTags { get; set; }
        public ICollection<SupplierProduct> SupplierProducts { get; set; }

    }
}
