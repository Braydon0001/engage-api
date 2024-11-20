using Finbuckle.MultiTenant.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Engage.Application.Interfaces;

public interface IAppDbContext : IMultiTenantDbContext
{
    #region Entities
    DbSet<ApiKey> ApiKeys { get; set; }
    DbSet<Budget> Budgets { get; set; }
    DbSet<BudgetPeriod> BudgetPeriods { get; set; }
    DbSet<BudgetYear> BudgetYears { get; set; }
    DbSet<CategoryFile> CategoryFiles { get; set; }
    DbSet<CategoryFileTarget> CategoryFileTargets { get; set; }
    DbSet<CategoryFileEmployeeJobTitle> CategoryFileEmployeeJobTitles { get; set; }
    DbSet<CategoryFileEngageRegion> CategoryFileEngageRegions { get; set; }
    DbSet<CategoryFileEmployee> CategoryFileEmployees { get; set; }
    DbSet<CategoryFileEngageSubGroup> CategoryFileEngageSubGroups { get; set; }
    DbSet<CategoryFileStore> CategoryFileStores { get; set; }
    DbSet<CategoryFileCategoryGroup> CategoryFileCategoryGroups { get; set; }
    DbSet<CategoryFileStoreFormat> CategoryFileStoreFormats { get; set; }
    DbSet<CategoryFileType> CategoryFileTypes { get; set; }
    DbSet<CategoryGroup> CategoryGroups { get; set; }
    DbSet<CategoryStoreGroup> CategoryStoreGroups { get; set; }
    DbSet<CategorySubGroup> CategorySubGroups { get; set; }
    DbSet<CategoryTarget> CategoryTargets { get; set; }
    DbSet<CategoryTargetType> CategoryTargetTypes { get; set; }
    DbSet<CategoryTargetAnswer> CategoryTargetAnswers { get; set; }
    DbSet<CategoryTargetAnswerHistory> CategoryTargetAnswerHistories { get; set; }
    DbSet<CategoryTargetStore> CategoryTargetStores { get; set; }
    DbSet<Claim> Claims { get; set; }
    DbSet<ClaimBatch> ClaimBatches { get; set; }
    DbSet<ClaimBatchDetail> ClaimBatchDetails { get; set; }
    DbSet<ClaimBlob> ClaimBlobs { get; set; }
    DbSet<ClaimClassification> ClaimClassifications { get; set; }
    DbSet<ClaimFloat> ClaimFloats { get; set; }
    DbSet<ClaimFloatTopUpHistory> ClaimFloatTopUpHistories { get; set; }
    DbSet<ClaimHistory> ClaimHistories { get; set; }
    DbSet<ClaimHistoryHeader> ClaimHistoryHeaders { get; set; }
    DbSet<ClaimNotificationUser> ClaimNotificationUsers { get; set; }
    DbSet<ClaimPeriod> ClaimPeriods { get; set; }
    DbSet<ClaimSku> ClaimSkus { get; set; }
    DbSet<ClaimSkuType> ClaimSkuTypes { get; set; }
    DbSet<ClaimStatusUser> ClaimStatusUsers { get; set; }
    DbSet<ClaimType> ClaimTypes { get; set; }
    DbSet<ClaimYear> ClaimYears { get; set; }
    DbSet<CommunicationHistory> CommunicationHistories { get; set; }
    DbSet<CommunicationHistoryClaim> CommunicationHistoryClaims { get; set; }
    DbSet<CommunicationHistoryClaimFloat> CommunicationHistoryClaimFloats { get; set; }
    DbSet<CommunicationHistoryEmployee> CommunicationHistoryEmployees { get; set; }
    DbSet<CommunicationHistoryEmployeeStoreCalendar> CommunicationHistoryEmployeeStoreCalendars { get; set; }
    DbSet<CommunicationHistoryOrder> CommunicationHistoryOrders { get; set; }
    DbSet<CommunicationHistoryProject> CommunicationHistoryProjects { get; set; }
    DbSet<CommunicationHistoryStore> CommunicationHistoryStores { get; set; }
    DbSet<CommunicationTemplate> CommunicationTemplates { get; set; }
    DbSet<CommunicationTemplateType> CommunicationTemplateTypes { get; set; }
    DbSet<CommunicationType> CommunicationTypes { get; set; }
    DbSet<Contact> Contacts { get; set; }
    DbSet<ContactEvent> ContactEvents { get; set; }
    DbSet<ContactItem> ContactItems { get; set; }
    DbSet<CostCenter> CostCenters { get; set; }
    DbSet<CostCenterDepartment> CostCenterDepartments { get; set; }
    DbSet<CostCenterEmployee> CostCenterEmployees { get; set; }
    DbSet<CostDepartment> CostDepartments { get; set; }
    DbSet<CostSubDepartment> CostSubDepartments { get; set; }
    DbSet<CostType> CostTypes { get; set; }
    DbSet<Creditor> Creditors { get; set; }
    DbSet<CreditorCutOffSetting> CreditorCutOffSettings { get; set; }
    DbSet<CreditorFile> CreditorFiles { get; set; }
    DbSet<CreditorFileType> CreditorFileTypes { get; set; }
    DbSet<CreditorNotificationStatusUser> CreditorNotificationStatusUsers { get; set; }
    DbSet<CreditorStatus> CreditorStatuses { get; set; }
    DbSet<CreditorStatusHistory> CreditorStatusHistories { get; set; }
    DbSet<CreditorBankAccount> CreditorBankAccounts { get; set; }
    DbSet<DCAccount> DCAccounts { get; set; }
    DbSet<DCDepartment> DCDepartments { get; set; }
    DbSet<DCProduct> DCProducts { get; set; }
    DbSet<DCStockOnHand> DCStockOnHands { get; set; }
    DbSet<DistributionCenter> DistributionCenters { get; set; }
    DbSet<EmailHistory> EmailHistories { get; set; }
    DbSet<EmailHistoryCCEmail> EmailHistoryCCEmails { get; set; }
    DbSet<EmailHistoryTemplateVariable> EmailHistoryTemplateVariables { get; set; }
    DbSet<EmailTemplate> EmailTemplates { get; set; }
    DbSet<EmailTemplateHistory> EmailTemplateHistories { get; set; }
    DbSet<EmailTemplateVariableClaimNumber> EmailTemplateVariableClaimNumbers { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
    DbSet<EmployeeAsset> EmployeeAssets { get; set; }
    DbSet<EmployeeAssetHistory> EmployeeAssetHistories { get; set; }
    DbSet<EmployeeBadge> EmployeeBadges { get; set; }
    DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }
    DbSet<EmployeeBenefit> EmployeeBenefits { get; set; }
    DbSet<EmployeeCoolerBox> EmployeeCoolerBoxes { get; set; }
    DbSet<EmployeeCoolerBoxHistory> EmployeeCoolerBoxHistories { get; set; }
    DbSet<EmployeeDeduction> EmployeeDeductions { get; set; }
    DbSet<EmployeeDivision> EmployeeDivisions { get; set; }
    DbSet<EmployeeEmployeeDivision> EmployeeEmployeeDivisions { get; set; }
    DbSet<EmployeeDisciplinaryProcedure> EmployeeDisciplinaryProcedures { get; set; }
    DbSet<EmployeeExpenseClaim> EmployeeExpenseClaims { get; set; }
    DbSet<EmployeeFile> EmployeeFiles { get; set; }
    DbSet<EmployeeFileType> EmployeeFileTypes { get; set; }
    DbSet<EmployeeFuel> EmployeeFuels { get; set; }
    DbSet<EmployeeHealthCondition> EmployeeHealthConditions { get; set; }
    DbSet<EmployeeJobTitle> EmployeeJobTitles { get; set; }
    DbSet<EmployeeJobTitleTime> EmployeeJobTitleTimes { get; set; }
    DbSet<EmployeeJobTitleType> EmployeeJobTitleTypes { get; set; }
    DbSet<EmployeeKpi> EmployeeKpis { get; set; }
    DbSet<EmployeeKpiTier> EmployeeKpiTiers { get; set; }
    DbSet<EmployeeLeaveEntry> EmployeeLeaveEntries { get; set; }
    DbSet<EmployeeLoan> EmployeeLoans { get; set; }
    DbSet<EmployeePayRate> EmployeePayRates { get; set; }
    DbSet<EmployeePension> EmployeePensions { get; set; }
    DbSet<EmployeePopiConsent> EmployeePopiConsents { get; set; }
    DbSet<EmployeeQualification> EmployeeQualifications { get; set; }
    DbSet<EmployeeRecurringTransaction> EmployeeRecurringTransactions { get; set; }
    DbSet<EmployeeRecurringTransactionStatus> EmployeeRecurringTransactionStatuses { get; set; }
    DbSet<EmployeeRegionContact> EmployeeRegionContacts { get; set; }
    DbSet<EmployeeReinstatementHistory> EmployeeReinstatementHistories { get; set; }
    DbSet<EmployeeSkill> EmployeeSkills { get; set; }
    DbSet<EmployeeSkillsDevelopment> EmployeeSkillsDevelopment { get; set; }
    DbSet<EmployeeStoreCalendar> EmployeeStoreCalendars { get; set; }
    DbSet<EmployeeStoreCalendarGroup> EmployeeStoreCalendarGroups { get; set; }
    DbSet<EmployeeStoreCalendarPeriod> EmployeeStoreCalendarPeriods { get; set; }
    DbSet<EmployeeStoreCalendarYear> EmployeeStoreCalendarYears { get; set; }
    DbSet<EmployeeStoreCalendarStatus> EmployeeStoreCalendarStatuses { get; set; }
    DbSet<EmployeeStoreCalendarType> EmployeeStoreCalendarTypes { get; set; }
    DbSet<EmployeeStoreCalendarBlockDay> EmployeeStoreCalendarBlockDays { get; set; }
    DbSet<EmployeeStoreCalendarSurveyFormSubmission> EmployeeStoreCalendarSurveyFormSubmissions { get; set; }
    DbSet<EmployeeStoreCheckIn> EmployeeStoreCheckIns { get; set; }
    DbSet<EmployeeSuspension> EmployeeSuspensions { get; set; }
    DbSet<EmployeeTerminationHistory> EmployeeTerminationHistories { get; set; }
    DbSet<EmployeeTraining> EmployeeTrainings { get; set; }
    DbSet<EmployeeTrainingRecord> EmployeeTrainingRecords { get; set; }
    DbSet<EmployeeTransaction> EmployeeTransactions { get; set; }
    DbSet<EmployeeTransactionStatus> EmployeeTransactionStatuses { get; set; }
    DbSet<EmployeeTransactionType> EmployeeTransactionTypes { get; set; }
    DbSet<EmployeeTransactionTypeGroup> EmployeeTransactionTypeGroups { get; set; }
    DbSet<EmployeeTransactionRemunerationType> EmployeeTransactionRemunerationTypes { get; set; }
    DbSet<EmployeeType> EmployeeTypes { get; set; }
    DbSet<EmployeeVehicle> EmployeeVehicles { get; set; }
    DbSet<EmployeeVehicleHistory> EmployeeVehicleHistories { get; set; }
    DbSet<EmployeeWorkRole> EmployeeWorkRoles { get; set; }
    DbSet<EmployeeWorkRoleContact> EmployeeWorkRoleContacts { get; set; }
    DbSet<EngageMasterProduct> EngageMasterProducts { get; set; }
    DbSet<EngageRegionContact> EngageRegionContacts { get; set; }
    DbSet<EngageSubRegion> EngageSubRegions { get; set; }
    DbSet<EngageVariantProduct> EngageVariantProducts { get; set; }
    DbSet<EntityBlob> EntityBlobs { get; set; }
    DbSet<EntityContact> EntityContacts { get; set; }
    DbSet<EntityContactCommunicationType> EntityContactCommunicationTypes { get; set; }
    DbSet<EntityContactRegion> EntityContactRegions { get; set; }
    DbSet<ExpenseType> ExpenseTypes { get; set; }
    DbSet<ExternalUserType> ExternalUserTypes { get; set; }
    DbSet<FileContainer> FileContainers { get; set; }
    DbSet<FileType> FileTypes { get; set; }
    DbSet<FileUpload> FileUploads { get; set; }
    DbSet<GLAccount> GLAccounts { get; set; }
    DbSet<GLAccountType> GLAccountTypes { get; set; }
    DbSet<GLAdjustment> GLAdjustments { get; set; }
    DbSet<ImportFile> ImportFiles { get; set; }
    DbSet<ImportPromotionStore> ImportPromotionStores { get; set; }
    DbSet<ImportSurveyStore> ImportSurveyStores { get; set; }
    DbSet<Incident> Incidents { get; set; }
    DbSet<IncidentSku> IncidentSkus { get; set; }
    DbSet<IncidentSkuStatus> IncidentSkuStatuses { get; set; }
    DbSet<IncidentSkuType> IncidentSkuTypes { get; set; }
    DbSet<IncidentStatus> IncidentStatuses { get; set; }
    DbSet<IncidentType> IncidentTypes { get; set; }
    DbSet<Inventory> Inventories { get; set; }
    DbSet<InventoryGroup> InventoryGroups { get; set; }
    DbSet<InventoryPeriod> InventoryPeriods { get; set; }
    DbSet<InventoryStatus> InventoryStatuses { get; set; }
    DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    DbSet<InventoryTransactionStatus> InventoryTransactionStatuses { get; set; }
    DbSet<InventoryTransactionType> InventoryTransactionTypes { get; set; }
    DbSet<InventoryUnitType> InventoryUnitTypes { get; set; }
    DbSet<InventoryWarehouse> InventoryWarehouses { get; set; }
    DbSet<InventoryYear> InventoryYears { get; set; }
    DbSet<Location> Locations { get; set; }
    DbSet<Manufacturer> Manufacturers { get; set; }
    DbSet<Notification> Notifications { get; set; }
    DbSet<NotificationEmployee> NotificationEmployees { get; set; }
    DbSet<NotificationEmployeeJobTitle> NotificationEmployeeJobTitles { get; set; }
    DbSet<NotificationEngageRegion> NotificationEngageRegions { get; set; }
    DbSet<NotificationStore> NotificationStores { get; set; }
    DbSet<NotificationStoreFormat> NotificationStoreFormats { get; set; }
    DbSet<NotificationTarget> NotificationTargets { get; set; }
    DbSet<NPrinting> NPrintings { get; set; }
    DbSet<NPrintingBatch> NPrintingBatches { get; set; }
    DbSet<OrderStaging> OrderStagings { get; set; }
    DbSet<OrderStagingSku> OrderStagingSkus { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderSku> OrderSkus { get; set; }
    DbSet<OrderTemplate> OrderTemplates { get; set; }
    DbSet<OrderTemplateGroup> OrderTemplateGroups { get; set; }
    DbSet<OrderTemplateProduct> OrderTemplateProducts { get; set; }
    DbSet<OrderTemplateStatus> OrderTemplateStatuses { get; set; }
    DbSet<Organization> Organizations { get; set; }
    DbSet<OrganizationSetting> OrganizationSettings { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<PaymentArchive> PaymentArchives { get; set; }
    DbSet<PaymentBatch> PaymentBatches { get; set; }
    DbSet<PaymentBatchRegion> PaymentBatchRegions { get; set; }
    DbSet<PaymentLine> PaymentLines { get; set; }
    DbSet<PaymentNotificationStatusUser> PaymentNotificationStatusUsers { get; set; }
    DbSet<PaymentLineCostCenter> PaymentLineCostCenters { get; set; }
    DbSet<PaymentLineCostSubDepartment> PaymentLineCostSubDepartments { get; set; }
    DbSet<PaymentLineDivision> PaymentLineDivisions { get; set; }
    DbSet<PaymentLineEmployee> PaymentLineEmployees { get; set; }
    DbSet<PaymentLineFile> PaymentLineFiles { get; set; }
    DbSet<PaymentLineFileType> PaymentLineFileTypes { get; set; }
    DbSet<PaymentPeriod> PaymentPeriods { get; set; }
    DbSet<PaymentProof> PaymentProofs { get; set; }
    DbSet<PaymentProofPayment> PaymentProofPayments { get; set; }
    DbSet<PaymentStatus> PaymentStatuses { get; set; }
    DbSet<PaymentStatusHistory> PaymentStatusHistories { get; set; }
    DbSet<PaymentYear> PaymentYears { get; set; }
    DbSet<PayrollPeriod> PayrollPeriods { get; set; }
    DbSet<PayrollYear> PayrollYears { get; set; }
    DbSet<ProductAnalysis> ProductAnalyses { get; set; }
    DbSet<ProductFilterUpload> ProductFilterUploads { get; set; }
    DbSet<ProductFilter> ProductFilters { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductPrice> ProductPrices { get; set; }
    DbSet<ProductBrand> ProductBrands { get; set; }
    DbSet<ProductCategory> ProductCategories { get; set; }
    DbSet<ProductGroup> ProductGroups { get; set; }
    DbSet<ProductManufacturer> ProductManufacturers { get; set; }
    DbSet<ProductMaster> ProductMasters { get; set; }
    DbSet<ProductMasterColor> ProductMasterColors { get; set; }
    DbSet<ProductMasterSize> ProductMasterSizes { get; set; }
    DbSet<ProductMasterStatus> ProductMasterStatuses { get; set; }
    DbSet<ProductMasterSystemStatus> ProductMasterSystemStatuses { get; set; }
    DbSet<ProductModuleStatus> ProductModuleStatuses { get; set; }
    DbSet<ProductPackSizeType> ProductPackSizeTypes { get; set; }
    DbSet<ProductPeriod> ProductPeriods { get; set; }
    DbSet<ProductReason> ProductReasons { get; set; }
    DbSet<ProductSizeType> ProductSizeTypes { get; set; }
    DbSet<ProductSubCategory> ProductSubCategories { get; set; }
    DbSet<ProductSubGroup> ProductSubGroups { get; set; }
    DbSet<ProductSupplier> ProductSuppliers { get; set; }
    DbSet<ProductSystemStatus> ProductSystemStatuses { get; set; }
    DbSet<ProductTransaction> ProductTransactions { get; set; }
    DbSet<ProductTransactionStatus> ProductTransactionStatuses { get; set; }
    DbSet<ProductTransactionType> ProductTransactionTypes { get; set; }
    DbSet<ProductVendor> ProductVendors { get; set; }
    DbSet<ProductWarehouse> ProductWarehouses { get; set; }
    DbSet<ProductYear> ProductYears { get; set; }
    DbSet<ProductWarehouseSummary> ProductWarehouseSummaries { get; set; }
    DbSet<ProductOrder> ProductOrders { get; set; }
    DbSet<ProductOrderStatus> ProductOrderStatuses { get; set; }
    DbSet<ProductOrderType> ProductOrderTypes { get; set; }
    DbSet<ProductOrderLine> ProductOrderLines { get; set; }
    DbSet<ProductOrderLineStatus> ProductOrderLineStatuses { get; set; }
    DbSet<ProductOrderLineType> ProductOrderLineTypes { get; set; }
    DbSet<ProductOrderHistory> ProductOrderHistories { get; set; }
    DbSet<Project> Projects { get; set; }
    DbSet<ProjectCampaign> ProjectCampaigns { get; set; }
    DbSet<ProjectFile> ProjectFiles { get; set; }
    DbSet<ProjectFileType> ProjectFileTypes { get; set; }
    DbSet<ProjectNote> ProjectNotes { get; set; }
    DbSet<ProjectStatus> ProjectStatuses { get; set; }
    DbSet<ProjectType> ProjectTypes { get; set; }
    DbSet<ProjectPriority> ProjectPriorities { get; set; }
    DbSet<ProjectUser> ProjectUsers { get; set; }
    DbSet<ProjectStakeholder> ProjectStakeholders { get; set; }
    DbSet<ProjectStakeholderEmployeeRegionContact> ProjectStakeholderEmployeeRegionContacts { get; set; }
    DbSet<ProjectStakeholderStoreContact> ProjectStakeholderStoreContacts { get; set; }
    DbSet<ProjectStakeholderSupplierContact> ProjectStakeholderSupplierContacts { get; set; }
    DbSet<ProjectStakeholderUser> ProjectStakeholderUsers { get; set; }
    DbSet<ProjectStore> ProjectStores { get; set; }
    DbSet<ProjectTacOp> ProjectTacOps { get; set; }
    DbSet<ProjectTacOpRegion> ProjectTacOpRegions { get; set; }
    DbSet<ProjectProjectTag> ProjectProjectTags { get; set; }
    DbSet<ProjectProjectTagClaim> ProjectProjectTagClaims { get; set; }
    DbSet<ProjectProjectTagEmployeeJobTitle> ProjectProjectTagEmployeeJobTitles { get; set; }
    DbSet<ProjectProjectTagEngageRegion> ProjectProjectTagEngageRegions { get; set; }
    DbSet<ProjectProjectTagDCProduct> ProjectProjectTagDCProducts { get; set; }
    DbSet<ProjectProjectTagOrder> ProjectProjectTagOrders { get; set; }
    DbSet<ProjectProjectTagStore> ProjectProjectTagStores { get; set; }
    DbSet<ProjectProjectTagStoreAsset> ProjectProjectTagStoreAssets { get; set; }
    DbSet<ProjectProjectTagSupplier> ProjectProjectTagSuppliers { get; set; }
    DbSet<ProjectProjectTagUser> ProjectProjectTagUsers { get; set; }
    DbSet<ProjectTask> ProjectTasks { get; set; }
    DbSet<ProjectTaskStatus> ProjectTaskStatuses { get; set; }
    DbSet<ProjectTaskType> ProjectTaskTypes { get; set; }
    DbSet<ProjectTaskNote> ProjectTaskNotes { get; set; }
    DbSet<ProjectTaskPriority> ProjectTaskPriorities { get; set; }
    DbSet<ProjectTaskSeverity> ProjectTaskSeverities { get; set; }
    DbSet<ProjectTaskStakeholder> ProjectTaskStakeholders { get; set; }
    DbSet<ProjectTaskProjectStakeholderUser> ProjectTaskProjectStakeholderUsers { get; set; }
    DbSet<ProjectAssignee> ProjectAssignees { get; set; }
    DbSet<ProjectCategory> ProjectCategories { get; set; }
    DbSet<ProjectSubCategory> ProjectSubCategories { get; set; }
    DbSet<ProjectCategorySupplier> ProjectCategorySuppliers { get; set; }
    DbSet<ProjectComment> ProjectComments { get; set; }
    DbSet<ProjectDcProduct> ProjectDcProducts { get; set; }
    DbSet<ProjectExternalUser> ProjectExternalUsers { get; set; }
    DbSet<ProjectExternalUserCommunicationType> ProjectExternalUserCommunicationTypes { get; set; }
    DbSet<ProjectStatusHistory> projectStatusHistories { get; set; }
    DbSet<ProjectStoreAsset> ProjectStoreAssets { get; set; }
    DbSet<ProjectSubType> ProjectSubTypes { get; set; }
    DbSet<ProjectTaskAssignee> ProjectTaskAssignees { get; set; }
    DbSet<ProjectTaskComment> ProjectTaskComments { get; set; }
    DbSet<ProjectTaskStatusHistory> ProjectTaskStatusHistories { get; set; }
    DbSet<ProjectStakeholderExternalUser> ProjectStakeholderExternalUsers { get; set; }
    DbSet<ProjectSupplier> ProjectSuppliers { get; set; }
    DbSet<ProjectEngageBrand> ProjectEngageBrands { get; set; }
    DbSet<Promotion> Promotions { get; set; }
    DbSet<PromotionProduct> PromotionProducts { get; set; }
    DbSet<PromotionStore> PromotionStores { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<RoleUserGroup> RoleUserGroups { get; set; }
    DbSet<SupplierSalesLead> SupplierSalesLeads { get; set; }
    DbSet<Setting> Settings { get; set; }
    DbSet<SecurityOrganization> SecurityOrganizations { get; set; }
    DbSet<SecurityPermission> SecurityPermissions { get; set; }
    DbSet<SecurityRole> SecurityRoles { get; set; }
    DbSet<SecurityPermissionRole> SecurityPermissionRoles { get; set; }
    DbSet<SecurityRoleUser> SecurityRoleUsers { get; set; }
    DbSet<AnalysisPillarGroup> AnalysisPillarGroups { get; set; }
    DbSet<AnalysisPillarSubGroup> AnalysisPillarSubGroups { get; set; }
    DbSet<EvoLedger> EvoLedgers { get; set; }
    DbSet<SparAnalysisGroup> SparAnalysisGroups { get; set; }
    DbSet<SparProduct> SparProducts { get; set; }
    DbSet<SparProductStatus> SparProductStatuses { get; set; }
    DbSet<SparSource> SparSources { get; set; }
    DbSet<SparSubProduct> SparSubProducts { get; set; }
    DbSet<SparSubProductStatus> SparSubProductStatuses { get; set; }
    DbSet<SparSystemStatus> SparSystemStatuses { get; set; }
    DbSet<SparUnitType> SparUnitTypes { get; set; }
    DbSet<Stakeholder> Stakeholders { get; set; }
    DbSet<StatsStoresByRegion> StatsStoresByRegions { get; set; }
    DbSet<StatsOrdersByRegion> StatsOrdersByRegions { get; set; }
    DbSet<Store> Stores { get; set; }
    DbSet<StoreAssetCondition> StoreAssetConditions { get; set; }
    DbSet<StoreAsset> StoreAssets { get; set; }
    DbSet<StoreAssetSubType> StoreAssetSubTypes { get; set; }
    DbSet<StoreAssetStoreAssetTypeContact> StoreAssetStoreAssetTypeContacts { get; set; }
    DbSet<StoreAssetTypeStoreAssetTypeContact> StoreAssetTypeStoreAssetTypeContacts { get; set; }
    DbSet<StoreAssetTypeContact> StoreAssetTypeContacts { get; set; }
    DbSet<StoreAssetOwnerStoreAssetType> StoreAssetOwnerStoreAssetTypes { get; set; }
    DbSet<StoreAssetTypeStoreAssetSubType> StoreAssetTypeStoreAssetSubTypes { get; set; }
    DbSet<StoreAssetStatus> StoreAssetStatuses { get; set; }
    DbSet<StoreAssetFile> StoreAssetFiles { get; set; }
    DbSet<StoreAssetFileType> StoreAssetFileTypes { get; set; }
    DbSet<StoreAssetBlob> StoreAssetBlobs { get; set; }
    DbSet<StoreBankDetail> StoreBankDetails { get; set; }
    DbSet<StoreConceptAttribute> StoreConceptAttributes { get; set; }
    DbSet<StoreConceptAttributeOption> StoreConceptAttributeOptions { get; set; }
    DbSet<StoreConceptAttributeValue> StoreConceptAttributeValues { get; set; }
    DbSet<StoreContact> StoreContacts { get; set; }
    DbSet<StoreCycle> StoreCycles { get; set; }
    DbSet<StoreFilter> StoreFilters { get; set; }
    DbSet<StoreFilterUpload> StoreFilterUploads { get; set; }
    DbSet<StorePOS> StorePOS { get; set; }
    DbSet<StorePOSFreezerQuestion> StorePOSFreezerQuestions { get; set; }
    DbSet<StorePOSQuestion> StorePOSQuestions { get; set; }
    DbSet<StoreOwner> StoreOwners { get; set; }
    DbSet<SubContractorBrand> SubContractorBrands { get; set; }
    DbSet<SubWarehouse> SubWarehouses { get; set; }
    DbSet<SupplierAllowance> SupplierAllowances { get; set; }
    DbSet<SupplierAllowanceContract> SupplierAllowanceContracts { get; set; }
    DbSet<SupplierAllowanceSubContract> SupplierAllowanceSubContracts { get; set; }
    DbSet<Supplier> Suppliers { get; set; }
    DbSet<SupplierContact> SupplierContacts { get; set; }
    DbSet<SupplierSetting> SupplierSettings { get; set; }
    DbSet<SupplierBudget> SupplierBudgets { get; set; }
    DbSet<SupplierBudgetType> SupplierBudgetTypes { get; set; }
    DbSet<SupplierBudgetVersion> SupplierBudgetVersions { get; set; }
    DbSet<SupplierBudgetVersionType> SupplierBudgetVersionTypes { get; set; }
    DbSet<SupplierContract> SupplierContracts { get; set; }
    DbSet<SupplierContractDetail> SupplierContractDetails { get; set; }
    DbSet<SupplierContractDetailType> SupplierContractDetailTypes { get; set; }
    DbSet<SupplierContractGroup> SupplierContractGroups { get; set; }
    DbSet<SupplierContractSubGroup> SupplierContractSubGroups { get; set; }
    DbSet<SupplierContractType> SupplierContractTypes { get; set; }
    DbSet<SupplierYear> SupplierYears { get; set; }
    DbSet<SupplierPeriod> SupplierPeriods { get; set; }
    DbSet<SupplierContractAmount> SupplierContractAmounts { get; set; }
    DbSet<SupplierContractAmountType> SupplierContractAmountTypes { get; set; }
    DbSet<SupplierContractSplit> SupplierContractSplits { get; set; }
    DbSet<SupplierSubContract> SupplierSubContracts { get; set; }
    DbSet<SupplierSubContractDetail> SupplierSubContractDetails { get; set; }
    DbSet<SupplierSubContractDetailType> SupplierSubContractDetailTypes { get; set; }
    DbSet<SupplierSubContractType> SupplierSubContractTypes { get; set; }
    DbSet<SupplierSubRegion> SupplierSubRegions { get; set; }
    DbSet<SurveyForm> SurveyForms { get; set; }
    DbSet<SurveyFormAnswer> SurveyFormAnswers { get; set; }
    DbSet<SurveyFormAnswerOption> SurveyFormAnswerOptions { get; set; }
    DbSet<SurveyFormAnswerHistory> SurveyFormAnswerHistories { get; set; }
    DbSet<SurveyFormAnswerOptionHistory> SurveyFormAnswerOptionHistories { get; set; }
    DbSet<SurveyFormOption> SurveyFormOptions { get; set; }
    DbSet<SurveyFormProduct> SurveyFormProducts { get; set; }
    DbSet<SurveyFormQuestion> SurveyFormQuestions { get; set; }
    DbSet<SurveyFormQuestionGroup> SurveyFormQuestionGroups { get; set; }
    DbSet<SurveyFormQuestionGroupProduct> SurveyFormQuestionGroupProducts { get; set; }
    DbSet<SurveyFormQuestionOption> SurveyFormQuestionOptions { get; set; }
    DbSet<SurveyFormQuestionProduct> SurveyFormQuestionProducts { get; set; }
    DbSet<SurveyFormQuestionReason> SurveyFormQuestionReasons { get; set; }
    DbSet<SurveyFormQuestionType> SurveyFormQuestionTypes { get; set; }
    DbSet<SurveyFormQuestionValueComparisonTargetType> SurveyFormQuestionValueComparisonTargetTypes { get; set; }
    DbSet<SurveyFormQuestionValueComparisonOperation> SurveyFormQuestionValueComparisonOperations { get; set; }
    DbSet<SurveyFormReason> SurveyFormReasons { get; set; }
    DbSet<SurveyFormSubmission> SurveyFormSubmissions { get; set; }
    DbSet<SurveyFormType> SurveyFormTypes { get; set; }
    DbSet<SurveyFormTarget> SurveyFormTargets { get; set; }
    DbSet<SurveyFormEmployee> SurveyFormEmployees { get; set; }
    DbSet<SurveyFormEmployeeDivision> SurveyFormEmployeeDivisions { get; set; }
    DbSet<SurveyFormEmployeeJobTitle> SurveyFormEmployeeJobTitles { get; set; }
    DbSet<SurveyFormEmployeeEngageRegion> SurveyFormEmployeeEngageRegions { get; set; }
    DbSet<SurveyFormExcludedEmployee> SurveyFormExcludedEmployees { get; set; }
    DbSet<SurveyFormExcludedStore> SurveyFormExcludedStores { get; set; }
    DbSet<SurveyFormEngageDepartment> SurveyFormEngageDepartments { get; set; }
    DbSet<SurveyFormStore> SurveyFormStores { get; set; }
    DbSet<SurveyFormStoreEngageRegion> SurveyFormStoreEngageRegions { get; set; }
    DbSet<SurveyFormStoreCluster> SurveyFormStoreClusters { get; set; }
    DbSet<SurveyFormStoreFormat> SurveyFormStoreFormats { get; set; }
    DbSet<SurveyFormStoreLSM> SurveyFormStoreLSMs { get; set; }
    DbSet<SurveyFormStoreType> SurveyFormStoreTypes { get; set; }
    DbSet<Survey> Surveys { get; set; }
    DbSet<SurveyAnswer> SurveyAnswers { get; set; }
    DbSet<SurveyAnswerOption> SurveyAnswerOptions { get; set; }
    DbSet<SurveyAnswerPhoto> SurveyAnswerPhotos { get; set; }
    DbSet<SurveyEmployeeJobTitleTarget> SurveyEmployeeJobTitleTargets { get; set; }
    DbSet<SurveyEmployeeTarget> SurveyEmployeeTargets { get; set; }
    DbSet<SurveyEngageRegionTarget> SurveyEngageRegionTargets { get; set; }
    DbSet<SurveyInstance> SurveyInstances { get; set; }
    DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    DbSet<SurveyQuestionRule> SurveyQuestionRules { get; set; }
    DbSet<SurveyStoreFormatTarget> SurveyStoreFormatTargets { get; set; }
    DbSet<SurveyStoreTarget> SurveyStoreTargets { get; set; }
    DbSet<SurveyQuestionOption> SurveyQuestionOptions { get; set; }
    DbSet<SurveyTarget> SurveyTargets { get; set; }
    DbSet<Targeting> Targetings { get; set; }
    DbSet<TargetStrategy> TargetStrategies { get; set; }
    DbSet<TenantSetting> TenantSettings { get; set; }
    DbSet<TrainingFile> TrainingFiles { get; set; }
    DbSet<TrainingFileType> TrainingFileTypes { get; set; }
    DbSet<TrainingProvider> TrainingProviders { get; set; }
    DbSet<TrainingType> TrainingTypes { get; set; }
    DbSet<TrainingCategory> TrainingCategories { get; set; }
    DbSet<Training> Trainings { get; set; }
    DbSet<TrainingDuration> TrainingDurations { get; set; }
    DbSet<TrainingFacilitator> TrainingFacilitators { get; set; }
    DbSet<TrainingPeriod> TrainingPeriods { get; set; }
    DbSet<TrainingYear> TrainingYears { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<UserCommunicationType> UserCommunicationTypes { get; set; }
    DbSet<UserEngageSubGroup> UserEngageSubGroups { get; set; }
    DbSet<UserEntity> UserEntities { get; set; }
    DbSet<UserEntityRecord> UserEntityRecords { get; set; }
    DbSet<UserGroup> UserGroups { get; set; }
    DbSet<UserOrganization> UserOrganizations { get; set; }
    DbSet<UserRegion> UserRegions { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<UserPermission> UserPermissions { get; set; }
    DbSet<UserOrganizationRole> UserOrganizationRoles { get; set; }
    DbSet<UserRolePermission> UserRolePermissions { get; set; }
    DbSet<UserStore> UserStores { get; set; }
    DbSet<Vacancy> Vacancies { get; set; }
    DbSet<Voucher> Vouchers { get; set; }
    DbSet<VoucherDetail> VoucherDetails { get; set; }
    DbSet<Vat> Vat { get; set; }
    DbSet<VatPeriod> VatPeriods { get; set; }
    DbSet<Vendor> Vendors { get; set; }
    DbSet<Warehouse> Warehouses { get; set; }
    DbSet<WebEvent> WebEvents { get; set; }
    DbSet<WebFile> WebFiles { get; set; }
    DbSet<WebFileCategory> WebFileCategories { get; set; }
    DbSet<WebFileEmployee> WebFileEmployees { get; set; }
    DbSet<WebFileEmployeeJobTitle> WebFileEmployeeJobTitles { get; set; }
    DbSet<WebFileEmployeeDivision> WebFileEmployeeDivisions { get; set; }
    DbSet<WebFileEngageDepartment> WebFileEngageDepartments { get; set; }
    DbSet<WebFileEngageRegion> WebFileEngageRegions { get; set; }
    DbSet<WebFileGroup> WebFileGroups { get; set; }
    DbSet<WebFileStore> WebFileStores { get; set; }
    DbSet<WebFileStoreFormat> WebFileStoreFormats { get; set; }
    DbSet<WebFileTarget> WebFileTargets { get; set; }
    DbSet<WebPage> WebPages { get; set; }
    DbSet<WebPageEmployee> WebPageEmployees { get; set; }
    DbSet<WhatsAppHistory> WhatsAppHistories { get; set; }
    #endregion

    #region File Entities

    // Deprecate Use JSON file columns 
    DbSet<EmployeeQualificationFile> EmployeeQualificationFiles { get; set; }
    DbSet<EmployeeBankDetailFile> EmployeeBankDetailFiles { get; set; }
    DbSet<EmployeeSkillFile> EmployeeSkillFiles { get; set; }
    DbSet<EmployeeCoolerBoxFile> EmployeeCoolerBoxFiles { get; set; }
    #endregion

    #region Link Entities
    DbSet<BudgetYearVersion> BudgetYearVersions { get; set; }
    DbSet<ClaimClassificationType> ClaimClassificationTypes { get; set; }
    DbSet<ClaimFloatClaim> ClaimFloatClaims { get; set; }
    DbSet<ClaimTypeReportType> ClaimTypeReportTypes { get; set; }
    DbSet<DCDept> DCDepts { get; set; }
    DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
    DbSet<EmployeeNotification> EmployeeNotifications { get; set; }
    DbSet<EmployeeRegion> EmployeeRegions { get; set; }
    DbSet<EmployeeEmployeeHealthCondition> EmployeeEmployeeHealthConditions { get; set; }
    DbSet<EmployeeEmployeeJobTitle> EmployeeEmployeeJobTitles { get; set; }
    DbSet<EmployeeJobTitleUserGroup> EmployeeJobTitleUserGroups { get; set; }
    DbSet<EmployeeReport> EmployeeReports { get; set; }
    DbSet<EmployeeStore> EmployeeStores { get; set; }
    DbSet<EmployeeStoreArchive> EmployeeStoreArchives { get; set; }
    DbSet<EngageRegionClaimManager> EngageRegionClaimManagers { get; set; }
    DbSet<EngageProductTag> EngageProductTags { get; set; }
    DbSet<EngageSubGroupEngageBrand> EngageSubGroupEngageBrands { get; set; }
    DbSet<NotificationEmployeeRead> NotificationEmployeeReads { get; set; }
    DbSet<NotificationRegion> NotificationRegions { get; set; }
    DbSet<OrderEngageDepartment> OrderEngageDepartments { get; set; }
    DbSet<ProductWarehouseRegion> ProductWarehouseRegions { get; set; }
    DbSet<StoreStoreConcept> StoreStoreConcepts { get; set; }
    DbSet<StoreConceptLevel> StoreConceptLevels { get; set; }
    DbSet<StoreStoreDepartment> StoreStoreDepartments { get; set; }
    DbSet<SupplierClaimAccountManager> SupplierClaimAccountManagers { get; set; }
    DbSet<SupplierClaimClassification> SupplierClaimClassifications { get; set; }
    DbSet<SupplierEngageBrand> SupplierEngageBrands { get; set; }
    DbSet<SupplierProduct> SupplierProducts { get; set; }
    DbSet<SupplierStore> SupplierStores { get; set; }
    DbSet<SupplierSupplierType> SupplierSupplierTypes { get; set; }
    DbSet<EngageSubGroupSupplier> EngageSubGroupSuppliers { get; set; }
    DbSet<SurveyEmployee> SurveyEmployees { get; set; }
    DbSet<SurveyEngageRegion> SurveyEngageRegions { get; set; }
    DbSet<SurveyQuestionFalseReason> SurveyQuestionFalseReasons { get; set; }
    DbSet<SurveyStore> SurveyStores { get; set; }
    DbSet<SurveyStoreFormat> SurveyStoreFormats { get; set; }
    DbSet<UserUserGroup> UserUserGroups { get; set; }
    DbSet<EmployeeEmployeeBadge> EmployeeEmployeeBadges { get; set; }
    DbSet<EmployeeEmployeeKpi> EmployeeEmployeeKpis { get; set; }
    DbSet<EmployeeStoreKpi> EmployeeStoreKpis { get; set; }
    DbSet<EmployeeKpiScore> EmployeeKpiScores { get; set; }
    DbSet<EmployeeStoreKpiScore> EmployeeStoreKpiScores { get; set; }
    DbSet<NotificationNotificationChannel> NotificationNotificationChannels { get; set; }
    DbSet<StoreConceptAttributeStoreAsset> StoreConceptAttributeStoreAssets { get; set; }
    DbSet<StoreStoreConceptPerformance> StoreStoreConceptPerformances { get; set; }

    #endregion

    #region Option Entities

    public DbSet<ClientType> ClientTypes { get; set; }

    // Option Types
    DbSet<OptionTypeGroup> OptionTypeGroups { get; set; }
    DbSet<OptionType> OptionTypes { get; set; }

    // Assets
    DbSet<AssetOwner> AssetOwners { get; set; }
    DbSet<AssetStatus> AssetStatuses { get; set; }

    // Attachments
    DbSet<AttachmentType> AttachmentTypes { get; set; }

    // Bank Accounts 
    DbSet<BankAccountOwner> BankAccountOwners { get; set; }
    DbSet<BankAccountType> BankAccountTypes { get; set; }
    DbSet<BankPaymentMethod> BankPaymentMethods { get; set; }
    DbSet<BankName> BankNames { get; set; }

    // CRM Types
    DbSet<ContactType> ContactTypes { get; set; }
    DbSet<EventType> EventTypes { get; set; }

    // Location Types
    DbSet<LocationType> LocationTypes { get; set; }
    DbSet<EngageRegion> EngageRegions { get; set; }
    DbSet<EngageLocation> EngageLocations { get; set; }

    // Employee
    DbSet<EmployeeAssetBrand> EmployeeAssetBrands { get; set; }
    DbSet<EmployeeAssetType> EmployeeAssetTypes { get; set; }
    DbSet<EmployeeBadgeType> EmployeeBadgeTypes { get; set; }
    DbSet<EmployeeKpiType> EmployeeKpiTypes { get; set; }

    DbSet<EmployeeCoolerBoxCondition> EmployeeCoolerBoxConditions { get; set; }
    DbSet<EmployeeState> EmployeeStates { get; set; }
    DbSet<EmployeeIncentiveTier> EmployeeIncentiveTiers { get; set; }
    DbSet<EmployeeLanguage> EmployeeLanguages { get; set; }
    DbSet<EmployeePaymentType> EmployeePaymentTypes { get; set; }
    DbSet<EmployeePensionScheme> EmployeePensionSchemes { get; set; }
    DbSet<EmployeePensionCategory> EmployeePensionCategories { get; set; }
    DbSet<EmployeePensionContributionPercentage> EmployeePensionContributionPercentages { get; set; }
    DbSet<EmployeeFuelExpenseType> EmployeeFuelExpenseTypes { get; set; }
    DbSet<LeaveType> LeaveTypes { get; set; }
    DbSet<BenefitType> BenefitTypes { get; set; }
    DbSet<DeductionType> DeductionTypes { get; set; }
    DbSet<DeductionCycleType> DeductionCycleTypes { get; set; }
    DbSet<EmploymentType> EmploymentTypes { get; set; }
    DbSet<EmployeeDisabledType> EmployeeDisabledTypes { get; set; }
    DbSet<EmployeeIdentificationType> EmployeeIdentificationTypes { get; set; }
    DbSet<EmployeeNationality> EmployeeNationalities { get; set; }
    DbSet<EmployeePersonType> EmployeePersonTypes { get; set; }
    DbSet<EmployeeSDLExemption> EmployeeSDLExemptions { get; set; }
    DbSet<EmployeeTaxStatus> EmployeeTaxStatuses { get; set; }
    DbSet<EmployeeUIFExemption> EmployeeUIFExemptions { get; set; }
    DbSet<EmployeeDefaultPayslip> EmployeeDefaultPayslips { get; set; }
    DbSet<EmployeeReinstatementReason> EmployeeReinstatementReasons { get; set; }
    DbSet<EmployeeSuspensionReason> EmployeeSuspensionReasons { get; set; }
    DbSet<EmployeeTerminationReason> EmployeeTerminationReasons { get; set; }
    DbSet<EmployeeStandardIndustryGroupCode> EmployeeStandardIndustryGroupCodes { get; set; }
    DbSet<EmployeeStandardIndustryCode> EmployeeStandardIndustryCodes { get; set; }
    DbSet<EmploymentAction> EmploymentActions { get; set; }

    DbSet<ExpenseClaimStatus> ExpenseClaimStatuses { get; set; }
    DbSet<WorkRoleStatus> WorkRoleStatuses { get; set; }

    DbSet<Grade> Grades { get; set; }
    DbSet<SkillCategory> SkillCategories { get; set; }
    DbSet<Proficiency> Proficiencies { get; set; }
    DbSet<Experience> Experiences { get; set; }
    DbSet<EducationLevel> EducationLevels { get; set; }
    DbSet<InstitutionType> InstitutionTypes { get; set; }
    DbSet<EmployeeTrainingStatus> EmployeeTrainingStatuses { get; set; }

    DbSet<MaritalStatus> MaritalStatuses { get; set; }
    DbSet<Province> Provinces { get; set; }
    DbSet<Race> Races { get; set; }
    DbSet<Gender> Genders { get; set; }
    DbSet<NextOfKinType> NextOfKinTypes { get; set; }
    DbSet<Title> Titles { get; set; }
    DbSet<UniformSize> UniformSizes { get; set; }

    DbSet<VehicleBrand> VehicleBrands { get; set; }
    DbSet<VehicleType> VehicleTypes { get; set; }
    // Store 
    DbSet<StoreAssetOwner> StoreAssetOwners { get; set; }
    DbSet<StoreOwnerType> StoreOwnerTypes { get; set; }
    DbSet<StoreAssetType> StoreAssetTypes { get; set; }
    DbSet<StoreDepartment> StoreDepartments { get; set; }
    DbSet<StoreCycleOperation> StoreCycleOperations { get; set; }
    DbSet<StoreConcept> StoreConcepts { get; set; }
    DbSet<StoreConceptType> StoreConceptTypes { get; set; }
    DbSet<StoreConceptAttributeType> StoreConceptAttributeTypes { get; set; }
    DbSet<FrequencyType> FrequencyTypes { get; set; }
    DbSet<StoreClaimType> StoreClaimTypes { get; set; }
    DbSet<StoreGroup> StoreGroups { get; set; }
    DbSet<StoreType> StoreTypes { get; set; }
    DbSet<StoreFormat> StoreFormats { get; set; }
    DbSet<StoreCluster> StoreClusters { get; set; }
    DbSet<StoreLSM> StoreLSMs { get; set; }
    DbSet<StoreMediaGroup> StoreMediaGroups { get; set; }
    DbSet<StoreSparRegion> StoreSparRegions { get; set; }
    DbSet<StorePOSType> StorePOSTypes { get; set; }
    DbSet<StorePOSFreezerType> StorePOSFreezerTypes { get; set; }

    // Products
    DbSet<DCProductClass> DCProductClasses { get; set; }
    DbSet<ProductClassification> ProductClassifications { get; set; }

    // Warehouse
    DbSet<WarehouseType> WarehouseTypes { get; set; }

    // Supplier
    DbSet<SupplierType> SupplierTypes { get; set; }
    DbSet<SupplierGroup> SupplierGroups { get; set; }
    DbSet<SupplierRegion> SupplierRegions { get; set; }

    // Product
    DbSet<UnitType> UnitTypes { get; set; }
    DbSet<EngageTag> EngageTags { get; set; }
    DbSet<EngageDepartmentGroup> EngageDepartmentGroups { get; set; }
    DbSet<EngageDepartment> EngageDepartments { get; set; }
    DbSet<EngageDepartmentCategory> EngageDepartmentCategories { get; set; }
    DbSet<EngageGroup> EngageGroups { get; set; }
    DbSet<EngageSubGroup> EngageSubGroups { get; set; }
    DbSet<EngageCategory> EngageCategories { get; set; }
    DbSet<EngageSubCategory> EngageSubCategories { get; set; }
    DbSet<EngageBrand> EngageBrands { get; set; }
    DbSet<ProductActiveStatus> ProductActiveStatuses { get; set; }
    DbSet<ProductStatus> ProductStatuses { get; set; }
    DbSet<ProductWarehouseStatus> ProductWarehouseStatuses { get; set; }
    DbSet<ProductAnalysisGroup> ProductAnalysisGroups { get; set; }
    DbSet<ProductAnalysisDivision> ProductAnalysisDivisions { get; set; }

    // Notification
    DbSet<NotificationType> NotificationTypes { get; set; }
    DbSet<NotificationCategory> NotificationCategories { get; set; }
    DbSet<NotificationChannel> NotificationChannels { get; set; }

    // Order
    DbSet<OrderType> OrderTypes { get; set; }
    DbSet<OrderSkuType> OrderSkuTypes { get; set; }
    DbSet<OrderStatus> OrderStatuses { get; set; }
    DbSet<OrderSkuStatus> OrderSkuStatuses { get; set; }
    DbSet<OrderQuantityType> OrderQuantityTypes { get; set; }

    // Surveys
    DbSet<SurveyType> SurveyTypes { get; set; }
    DbSet<QuestionType> QuestionTypes { get; set; }
    DbSet<QuestionFalseReason> QuestionFalseReasons { get; set; }

    // Financial Reporting
    DbSet<BudgetVersion> BudgetVersions { get; set; }
    DbSet<BudgetType> BudgetTypes { get; set; }
    DbSet<GLAdjustmentType> GLAdjustmentTypes { get; set; }

    //Claims     
    DbSet<ClaimStatus> ClaimStatuses { get; set; }
    DbSet<ClaimSupplierStatus> ClaimSupplierStatuses { get; set; }
    DbSet<ClaimSkuStatus> ClaimSkuStatuses { get; set; }
    DbSet<EntityContactType> EntityContactTypes { get; set; }
    DbSet<ClaimQuantityType> ClaimQuantityTypes { get; set; }
    DbSet<ClaimRejectedReason> ClaimRejectedReasons { get; set; }
    DbSet<ClaimPendingReason> ClaimPendingReasons { get; set; }
    DbSet<ClaimReportType> ClaimReportTypes { get; set; }
    DbSet<EmailTemplateType> EmailTemplateTypes { get; set; }
    DbSet<EmailType> EmailTypes { get; set; }
    // Promotions
    DbSet<PromotionType> PromotionTypes { get; set; }
    DbSet<PromotionProductType> PromotionProductTypes { get; set; }

    DbSet<EmployeePayRateFrequency> EmployeePayRateFrequencies { get; set; }
    DbSet<EmployeePayRatePackage> EmployeePayRatePackages { get; set; }

    // Vouchers
    DbSet<VoucherType> VoucherTypes { get; set; }
    DbSet<VoucherStatus> VoucherStatuses { get; set; }
    DbSet<VoucherDetailStatus> VoucherDetailStatuses { get; set; }


    DbSet<WebEventType> WebEventTypes { get; set; }

    #endregion

    #region Views
    DbSet<StatsByEngageRegionView> StatsByEngageRegionView { get; set; }
    DbSet<SurveysByEmployeeStoreView> SurveysByEmployeeStoreView { get; set; }
    DbSet<SurveysByEmployeePerRegionView2> SurveysByEmployeePerRegionView2 { get; set; }
    DbSet<SurveysByEmployeePerStoreView> SurveysByEmployeePerStoreView { get; set; }
    DbSet<SurveysByEmployeePerStoreView_> SurveysByEmployeePerStoreView_ { get; set; }
    DbSet<SurveysByEmployeePerStoreFormatView> SurveysByEmployeePerStoreFormatView { get; set; }
    DbSet<SurveysByEmployeePerRegionView> SurveysByEmployeePerRegionView { get; set; }
    DbSet<SurveysByEmployeeSubGroupPerStoreView> SurveysByEmployeeSubGroupPerStoreView { get; set; }
    DbSet<SurveysByEmployeeSubGroupPerStoreFormatView> SurveysByEmployeeSubGroupPerStoreFormatView { get; set; }
    DbSet<SurveysByEmployeeSubGroupPerRegionView> SurveysByEmployeeSubGroupPerRegionView { get; set; }

    #endregion

    #region Auditing
    DbSet<AuditEntry> AuditEntries { get; set; }
    DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }
    #endregion

    Task<OperationStatus> SaveChangesAsync(CancellationToken cancellationToken);
}
