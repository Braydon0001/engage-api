namespace Engage.Domain.Entities
{
    public class Store : BaseMasterEntity
    {
        public Store()
        {
            BankDetails = new HashSet<StoreBankDetail>();
            DCAccounts = new HashSet<DCAccount>();
            StoreAssets = new HashSet<StoreAsset>();
            StoreStoreDepartments = new HashSet<StoreStoreDepartment>();
            StoreConceptLevels = new HashSet<StoreConceptLevel>();
            StoreStoreLists = new HashSet<StoreStoreList>();
            EmployeeStores = new HashSet<EmployeeStore>();
            EmployeeStoreArchives = new HashSet<EmployeeStoreArchive>();
            EmployeeStoreCheckIns = new HashSet<EmployeeStoreCheckIn>();
            SurveyInstances = new HashSet<SurveyInstance>();
            SurveyStores = new HashSet<SurveyStore>();
            SupplierStores = new HashSet<SupplierStore>();
            StoreContacts = new HashSet<StoreContact>();
            PromotionStores = new HashSet<PromotionStore>();
            EmployeeStoreKpis = new HashSet<EmployeeStoreKpi>();
            EmployeeStoreKpiScores = new HashSet<EmployeeStoreKpiScore>();
            StoreStoreConceptPerformances = new HashSet<StoreStoreConceptPerformance>();
            StoreCategoryGroups = new HashSet<CategoryStoreGroup>();
            StoreOwners = new HashSet<StoreOwner>();
            StoreConceptAttributeValues = new HashSet<StoreConceptAttributeValue>();

        }

        public int StoreId { get; set; }
        public int? ParentStoreId { get; set; }
        public int StakeholderId { get; set; }
        public int StoreClusterId { get; set; }
        public int StoreFormatId { get; set; }
        public int StoreGroupId { get; set; }
        public int StoreLSMId { get; set; }
        public int StoreMediaGroupId { get; set; }
        public int StoreTypeId { get; set; }
        public int StoreLocationTypeId { get; set; }
        public int StoreSparRegionId { get; set; }
        public int EngageRegionId { get; set; }
        public int? EngageSubRegionId { get; set; }
        public int? EngageLocationId { get; set; }
        public int? PrimaryLocationId { get; set; }
        public int? PrimaryContactId { get; set; }
        public string CatManStoreCode { get; set; }
        public string StoreImageUrl { get; set; }
        public string VatNumber { get; set; }
        public bool IsHalaal { get; set; }
        public bool IsNotServiced { get; set; }

        // Navigation Properties       
        public Store ParentStore { get; set; }
        public Stakeholder Stakeholder { get; set; }
        public StoreCluster StoreCluster { get; set; }
        public StoreGroup StoreGroup { get; set; }
        public StoreType StoreType { get; set; }
        public StoreFormat StoreFormat { get; set; }
        public StoreLSM StoreLSM { get; set; }
        public StoreMediaGroup StoreMediaGroup { get; set; }
        public StoreSparRegion StoreSparRegion { get; set; }
        public LocationType StoreLocationType { get; set; }
        public EngageRegion EngageRegion { get; set; }
        public EngageSubRegion EngageSubRegion { get; set; }
        public EngageLocation EngageLocation { get; set; }
        public Location PrimaryLocation { get; set; }
        public Contact PrimaryContact { get; set; }
        public ICollection<StoreBankDetail> BankDetails { get; set; }
        public ICollection<DCAccount> DCAccounts { get; set; }
        public ICollection<StoreAsset> StoreAssets { get; set; }
        public ICollection<StoreStoreDepartment> StoreStoreDepartments { get; set; }
        public ICollection<StoreConceptLevel> StoreConceptLevels { get; set; }
        public ICollection<StoreStoreList> StoreStoreLists { get; set; }
        public ICollection<EmployeeStore> EmployeeStores { get; set; }
        public ICollection<EmployeeStoreArchive> EmployeeStoreArchives { get; set; }
        public ICollection<EmployeeStoreCheckIn> EmployeeStoreCheckIns { get; set; }
        public ICollection<SurveyInstance> SurveyInstances { get; set; }
        public ICollection<SurveyStore> SurveyStores { get; set; }
        public ICollection<SupplierStore> SupplierStores { get; set; }
        public ICollection<StoreContact> StoreContacts { get; set; }
        public ICollection<PromotionStore> PromotionStores { get; set; }
        public ICollection<EmployeeStoreKpi> EmployeeStoreKpis { get; private set; }
        public ICollection<EmployeeStoreKpiScore> EmployeeStoreKpiScores { get; private set; }
        public ICollection<StoreOwner> StoreOwners { get; private set; }
        //many to many
        public ICollection<StoreStoreConceptPerformance> StoreStoreConceptPerformances { get; set; }
        public ICollection<CategoryStoreGroup> StoreCategoryGroups { get; set; }
        public ICollection<StoreConceptAttributeValue> StoreConceptAttributeValues { get; set; }
    }
}
