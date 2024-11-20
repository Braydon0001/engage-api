namespace Engage.Persistence.Configurations
{
    public class ClientTypeConfiguration : IEntityTypeConfiguration<ClientType>
    {
        public void Configure(EntityTypeBuilder<ClientType> builder)
        {
            builder.ToTable("opt_ClientTypes");
        }
    }

    // Option Types
    public class OptionTypeGroupConfiguration : IEntityTypeConfiguration<OptionTypeGroup>
    {
        public void Configure(EntityTypeBuilder<OptionTypeGroup> builder)
        {
        }
    }

    public class OptionTypeConfiguration : IEntityTypeConfiguration<OptionType>
    {
        public void Configure(EntityTypeBuilder<OptionType> builder)
        {
            builder.HasOne(e => e.OptionTypeGroup)
                   .WithMany(e => e.OptionTypes)
                   .HasForeignKey(e => e.OptionTypeGroupId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

    // Assets
    public class AssetOwnerConfiguration : IEntityTypeConfiguration<AssetOwner>
    {
        public void Configure(EntityTypeBuilder<AssetOwner> builder)
        {
            builder.ToTable("opt_AssetOwners");
        }
    }

    public class AssetStatusConfiguration : IEntityTypeConfiguration<AssetStatus>
    {
        public void Configure(EntityTypeBuilder<AssetStatus> builder)
        {
            builder.ToTable("opt_AssetStatuses");
        }
    }

    // Attachement
    public class AttachmentTypeConfiguration : IEntityTypeConfiguration<AttachmentType>
    {
        public void Configure(EntityTypeBuilder<AttachmentType> builder)
        {
            builder.ToTable("opt_AttachmentTypes");
        }
    }

    // Bank Accounts 
    public class BankAccountOwnerConfiguration : IEntityTypeConfiguration<BankAccountOwner>
    {
        public void Configure(EntityTypeBuilder<BankAccountOwner> builder)
        {
            builder.ToTable("opt_BankAccountOwners");
        }
    }

    public class BankAccountTypeConfiguration : IEntityTypeConfiguration<BankAccountType>
    {
        public void Configure(EntityTypeBuilder<BankAccountType> builder)
        {
            builder.ToTable("opt_BankAccountTypes");
        }
    }

    public class BankPaymentMethodConfiguration : IEntityTypeConfiguration<BankPaymentMethod>
    {
        public void Configure(EntityTypeBuilder<BankPaymentMethod> builder)
        {
            builder.ToTable("opt_BankPaymentMethods");
        }
    }

    public class BankNameConfiguration : IEntityTypeConfiguration<BankName>
    {
        public void Configure(EntityTypeBuilder<BankName> builder)
        {
            builder.ToTable("opt_BankNames");
        }
    }

    // CRM Types
    public class ContactTypeConfiguration : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> builder)
        {
            builder.ToTable("opt_ContactTypes");
        }
    }
    public class EventTypeConfiguration : IEntityTypeConfiguration<EventType>
    {
        public void Configure(EntityTypeBuilder<EventType> builder)
        {
            builder.ToTable("opt_EventTypes");
        }
    }

    // Location Types
    public class LocationTypeConfiguration : IEntityTypeConfiguration<LocationType>
    {
        public void Configure(EntityTypeBuilder<LocationType> builder)
        {
            builder.ToTable("opt_LocationTypes");
        }
    }
    public class EngageRegionConfiguration : IEntityTypeConfiguration<EngageRegion>
    {
        public void Configure(EntityTypeBuilder<EngageRegion> builder)
        {
            builder.ToTable("opt_EngageRegions");
        }
    }
    public class EngageLocationConfiguration : IEntityTypeConfiguration<EngageLocation>
    {
        public void Configure(EntityTypeBuilder<EngageLocation> builder)
        {
            builder.ToTable("opt_EngageLocations");
        }
    }

    // Employee
    public class EmployeeAssetBrandConfiguration : IEntityTypeConfiguration<EmployeeAssetBrand>
    {
        public void Configure(EntityTypeBuilder<EmployeeAssetBrand> builder)
        {
            builder.ToTable("opt_EmployeeAssetBrands");
        }
    }
    public class EmployeeAssetTypeConfiguration : IEntityTypeConfiguration<EmployeeAssetType>
    {
        public void Configure(EntityTypeBuilder<EmployeeAssetType> builder)
        {
            builder.ToTable("opt_EmployeeAssetTypes");
        }
    }

    public class EmployeeCoolerBoxConditionConfiguration : IEntityTypeConfiguration<EmployeeCoolerBoxCondition>
    {
        public void Configure(EntityTypeBuilder<EmployeeCoolerBoxCondition> builder)
        {
            builder.ToTable("opt_EmployeeCoolerBoxConditions");
        }
    }

    public class EmployeeStateConfiguration : IEntityTypeConfiguration<EmployeeState>
    {
        public void Configure(EntityTypeBuilder<EmployeeState> builder)
        {
            builder.ToTable("opt_EmployeeStates");
        }
    }

    public class EmployeeIncentiveTierConfiguration : IEntityTypeConfiguration<EmployeeIncentiveTier>
    {
        public void Configure(EntityTypeBuilder<EmployeeIncentiveTier> builder)
        {
            builder.ToTable("opt_EmployeeIncentiveTiers");
        }
    }

    public class EmployeeLangugeConfiguration : IEntityTypeConfiguration<EmployeeLanguage>
    {
        public void Configure(EntityTypeBuilder<EmployeeLanguage> builder)
        {
            builder.ToTable("opt_EmployeeLanguages");
        }
    }

    public class EmployeePaymentTypeConfiguration : IEntityTypeConfiguration<EmployeePaymentType>
    {
        public void Configure(EntityTypeBuilder<EmployeePaymentType> builder)
        {
            builder.ToTable("opt_EmployeePaymentTypes");
        }
    }

    public class EmployeePensionSchemeConfiguration : IEntityTypeConfiguration<EmployeePensionScheme>
    {
        public void Configure(EntityTypeBuilder<EmployeePensionScheme> builder)
        {
            builder.ToTable("opt_EmployeePensionSchemes");
        }
    }

    public class EmployeePensionCategoryConfiguration : IEntityTypeConfiguration<EmployeePensionCategory>
    {
        public void Configure(EntityTypeBuilder<EmployeePensionCategory> builder)
        {
            builder.ToTable("opt_EmployeePensionCategories");
        }
    }

    public class EmployeePensionContributionPercentageConfiguration : IEntityTypeConfiguration<EmployeePensionContributionPercentage>
    {
        public void Configure(EntityTypeBuilder<EmployeePensionContributionPercentage> builder)
        {
            builder.ToTable("opt_EmployeePensionContributionPercentages");
        }
    }

    public class EmployeeFuelExpenseTypeConfiguration : IEntityTypeConfiguration<EmployeeFuelExpenseType>
    {
        public void Configure(EntityTypeBuilder<EmployeeFuelExpenseType> builder)
        {
            builder.ToTable("opt_EmployeeFuelExpenseTypes");
        }
    }

    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.ToTable("opt_LeaveTypes");
        }
    }

    public class BenefitTypeConfiguration : IEntityTypeConfiguration<BenefitType>
    {
        public void Configure(EntityTypeBuilder<BenefitType> builder)
        {
            builder.ToTable("opt_BenefitTypes");
        }
    }
    public class DeductionTypeConfiguration : IEntityTypeConfiguration<DeductionType>
    {
        public void Configure(EntityTypeBuilder<DeductionType> builder)
        {
            builder.ToTable("opt_DeductionTypes");
        }
    }
    public class DeductionCycleTypeConfiguration : IEntityTypeConfiguration<DeductionCycleType>
    {
        public void Configure(EntityTypeBuilder<DeductionCycleType> builder)
        {
            builder.ToTable("opt_DeductionCycleTypes");
        }
    }
    public class EmploymentTypeConfiguration : IEntityTypeConfiguration<EmploymentType>
    {
        public void Configure(EntityTypeBuilder<EmploymentType> builder)
        {
            builder.ToTable("opt_EmploymentTypes");
        }
    }

    public class EmployeeDisabledTypeConfiguration : IEntityTypeConfiguration<EmployeeDisabledType>
    {
        public void Configure(EntityTypeBuilder<EmployeeDisabledType> builder)
        {
            builder.ToTable("opt_EmployeeDisabledTypes");
        }
    }

    public class EmployeeIdentificationTypeConfiguration : IEntityTypeConfiguration<EmployeeIdentificationType>
    {
        public void Configure(EntityTypeBuilder<EmployeeIdentificationType> builder)
        {
            builder.ToTable("opt_EmployeeIdentificationTypes");
        }
    }

    public class EmployeeNationalityConfiguration : IEntityTypeConfiguration<EmployeeNationality>
    {
        public void Configure(EntityTypeBuilder<EmployeeNationality> builder)
        {
            builder.ToTable("opt_EmployeeNationalities");
        }
    }

    public class EmployeePersonTypeConfiguration : IEntityTypeConfiguration<EmployeePersonType>
    {
        public void Configure(EntityTypeBuilder<EmployeePersonType> builder)
        {
            builder.ToTable("opt_EmployeePersonTypes");
        }
    }
    public class EmployeeSDLExemptionConfiguration : IEntityTypeConfiguration<EmployeeSDLExemption>
    {
        public void Configure(EntityTypeBuilder<EmployeeSDLExemption> builder)
        {
            builder.ToTable("opt_EmployeeSDLExemptions");
        }
    }

    public class EmployeeTaxStatusConfiguration : IEntityTypeConfiguration<EmployeeTaxStatus>
    {
        public void Configure(EntityTypeBuilder<EmployeeTaxStatus> builder)
        {
            builder.ToTable("opt_EmployeeTaxStatuses");
        }
    }
    public class EmployeeUIFExemptionConfiguration : IEntityTypeConfiguration<EmployeeUIFExemption>
    {
        public void Configure(EntityTypeBuilder<EmployeeUIFExemption> builder)
        {
            builder.ToTable("opt_EmployeeUIFExemptions");
        }
    }
    public class EmployeeDefaultPayslipConfiguration : IEntityTypeConfiguration<EmployeeDefaultPayslip>
    {
        public void Configure(EntityTypeBuilder<EmployeeDefaultPayslip> builder)
        {
            builder.ToTable("opt_EmployeeDefaultPayslips");
        }
    }

    public class EmployeeReinstatementReasonConfiguration : IEntityTypeConfiguration<EmployeeReinstatementReason>
    {
        public void Configure(EntityTypeBuilder<EmployeeReinstatementReason> builder)
        {
            builder.ToTable("opt_EmployeeReinstatementReasons");
        }
    }

    public class EmployeeSuspentionReasonConfiguration : IEntityTypeConfiguration<EmployeeSuspensionReason>
    {
        public void Configure(EntityTypeBuilder<EmployeeSuspensionReason> builder)
        {
            builder.ToTable("opt_EmployeeSuspensionReasons");
        }
    }

    public class EmployeeTerminationReasonConfiguration : IEntityTypeConfiguration<EmployeeTerminationReason>
    {
        public void Configure(EntityTypeBuilder<EmployeeTerminationReason> builder)
        {
            builder.ToTable("opt_EmployeeTerminationReasons");
        }
    }

    public class EmployeeStandardIndustryGroupCodeConfiguration : IEntityTypeConfiguration<EmployeeStandardIndustryGroupCode>
    {
        public void Configure(EntityTypeBuilder<EmployeeStandardIndustryGroupCode> builder)
        {
            builder.ToTable("opt_EmployeeStandardIndustryGroupCodes");
        }
    }

    public class EmployeeStandardIndustryCodeConfiguration : IEntityTypeConfiguration<EmployeeStandardIndustryCode>
    {
        public void Configure(EntityTypeBuilder<EmployeeStandardIndustryCode> builder)
        {
            builder.ToTable("opt_EmployeeStandardIndustryCodes");
        }
    }

    public class EmployeeKpiTypeConfiguration : IEntityTypeConfiguration<EmployeeKpiType>
    {
        public void Configure(EntityTypeBuilder<EmployeeKpiType> builder)
        {
            builder.ToTable("opt_EmployeeKpiTypes");
        }
    }
    public class EmploymentActionConfiguration : IEntityTypeConfiguration<EmploymentAction>
    {
        public void Configure(EntityTypeBuilder<EmploymentAction> builder)
        {
            builder.ToTable("opt_EmploymentActions");
        }
    }

    public class WebEventTypeConfiguration : IEntityTypeConfiguration<WebEventType>
    {
        public void Configure(EntityTypeBuilder<WebEventType> builder)
        {
            builder.ToTable("opt_WebEventTypes");
        }
    }

    public class EmployeeBadgeTypeConfiguration : IEntityTypeConfiguration<EmployeeBadgeType>
    {
        public void Configure(EntityTypeBuilder<EmployeeBadgeType> builder)
        {
            builder.ToTable("opt_EmployeeBadgeTypes");
        }
    }

    public class VehicleBrandConfiguration : IEntityTypeConfiguration<VehicleBrand>
    {
        public void Configure(EntityTypeBuilder<VehicleBrand> builder)
        {
            builder.ToTable("opt_VehicleBrands");
        }
    }
    public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
    {
        public void Configure(EntityTypeBuilder<VehicleType> builder)
        {
            builder.ToTable("opt_VehicleTypes");
        }
    }

    // Store 
    public class StoreAssetOwnerConfiguration : IEntityTypeConfiguration<StoreAssetOwner>
    {
        public void Configure(EntityTypeBuilder<StoreAssetOwner> builder)
        {
            builder.ToTable("opt_StoreAssetOwners");
        }
    }
    public class StoreAssetTypeConfiguration : IEntityTypeConfiguration<StoreAssetType>
    {
        public void Configure(EntityTypeBuilder<StoreAssetType> builder)
        {
            builder.ToTable("opt_StoreAssetTypes");
        }
    }

    public class StoreDepartmentConfiguration : IEntityTypeConfiguration<StoreDepartment>
    {
        public void Configure(EntityTypeBuilder<StoreDepartment> builder)
        {
            builder.ToTable("opt_StoreDepartments");
        }
    }

    public class StoreConceptConfiguration : IEntityTypeConfiguration<StoreConcept>
    {
        public void Configure(EntityTypeBuilder<StoreConcept> builder)
        {
            builder.Property(e => e.Files).HasColumnType("json");
            builder.ToTable("opt_StoreConcepts");
        }
    }

    public class StoreConceptTypeConfiguration : IEntityTypeConfiguration<StoreConceptType>
    {
        public void Configure(EntityTypeBuilder<StoreConceptType> builder)
        {
            builder.ToTable("opt_StoreConceptTypes");
        }
    }
    public class StoreConceptAttributeTypeConfiguration : IEntityTypeConfiguration<StoreConceptAttributeType>
    {
        public void Configure(EntityTypeBuilder<StoreConceptAttributeType> builder)
        {
            builder.ToTable("opt_StoreConceptAttributeTypes");
        }
    }

    public class StoreCycleOperationConfiguration : IEntityTypeConfiguration<StoreCycleOperation>
    {
        public void Configure(EntityTypeBuilder<StoreCycleOperation> builder)
        {
            builder.ToTable("opt_StoreCycleOperations");
        }
    }
    public class FrequencyTypeConfiguration : IEntityTypeConfiguration<FrequencyType>
    {
        public void Configure(EntityTypeBuilder<FrequencyType> builder)
        {
            builder.ToTable("opt_FrequencyTypes");
        }
    }
    public class StoreClaimTypeConfiguration : IEntityTypeConfiguration<StoreClaimType>
    {
        public void Configure(EntityTypeBuilder<StoreClaimType> builder)
        {
            builder.ToTable("opt_StoreClaimTypes");
        }
    }
    public class StoreGroupConfiguration : IEntityTypeConfiguration<StoreGroup>
    {
        public void Configure(EntityTypeBuilder<StoreGroup> builder)
        {
            builder.ToTable("opt_StoreGroups");
        }
    }
    public class StoreTypeConfiguration : IEntityTypeConfiguration<StoreType>
    {
        public void Configure(EntityTypeBuilder<StoreType> builder)
        {
            builder.ToTable("opt_StoreTypes");

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(300);
        }
    }
    public class StoreFormatConfiguration : IEntityTypeConfiguration<StoreFormat>
    {
        public void Configure(EntityTypeBuilder<StoreFormat> builder)
        {
            builder.ToTable("opt_StoreFormats");
        }
    }
    public class StoreProfileConfiguration : IEntityTypeConfiguration<StoreCluster>
    {
        public void Configure(EntityTypeBuilder<StoreCluster> builder)
        {
            builder.ToTable("opt_StoreClusters");
        }
    }
    public class StoreLSMConfiguration : IEntityTypeConfiguration<StoreLSM>
    {
        public void Configure(EntityTypeBuilder<StoreLSM> builder)
        {
            builder.ToTable("opt_StoreLSMs");
        }
    }
    public class StoreMediaGroupConfiguration : IEntityTypeConfiguration<StoreMediaGroup>
    {
        public void Configure(EntityTypeBuilder<StoreMediaGroup> builder)
        {
            builder.ToTable("opt_StoreMediaGroups");
        }
    }

    public class StoreSparRegionConfiguration : IEntityTypeConfiguration<StoreSparRegion>
    {
        public void Configure(EntityTypeBuilder<StoreSparRegion> builder)
        {
            builder.ToTable("opt_StoreSparRegions");
        }
    }

    public class StorePOSTypeConfiguration : IEntityTypeConfiguration<StorePOSType>
    {
        public void Configure(EntityTypeBuilder<StorePOSType> builder)
        {
            builder.ToTable("opt_StorePOSTypes");
        }
    }

    public class StorePOSFreezerTypeConfiguration : IEntityTypeConfiguration<StorePOSFreezerType>
    {
        public void Configure(EntityTypeBuilder<StorePOSFreezerType> builder)
        {
            builder.ToTable("opt_StorePOSFreezerTypes");
        }
    }

    // Product
    public class DCProductClassConfiguration : IEntityTypeConfiguration<DCProductClass>
    {
        public void Configure(EntityTypeBuilder<DCProductClass> builder)
        {
            builder.ToTable("opt_DCProductClasses");
        }
    }

    public class ProductClassificationConfiguration : IEntityTypeConfiguration<ProductClassification>
    {
        public void Configure(EntityTypeBuilder<ProductClassification> builder)
        {
            builder.ToTable("opt_ProductClassifications");
        }
    }

    // Warehouse
    public class WarehouseTypeConfiguration : IEntityTypeConfiguration<WarehouseType>
    {
        public void Configure(EntityTypeBuilder<WarehouseType> builder)
        {
            builder.ToTable("opt_WarehouseTypes");
        }
    }

    // Supplier
    public class SupplierTypeConfiguration : IEntityTypeConfiguration<SupplierType>
    {
        public void Configure(EntityTypeBuilder<SupplierType> builder)
        {
            builder.ToTable("opt_SupplierTypes");
        }
    }

    // StoreOwnerType
    public class StoreOwnerTypeConfiguration : IEntityTypeConfiguration<StoreOwnerType>
    {
        public void Configure(EntityTypeBuilder<StoreOwnerType> builder)
        {
            builder.ToTable("opt_StoreOwnerTypes");
        }
    }

    public class SupplierGroupConfiguration : IEntityTypeConfiguration<SupplierGroup>
    {
        public void Configure(EntityTypeBuilder<SupplierGroup> builder)
        {
            builder.ToTable("opt_SupplierGroups");
        }
    }

    public class SupplierRegionConfiguration : IEntityTypeConfiguration<SupplierRegion>
    {
        public void Configure(EntityTypeBuilder<SupplierRegion> builder)
        {
            builder.ToTable("opt_SupplierRegions");

            builder.HasOne(x => x.Supplier)
               .WithMany(c => c.SupplierRegions)
               .HasForeignKey(x => x.SupplierId)
               .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

    // Product
    public class UnitTypeConfiguration : IEntityTypeConfiguration<UnitType>
    {
        public void Configure(EntityTypeBuilder<UnitType> builder)
        {
            builder.ToTable("opt_UnitTypes");
        }
    }
    public class EngageTagConfiguration : IEntityTypeConfiguration<EngageTag>
    {
        public void Configure(EntityTypeBuilder<EngageTag> builder)
        {
            builder.ToTable("opt_EngageTags");
        }
    }
    public class EngageGroupConfiguration : IEntityTypeConfiguration<EngageGroup>
    {
        public void Configure(EntityTypeBuilder<EngageGroup> builder)
        {
            builder.ToTable("opt_EngageGroups");
        }
    }
    public class EngageSubGroupConfiguration : IEntityTypeConfiguration<EngageSubGroup>
    {
        public void Configure(EntityTypeBuilder<EngageSubGroup> builder)
        {
            builder.ToTable("opt_EngageSubGroups");

            builder.HasOne(x => x.EngageGroup)
                .WithMany(c => c.SubGroups)
                .HasForeignKey(x => x.EngageGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.StoreDepartment)
                .WithMany(c => c.EngageSubGroups)
                .HasForeignKey(x => x.StoreDepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
    public class EngageCategoryConfiguration : IEntityTypeConfiguration<EngageCategory>
    {
        public void Configure(EntityTypeBuilder<EngageCategory> builder)
        {
            builder.ToTable("opt_EngageCategories");

            builder.HasOne(x => x.EngageSubGroup)
                .WithMany(s => s.Categories)
                .HasForeignKey(x => x.EngageSubGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
    public class EngageSubCategoryConfiguration : IEntityTypeConfiguration<EngageSubCategory>
    {
        public void Configure(EntityTypeBuilder<EngageSubCategory> builder)
        {
            builder.ToTable("opt_EngageSubCategories");

            builder.HasOne(x => x.EngageCategory)
                .WithMany(s => s.SubCategories)
                .HasForeignKey(x => x.EngageCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
    public class EngageBrandConfiguration : IEntityTypeConfiguration<EngageBrand>
    {
        public void Configure(EntityTypeBuilder<EngageBrand> builder)
        {
            builder.ToTable("opt_EngageBrands");
        }
    }

    public class EngageDepartmentGroupConfiguration : IEntityTypeConfiguration<EngageDepartmentGroup>
    {
        public void Configure(EntityTypeBuilder<EngageDepartmentGroup> builder)
        {
            builder.ToTable("opt_EngageDepartmentGroups");
        }
    }

    public class EngageDepartmentConfiguration : IEntityTypeConfiguration<EngageDepartment>
    {
        public void Configure(EntityTypeBuilder<EngageDepartment> builder)
        {
            builder.ToTable("opt_EngageDepartments");
        }
    }

    public class EngageDepartmentCategoryConfiguration : IEntityTypeConfiguration<EngageDepartmentCategory>
    {
        public void Configure(EntityTypeBuilder<EngageDepartmentCategory> builder)
        {
            builder.ToTable("opt_EngageDepartmentCategories");
        }
    }

    public class ProductActiveStatusConfiguration : IEntityTypeConfiguration<ProductActiveStatus>
    {
        public void Configure(EntityTypeBuilder<ProductActiveStatus> builder)
        {
            builder.ToTable("opt_ProductActiveStatuses");
        }
    }

    public class ProductStatusConfiguration : IEntityTypeConfiguration<ProductStatus>
    {
        public void Configure(EntityTypeBuilder<ProductStatus> builder)
        {
            builder.ToTable("opt_ProductStatuses");
        }
    }

    public class ProductWarehouseStatusConfiguration : IEntityTypeConfiguration<ProductWarehouseStatus>
    {
        public void Configure(EntityTypeBuilder<ProductWarehouseStatus> builder)
        {
            builder.ToTable("opt_ProductWarehouseStatuses");
        }
    }

    public class ProductAnalysisGroupConfiguration : IEntityTypeConfiguration<ProductAnalysisGroup>
    {
        public void Configure(EntityTypeBuilder<ProductAnalysisGroup> builder)
        {
            builder.ToTable("opt_ProductAnalysisGroups");
        }
    }

    public class ProductAnalysisDivisionConfiguration : IEntityTypeConfiguration<ProductAnalysisDivision>
    {
        public void Configure(EntityTypeBuilder<ProductAnalysisDivision> builder)
        {
            builder.ToTable("opt_ProductAnalysisDivisions");
        }
    }

    public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
    {
        public void Configure(EntityTypeBuilder<NotificationType> builder)
        {
            builder.ToTable("opt_NotificationTypes");
        }
    }

    public class NotificationCategoryConfiguration : IEntityTypeConfiguration<NotificationCategory>
    {
        public void Configure(EntityTypeBuilder<NotificationCategory> builder)
        {
            builder.ToTable("opt_NotificationCategories");
        }
    }

    public class NotificationChannelConfiguration : IEntityTypeConfiguration<NotificationChannel>
    {
        public void Configure(EntityTypeBuilder<NotificationChannel> builder)
        {
            builder.ToTable("opt_NotificationChannels");
        }
    }

    public class OrderTypeConfiguration : IEntityTypeConfiguration<OrderType>
    {
        public void Configure(EntityTypeBuilder<OrderType> builder)
        {
            builder.ToTable("opt_OrderTypes");
        }
    }

    public class OrderSkuTypeConfiguration : IEntityTypeConfiguration<OrderSkuType>
    {
        public void Configure(EntityTypeBuilder<OrderSkuType> builder)
        {
            builder.ToTable("opt_OrderSkuTypes");
        }
    }

    public class OrderQuantityTypeConfiguration : IEntityTypeConfiguration<OrderQuantityType>
    {
        public void Configure(EntityTypeBuilder<OrderQuantityType> builder)
        {
            builder.ToTable("opt_OrderQuantityTypes");
        }
    }

    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("opt_OrderStatuses");
        }
    }

    public class OrderSkuStatusConfiguration : IEntityTypeConfiguration<OrderSkuStatus>
    {
        public void Configure(EntityTypeBuilder<OrderSkuStatus> builder)
        {
            builder.ToTable("opt_OrderSkuStatuses");
        }
    }

    public class SurveyTypeConfiguration : IEntityTypeConfiguration<SurveyType>
    {
        public void Configure(EntityTypeBuilder<SurveyType> builder)
        {
            builder.ToTable("opt_SurveyTypes");
        }
    }

    public class QuestionTypeConfiguration : IEntityTypeConfiguration<QuestionType>
    {
        public void Configure(EntityTypeBuilder<QuestionType> builder)
        {
            builder.ToTable("opt_QuestionTypes");
        }
    }

    public class QuestionFalseReasonConfiguration : IEntityTypeConfiguration<QuestionFalseReason>
    {
        public void Configure(EntityTypeBuilder<QuestionFalseReason> builder)
        {
            builder.ToTable("opt_QuestionFalseReasons");
        }
    }

    // Financial Reporting Types
    public class BudgetVersionConfiguration : IEntityTypeConfiguration<BudgetVersion>
    {
        public void Configure(EntityTypeBuilder<BudgetVersion> builder)
        {
            builder.ToTable("opt_BudgetVersions");
        }
    }
    public class BudgetTypeConfiguration : IEntityTypeConfiguration<BudgetType>
    {
        public void Configure(EntityTypeBuilder<BudgetType> builder)
        {
            builder.ToTable("opt_BudgetTypes");
        }
    }
    public class GLAdjustmentTypeConfiguration : IEntityTypeConfiguration<GLAdjustmentType>
    {
        public void Configure(EntityTypeBuilder<GLAdjustmentType> builder)
        {
            builder.ToTable("opt_GLAdjustmentTypes");
        }
    }

    //Claims

    public class ClaimStatusConfiguration : IEntityTypeConfiguration<ClaimStatus>
    {
        public void Configure(EntityTypeBuilder<ClaimStatus> builder)
        {
            builder.ToTable("opt_ClaimStatuses");
        }
    }

    public class ClaimSupplierStatusConfiguration : IEntityTypeConfiguration<ClaimSupplierStatus>
    {
        public void Configure(EntityTypeBuilder<ClaimSupplierStatus> builder)
        {
            builder.ToTable("opt_ClaimSupplierStatuses");
        }
    }

    public class ClaimSkuStatusConfiguration : IEntityTypeConfiguration<ClaimSkuStatus>
    {
        public void Configure(EntityTypeBuilder<ClaimSkuStatus> builder)
        {
            builder.ToTable("opt_ClaimSkuStatuses");
        }
    }

    public class EntityContactTypeConfiguration : IEntityTypeConfiguration<EntityContactType>
    {
        public void Configure(EntityTypeBuilder<EntityContactType> builder)
        {
            builder.ToTable("opt_EntityContactTypes");
        }
    }

    public class ClaimQuantityTypeConfiguration : IEntityTypeConfiguration<ClaimQuantityType>
    {
        public void Configure(EntityTypeBuilder<ClaimQuantityType> builder)
        {
            builder.ToTable("opt_ClaimQuantityTypes");
        }
    }

    public class ClaimRejectedReasonConfiguration : IEntityTypeConfiguration<ClaimRejectedReason>
    {
        public void Configure(EntityTypeBuilder<ClaimRejectedReason> builder)
        {
            builder.ToTable("opt_ClaimRejectedReasons");
        }
    }

    public class ClaimPendingReasonConfiguration : IEntityTypeConfiguration<ClaimPendingReason>
    {
        public void Configure(EntityTypeBuilder<ClaimPendingReason> builder)
        {
            builder.ToTable("opt_ClaimPendingReasons");
        }
    }

    public class ClaimReportTypeConfiguration : IEntityTypeConfiguration<ClaimReportType>
    {
        public void Configure(EntityTypeBuilder<ClaimReportType> builder)
        {
            builder.ToTable("opt_ClaimReportTypes");
        }
    }

    public class EmailTemplateTypeConfiguration : IEntityTypeConfiguration<EmailTemplateType>
    {
        public void Configure(EntityTypeBuilder<EmailTemplateType> builder)
        {
            builder.ToTable("opt_EmailTemplateTypes");
        }
    }

    public class EmailTypeConfiguration : IEntityTypeConfiguration<EmailType>
    {
        public void Configure(EntityTypeBuilder<EmailType> builder)
        {
            builder.ToTable("opt_EmailTypes");
        }
    }

    public class PromotionTypeConfiguration : IEntityTypeConfiguration<PromotionType>
    {
        public void Configure(EntityTypeBuilder<PromotionType> builder)
        {
            builder.ToTable("opt_PromotionTypes");
        }
    }

    public class PromotionProductTypeConfiguration : IEntityTypeConfiguration<PromotionProductType>
    {
        public void Configure(EntityTypeBuilder<PromotionProductType> builder)
        {
            builder.ToTable("opt_PromotionProductTypes");
        }
    }

    public class ExpenseClaimStatusConfiguration : IEntityTypeConfiguration<ExpenseClaimStatus>
    {
        public void Configure(EntityTypeBuilder<ExpenseClaimStatus> builder)
        {
            builder.ToTable("opt_ExpenseClaimStatuses");
        }
    }

    public class WorkRoleStatusConfiguration : IEntityTypeConfiguration<WorkRoleStatus>
    {
        public void Configure(EntityTypeBuilder<WorkRoleStatus> builder)
        {
            builder.ToTable("opt_WorkRoleStatuses");
        }
    }

    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("opt_Grades");
        }
    }

    public class SkillCategoryConfiguration : IEntityTypeConfiguration<SkillCategory>
    {
        public void Configure(EntityTypeBuilder<SkillCategory> builder)
        {
            builder.ToTable("opt_SkillCategories");
        }
    }

    public class ProficiencyConfiguration : IEntityTypeConfiguration<Proficiency>
    {
        public void Configure(EntityTypeBuilder<Proficiency> builder)
        {
            builder.ToTable("opt_Proficiencies");
        }
    }

    public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.ToTable("opt_Experiences");
        }
    }

    public class EducationLevelConfiguration : IEntityTypeConfiguration<EducationLevel>
    {
        public void Configure(EntityTypeBuilder<EducationLevel> builder)
        {
            builder.ToTable("opt_EducationLevels");
        }
    }

    public class InstitutionTypeConfiguration : IEntityTypeConfiguration<InstitutionType>
    {
        public void Configure(EntityTypeBuilder<InstitutionType> builder)
        {
            builder.ToTable("opt_InstitutionTypes");
        }
    }

    public class EmployeeTrainingStatusConfiguration : IEntityTypeConfiguration<EmployeeTrainingStatus>
    {
        public void Configure(EntityTypeBuilder<EmployeeTrainingStatus> builder)
        {
            builder.ToTable("opt_EmployeeTrainingStatuses");
        }
    }

    public class TitleConfiguration : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.ToTable("opt_Titles");
        }
    }

    public class MaritalStatusConfiguration : IEntityTypeConfiguration<MaritalStatus>
    {
        public void Configure(EntityTypeBuilder<MaritalStatus> builder)
        {
            builder.ToTable("opt_MaritalStatuses");
        }
    }

    //public class ProfileNationalityConfiguration : IEntityTypeConfiguration<ProfileNationality>
    //{
    //    public void Configure(EntityTypeBuilder<ProfileNationality> builder)
    //    {
    //        builder.ToTable("opt_ProfileNationalities");
    //    }
    //}

    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("opt_Provinces");
        }
    }

    //public class ProfileCitizenshipConfiguration : IEntityTypeConfiguration<ProfileCitizenship>
    //{
    //    public void Configure(EntityTypeBuilder<ProfileCitizenship> builder)
    //    {
    //        builder.ToTable("opt_ProfileCitizenships");
    //    }
    //}

    public class RaceConfiguration : IEntityTypeConfiguration<Race>
    {
        public void Configure(EntityTypeBuilder<Race> builder)
        {
            builder.ToTable("opt_Races");
        }
    }

    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("opt_Genders");
        }
    }

    public class UniformSizeConfiguration : IEntityTypeConfiguration<UniformSize>
    {
        public void Configure(EntityTypeBuilder<UniformSize> builder)
        {
            builder.ToTable("opt_UniformSizes");
        }
    }

    public class NextOfKinTypeConfiguration : IEntityTypeConfiguration<NextOfKinType>
    {
        public void Configure(EntityTypeBuilder<NextOfKinType> builder)
        {
            builder.ToTable("opt_NextOfKinTypes");
        }
    }

    public class EmployeePayRateFrequencyConfiguration : IEntityTypeConfiguration<EmployeePayRateFrequency>
    {
        public void Configure(EntityTypeBuilder<EmployeePayRateFrequency> builder)
        {
            builder.ToTable("opt_EmployeePayRateFrequencies");
        }
    }

    public class EmployeePayRatePackageConfiguration : IEntityTypeConfiguration<EmployeePayRatePackage>
    {
        public void Configure(EntityTypeBuilder<EmployeePayRatePackage> builder)
        {
            builder.ToTable("opt_EmployeePayRatePackages");
        }
    }

    public class VoucherTypeConfiguration : IEntityTypeConfiguration<VoucherType>
    {
        public void Configure(EntityTypeBuilder<VoucherType> builder)
        {
            builder.ToTable("opt_VoucherTypes");
        }
    }

    public class VoucherStatusConfiguration : IEntityTypeConfiguration<VoucherStatus>
    {
        public void Configure(EntityTypeBuilder<VoucherStatus> builder)
        {
            builder.ToTable("opt_VoucherStatuses");
        }
    }

    public class VoucherDetailStatusConfiguration : IEntityTypeConfiguration<VoucherDetailStatus>
    {
        public void Configure(EntityTypeBuilder<VoucherDetailStatus> builder)
        {
            builder.ToTable("opt_VoucherDetailStatuses");
        }
    }
}
