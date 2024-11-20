using Engage.Domain.Entities.Settings;

namespace Engage.Domain.Entities
{
    public class Supplier : BaseMasterEntity
    {
        public Supplier()
        {
            Vendors = new HashSet<Vendor>();
            EngageMasterProducts = new HashSet<EngageMasterProduct>();
            SupplierSupplierTypes = new HashSet<SupplierSupplierType>();
            EngageSubGroupSuppliers = new HashSet<EngageSubGroupSupplier>();
            GLAdjustments = new HashSet<GLAdjustment>();
            SupplierClaimAccountManagers = new HashSet<SupplierClaimAccountManager>();
            SupplierClaimClassifications = new HashSet<SupplierClaimClassification>();
            SupplierEngageBrands = new HashSet<SupplierEngageBrand>();
            SupplierProducts = new HashSet<SupplierProduct>();
            SupplierStores = new HashSet<SupplierStore>();
            SupplierRegions = new HashSet<SupplierRegion>();
        }

        public int SupplierId { get; set; }
        public int StakeholderId { get; set; }
        public int SupplierGroupId { get; set; }
        public int? PrimaryLocationId { get; set; }
        public int? PrimaryContactId { get; set; }
        public string ShortName { get; set; }
        public string VATNumber { get; set; }
        public List<StringSetting> StringSettings { get; set; }
        public List<NumberSetting> NumberSettings { get; set; }
        public List<BooleanSetting> BooleanSettings { get; set; }
        public List<JsonFile> Files { get; set; }
        public List<JsonSetting> Settings { get; set; }

        // Order Settings
        public bool OrderModuleEnabled { get; set; }
        public bool IsSupplierProductsOnly { get; set; }

        //Claim Settings
        public bool ClaimModuleEnabled { get; set; }
        public bool IsDairy { get; set; }
        public bool IsClaimAccountManager { get; set; }
        public bool IsClaimManager { get; set; }
        public string ClaimReportTitle { get; set; }
        public string ClaimReportAccountNumber { get; set; }
        public bool IsClaimAccountManagerRequired { get; set; }
        public bool IsClaimFloatRequired { get; set; }
        public bool IsEmployeeClaim { get; set; }
        public bool IsSubContractor { get; set; }

        public string ThemeColor { get; set; }
        public string ThemeCustomColor { get; set; }
        public JsonThemeSetting JsonTheme { get; set; }

        // Navigation Properties
        public Stakeholder Stakeholder { get; set; }
        public SupplierGroup SupplierGroup { get; set; }
        public Location PrimaryLocation { get; set; }
        public Contact PrimaryContact { get; set; }
        public ICollection<Vendor> Vendors { get; set; }
        public ICollection<GLAdjustment> GLAdjustments { get; set; }
        public ICollection<EngageMasterProduct> EngageMasterProducts { get; set; }
        public ICollection<SupplierSupplierType> SupplierSupplierTypes { get; set; }
        public ICollection<EngageSubGroupSupplier> EngageSubGroupSuppliers { get; set; }
        public ICollection<SupplierClaimAccountManager> SupplierClaimAccountManagers { get; set; }
        public ICollection<SupplierClaimClassification> SupplierClaimClassifications { get; set; }
        public ICollection<SupplierEngageBrand> SupplierEngageBrands { get; set; }
        public ICollection<SupplierProduct> SupplierProducts { get; set; }
        public ICollection<SupplierRegion> SupplierRegions { get; set; }
        public ICollection<SupplierStore> SupplierStores { get; set; }
    }
}
