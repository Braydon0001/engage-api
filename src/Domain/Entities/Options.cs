namespace Engage.Domain.Entities
{
    public class ClientType : BaseOption { }

    // Option Types. Used to manage options. 
    public class OptionTypeGroup : BaseOption
    {
        public OptionTypeGroup()
        {
            OptionTypes = new HashSet<OptionType>();
        }

        // Navigation Properties
        public ICollection<OptionType> OptionTypes { get; set; }
    }
    public class OptionType : BaseOption
    {
        public int OptionTypeGroupId { get; set; }
        public bool IsSystemOption { get; set; }

        // Navigation Properties
        public OptionTypeGroup OptionTypeGroup { get; set; }
    }

    // Assets
    public class AssetOwner : BaseOption { }
    public class AssetStatus : BaseOption { }

    // Attachments
    public class AttachmentType : BaseOption { }

    // Bank Accounts 
    public class BankAccountOwner : BaseOption { }
    public class BankAccountType : BaseOption { }
    public class BankPaymentMethod : BaseOption { }
    public class BankName : BaseOption { }

    // CRM Types
    public class ContactType : BaseOption { }
    public class EventType : BaseOption { }

    // Location Types
    public class LocationType : BaseOption { }
    public class EngageRegion : BaseOption
    {
        public EngageRegion()
        {
            EngageRegionClaimManagers = new HashSet<EngageRegionClaimManager>();
            Employees = new HashSet<EmployeeRegion>();
            Users = new HashSet<UserRegion>();
            EngageSubRegions = new HashSet<EngageSubRegion>();
            Stores = new HashSet<Store>();
            NotificationRegions = new HashSet<NotificationRegion>();
            SurveyEngageRegions = new HashSet<SurveyEngageRegion>();
            GlAccounts = new HashSet<GLAccount>();
            EmployeeRegionContacts = new HashSet<EmployeeRegionContact>();
            ProductWarehouseRegions = new HashSet<ProductWarehouseRegion>();
            ProjectTacOpRegions = new HashSet<ProjectTacOpRegion>();
            EntityContacts = new HashSet<EntityContactRegion>();
        }
        public bool IsAllRegions { get; set; }
        public bool IsApproveClaims { get; set; }
        public bool IsClaimManager { get; set; }
        public int? StoreSparRegionId { get; set; }

        public StoreSparRegion StoreSparRegion { get; set; }
        public ICollection<EngageRegionClaimManager> EngageRegionClaimManagers { get; set; }
        public ICollection<EmployeeRegion> Employees { get; set; }
        public ICollection<UserRegion> Users { get; set; }
        public ICollection<EngageSubRegion> EngageSubRegions { get; set; }
        public ICollection<Store> Stores { get; set; }
        public ICollection<NotificationRegion> NotificationRegions { get; set; }
        public ICollection<SurveyEngageRegion> SurveyEngageRegions { get; set; }
        public ICollection<GLAccount> GlAccounts { get; set; }
        public ICollection<EmployeeRegionContact> EmployeeRegionContacts { get; set; }
        public ICollection<ProductWarehouseRegion> ProductWarehouseRegions { get; set; }
        public ICollection<ProjectTacOpRegion> ProjectTacOpRegions { get; private set; }
        public ICollection<EntityContactRegion> EntityContacts { get; set; }
    }
    public class EngageLocation : BaseOption { }

    // Employee
    public class EmployeeAssetBrand : BaseOption { }
    public class EmployeeAssetType : BaseOption { }
    public class EmployeeCoolerBoxCondition : BaseOption { }
    public class EmployeeState : BaseOption { }
    public class EmployeeIncentiveTier : BaseOption { }

    public class EmployeeLanguage : BaseOption { }
    public class EmployeeFuelExpenseType : BaseOption { }
    public class EmployeePaymentType : BaseOption { }
    public class EmployeePayRateFrequency : BaseOption { }
    public class EmployeePayRatePackage : BaseOption { }
    public class EmployeePensionScheme : BaseOption { }
    public class EmployeePensionCategory : BaseOption { }
    public class EmployeePensionContributionPercentage : BaseOption { }
    public class LeaveType : BaseOption { }
    public class BenefitType : BaseOption { }
    public class DeductionType : BaseOption { }
    public class DeductionCycleType : BaseOption { }
    public class EmploymentType : BaseOption
    {
        public int? EndDateReminderDays { get; set; }
    }
    public class EmployeeDisabledType : BaseOption { }
    public class EmployeeIdentificationType : BaseOption { }
    public class EmployeeNationality : BaseOption { }
    public class EmployeePersonType : BaseOption { }
    public class EmployeeSDLExemption : BaseOption { }
    public class EmployeeTaxStatus : BaseOption { }
    public class EmployeeUIFExemption : BaseOption { }
    public class EmployeeDefaultPayslip : BaseOption { }
    public class EmployeeReinstatementReason : BaseOption { }
    public class EmployeeSuspensionReason : BaseOption { }
    public class EmployeeTerminationReason : BaseOption { }
    public class EmployeeStandardIndustryGroupCode : BaseOption { }
    public class EmployeeStandardIndustryCode : BaseOption { }
    public class EmployeeKpiType : BaseOption { }
    public class EmployeeBadgeType : BaseOption { }
    public class EmploymentAction : BaseOption { }
    public class VehicleBrand : BaseOption { }
    public class VehicleType : BaseOption { }
    public class WebEventType : BaseOption { }

    // Store 

    public class StoreAssetOwner : BaseOption
    {
        public StoreAssetOwner()
        {
            AssetOwnerAssetTypes = new HashSet<StoreAssetOwnerStoreAssetType>();
        }

        public ICollection<StoreAssetOwnerStoreAssetType> AssetOwnerAssetTypes { get; set; }
    }
    public class StoreOwnerType : BaseOption { }
    public class StoreAssetType : BaseOption
    {
        public StoreAssetType()
        {
            AssetOwnerAssetTypes = new HashSet<StoreAssetOwnerStoreAssetType>();
            AssetSubTypes = new HashSet<StoreAssetTypeStoreAssetSubType>();
            AssetContacts = new HashSet<StoreAssetTypeStoreAssetTypeContact>();
        }

        public ICollection<StoreAssetOwnerStoreAssetType> AssetOwnerAssetTypes { get; set; }
        public ICollection<StoreAssetTypeStoreAssetSubType> AssetSubTypes { get; set; }
        public ICollection<StoreAssetTypeStoreAssetTypeContact> AssetContacts { get; set; }
    }
    public class StoreDepartment : BaseOption
    {
        public StoreDepartment()
        {
            StoreDepartments = new HashSet<StoreStoreDepartment>();
            EngageSubGroups = new HashSet<EngageSubGroup>();
        }
        public ICollection<StoreStoreDepartment> StoreDepartments { get; set; }

        public ICollection<EngageSubGroup> EngageSubGroups { get; set; }
    }
    public class StoreConcept : BaseOption
    {
        public StoreConcept()
        {
            StoreConceptLevels = new HashSet<StoreConceptLevel>();
            StoreStoreConceptPerformances = new HashSet<StoreStoreConceptPerformance>();

        }
        public int EngageDepartmentId { get; set; }
        public EngageDepartment EngageDepartment { get; set; }
        public List<JsonFile> Files { get; set; }
        public ICollection<StoreConceptLevel> StoreConceptLevels { get; set; }
        //Many to Many
        public ICollection<StoreStoreConceptPerformance> StoreStoreConceptPerformances { get; set; }

    }

    public class StoreConceptType : BaseOption { }
    public class StoreConceptAttributeType : BaseOption { }
    public class FrequencyType : BaseOption { }
    public class StoreClaimType : BaseOption { }
    public class StoreCycleOperation : BaseOption { }
    public class StoreGroup : BaseOption { }
    public class StoreType : BaseOption
    {
        public string ImageUrl { get; set; }
    }
    public class StoreFormat : BaseOption
    {
        public StoreFormat()
        {
            SurveyStoreFormats = new HashSet<SurveyStoreFormat>();
        }

        public ICollection<SurveyStoreFormat> SurveyStoreFormats { get; set; }
    }
    public class StoreCluster : BaseOption { }
    public class StoreLSM : BaseOption { }
    public class StoreMediaGroup : BaseOption { }
    public class StoreSparRegion : BaseOption { }
    public class StorePOSType : BaseOption { }
    public class StorePOSFreezerType : BaseOption { }

    // Product
    public class DCProductClass : BaseOption { }
    public class ProductClassification : BaseOption { }

    // Warehouse
    public class WarehouseType : BaseOption { }

    // Supplier
    public class SupplierType : BaseOption
    {
        public SupplierType()
        {
            SupplierSupplierTypes = new HashSet<SupplierSupplierType>();
        }
        public ICollection<SupplierSupplierType> SupplierSupplierTypes { get; private set; }
    }
    public class SupplierGroup : BaseOption { }

    public class SupplierRegion : BaseOption
    {
        public SupplierRegion()
        {
            SupplierSubRegions = [];
        }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<SupplierSubRegion> SupplierSubRegions { get; set; }
    }

    // Product
    public class UnitType : BaseOption { }
    public class EngageTag : BaseOption
    {
        public EngageTag()
        {
            EngageProductTags = new HashSet<EngageProductTag>();
        }
        public ICollection<EngageProductTag> EngageProductTags { get; set; }
    }
    public class EngageGroup : BaseOption
    {
        public EngageGroup()
        {
            SubGroups = new HashSet<EngageSubGroup>();
        }

        public ICollection<EngageSubGroup> SubGroups { get; set; }
    }
    public class EngageSubGroup : BaseOption
    {
        public EngageSubGroup()
        {
            Categories = new HashSet<EngageCategory>();
            EngageBrands = new HashSet<EngageSubGroupEngageBrand>();
            EngageSubGroupSuppliers = new HashSet<EngageSubGroupSupplier>();
        }
        public int EngageGroupId { get; set; }
        public int EngageDepartmentCategoryId { get; set; }
        public int StoreDepartmentId { get; set; }
        public int EngageDepartmentId { get; set; }
        public EngageGroup EngageGroup { get; set; }
        public EngageDepartmentCategory EngageDepartmentCategory { get; set; }
        public StoreDepartment StoreDepartment { get; set; }
        public EngageDepartment EngageDepartment { get; set; }
        public ICollection<EngageCategory> Categories { get; set; }
        public ICollection<EngageSubGroupEngageBrand> EngageBrands { get; set; }
        public ICollection<EngageSubGroupSupplier> EngageSubGroupSuppliers { get; set; }
    }
    public class EngageCategory : BaseOption
    {
        public EngageCategory()
        {
            SubCategories = new HashSet<EngageSubCategory>();
        }
        public int EngageSubGroupId { get; set; }

        public EngageSubGroup EngageSubGroup { get; set; }
        public ICollection<EngageSubCategory> SubCategories { get; set; }
    }
    public class EngageSubCategory : BaseOption
    {
        public int EngageCategoryId { get; set; }

        public EngageCategory EngageCategory { get; set; }

    }
    public class EngageBrand : BaseOption
    {
        public EngageBrand()
        {
            EngageSubGroups = new HashSet<EngageSubGroupEngageBrand>();
            SupplierEngageBrands = new HashSet<SupplierEngageBrand>();
        }
        public bool IsSparBrand { get; set; }
        public ICollection<EngageSubGroupEngageBrand> EngageSubGroups { get; set; }
        public ICollection<SupplierEngageBrand> SupplierEngageBrands { get; set; }
    }

    public class EngageDepartmentGroup : BaseOption
    {
        public EngageDepartmentGroup()
        {
            EngageDepartments = new HashSet<EngageDepartment>();

        }

        public ICollection<EngageDepartment> EngageDepartments { get; set; }
    }

    public class EngageDepartment : BaseOption
    {
        public EngageDepartment()
        {
            Employees = new HashSet<EmployeeDepartment>();
            OrderEngageDepartments = new HashSet<OrderEngageDepartment>();
        }

        public int EngageDepartmentGroupId { get; set; }
        public EngageDepartmentGroup EngageDepartmentGroup { get; set; }

        public ICollection<EmployeeDepartment> Employees { get; set; }
        public ICollection<OrderEngageDepartment> OrderEngageDepartments { get; set; }
    }

    public class EngageDepartmentCategory : BaseOption
    {
        public int EngageDepartmentId { get; set; }
        public EngageDepartment EngageDepartment { get; set; }
    }

    public class ProductActiveStatus : BaseOption { }
    public class ProductStatus : BaseOption { }
    public class ProductWarehouseStatus : BaseOption { }
    public class ProductAnalysisGroup : BaseOption { }
    public class ProductAnalysisDivision : BaseOption { }

    // Notification

    public class NotificationType : BaseOption { }
    public class NotificationCategory : BaseOption { }
    public class NotificationChannel : BaseOption
    {
        public NotificationChannel()
        {
            NotificationChannels = new HashSet<NotificationNotificationChannel>();
        }
        public ICollection<NotificationNotificationChannel> NotificationChannels { get; set; }
    }

    // Orders
    public class OrderType : BaseOption { }
    public class OrderSkuType : BaseOption { }
    public class OrderStatus : BaseOption { }
    public class OrderSkuStatus : BaseOption { }
    public class OrderQuantityType : BaseOption { }

    // Survey
    public class SurveyType : BaseOption { }
    public class QuestionType : BaseOption { }
    public class QuestionFalseReason : BaseOption
    {
        public QuestionFalseReason()
        {
            SurveyQuestionFalseReasons = new HashSet<SurveyQuestionFalseReason>();
        }
        public ICollection<SurveyQuestionFalseReason> SurveyQuestionFalseReasons { get; set; }
    }


    // Financial Reporting
    public class BudgetVersion : BaseOption
    {
        public BudgetVersion()
        {
            BudgetYearVersions = new HashSet<BudgetYearVersion>();
            Budgets = new HashSet<Budget>();
        }

        public ICollection<BudgetYearVersion> BudgetYearVersions { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }

    public class BudgetType : BaseOption
    {
        public BudgetType()
        {
            Budgets = new HashSet<Budget>();
        }

        public ICollection<Budget> Budgets { get; set; }
    }

    public class GLAdjustmentType : BaseOption
    {
        public GLAdjustmentType()
        {
            GLAdjustments = new HashSet<GLAdjustment>();
        }

        public ICollection<GLAdjustment> GLAdjustments { get; set; }
    }

    //Claims    
    public class ClaimStatus : BaseOption { }
    public class ClaimSupplierStatus : BaseOption { }
    public class ClaimSkuStatus : BaseOption { }
    public class EntityContactType : BaseOption { }
    public class ClaimQuantityType : BaseOption { }
    public class ClaimRejectedReason : BaseOption { }
    public class ClaimPendingReason : BaseOption { }
    public class ClaimReportType : BaseOption
    {

        public ClaimReportType()
        {
            ClaimTypeReportTypes = new HashSet<ClaimTypeReportType>();
        }

        public ICollection<ClaimTypeReportType> ClaimTypeReportTypes { get; set; }
    }
    public class EmailTemplateType : BaseOption { }
    public class EmailType : BaseOption { }

    // Promotions
    public class PromotionType : BaseOption { }
    public class PromotionProductType : BaseOption { }

    public class ExpenseClaimStatus : BaseOption { }
    public class WorkRoleStatus : BaseOption { }

    public class Grade : BaseOption { }
    public class SkillCategory : BaseOption { }
    public class Proficiency : BaseOption { }
    public class Experience : BaseOption { }
    public class EducationLevel : BaseOption { }
    public class InstitutionType : BaseOption { }
    public class EmployeeTrainingStatus : BaseOption { }

    public class Title : BaseOption { }
    public class MaritalStatus : BaseOption { }
    public class Province : BaseOption { }
    public class Race : BaseOption { }
    public class Gender : BaseOption { }
    public class UniformSize : BaseOption { }
    public class NextOfKinType : BaseOption { }

    public class VoucherType : BaseOption { }
    public class VoucherStatus : BaseOption { }
    public class VoucherDetailStatus : BaseOption { }
}