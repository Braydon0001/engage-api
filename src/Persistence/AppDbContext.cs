using Engage.Domain.Entities.Json;
using Engage.Domain.Entities.Settings;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Engage.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly IUserService _user;
    private readonly IDateTimeService _dateTime;
    public ITenantInfo TenantInfo { get; }
    public TenantMismatchMode TenantMismatchMode { get; }
    public TenantNotSetMode TenantNotSetMode { get; }

    public AppDbContext(DbContextOptions<AppDbContext> options, IUserService user, IDateTimeService dateTime, IMultiTenantContextAccessor multiTenantContextAccessor) : base(options)
    {
        _user = user;
        _dateTime = dateTime;
        TenantInfo = multiTenantContextAccessor.MultiTenantContext?.TenantInfo;

        AuditManager.DefaultConfiguration
                .AuditEntryFactory = args =>
                new AuditEntityIdEntry() { EntityID = 0 };

        AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
        {
            (context as AppDbContext).AuditEntries.AddRange(audit.Entries);
            (context as AppDbContext).AuditEntries.AddRange(audit.Entries.Cast<AuditEntityIdEntry>());
        };

        //Enities
        //AuditManager.DefaultConfiguration.Exclude(x => true); // Exclude ALL - No table will be audited
        //AuditManager.DefaultConfiguration.Exclude<ClaimSku>(); // Exclude ClaimSku
        //AuditManager.DefaultConfiguration.Include<ClaimSku>(); // Include ClaimSku - Only ClaimSku will be audited

        //Properties
        //AuditManager.DefaultConfiguration.ExcludeProperty(); // Exclude ALL - No property will be audited
        //AuditManager.DefaultConfiguration.IncludeProperty<ClaimSku>(); // IncludePropery ClaimSku - Only ClaimSku Properties will be audited
        //AuditManager.DefaultConfiguration.ExcludeProperty<ClaimSku>(x => new { x.LastModified, x.Created }); // Exclude LastModified and Created properties from ClaimSku audit
    }

    #region Entities
    public DbSet<ApiKey> ApiKeys { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<BudgetPeriod> BudgetPeriods { get; set; }
    public DbSet<BudgetYear> BudgetYears { get; set; }
    public DbSet<CategoryFile> CategoryFiles { get; set; }
    public DbSet<CategoryFileTarget> CategoryFileTargets { get; set; }
    public DbSet<CategoryFileEmployeeJobTitle> CategoryFileEmployeeJobTitles { get; set; }
    public DbSet<CategoryFileEngageRegion> CategoryFileEngageRegions { get; set; }
    public DbSet<CategoryFileEmployee> CategoryFileEmployees { get; set; }
    public DbSet<CategoryFileEngageSubGroup> CategoryFileEngageSubGroups { get; set; }
    public DbSet<CategoryFileStore> CategoryFileStores { get; set; }
    public DbSet<CategoryFileStoreFormat> CategoryFileStoreFormats { get; set; }
    public DbSet<CategoryFileType> CategoryFileTypes { get; set; }
    public DbSet<CategoryFileCategoryGroup> CategoryFileCategoryGroups { get; set; }
    public DbSet<CategoryGroup> CategoryGroups { get; set; }
    public DbSet<CategoryStoreGroup> CategoryStoreGroups { get; set; }
    public DbSet<CategorySubGroup> CategorySubGroups { get; set; }
    public DbSet<CategoryTarget> CategoryTargets { get; set; }
    public DbSet<CategoryTargetType> CategoryTargetTypes { get; set; }
    public DbSet<CategoryTargetStore> CategoryTargetStores { get; set; }
    public DbSet<CategoryTargetAnswer> CategoryTargetAnswers { get; set; }
    public DbSet<CategoryTargetAnswerHistory> CategoryTargetAnswerHistories { get; set; }
    public DbSet<Claim> Claims { get; set; }
    public DbSet<ClaimBatch> ClaimBatches { get; set; }
    public DbSet<ClaimBatchDetail> ClaimBatchDetails { get; set; }
    public DbSet<ClaimBlob> ClaimBlobs { get; set; }
    public DbSet<ClaimClassification> ClaimClassifications { get; set; }
    public DbSet<ClaimFloat> ClaimFloats { get; set; }
    public DbSet<ClaimFloatTopUpHistory> ClaimFloatTopUpHistories { get; set; }
    public DbSet<ClaimHistory> ClaimHistories { get; set; }
    public DbSet<ClaimHistoryHeader> ClaimHistoryHeaders { get; set; }
    public DbSet<ClaimNotificationUser> ClaimNotificationUsers { get; set; }
    public DbSet<ClaimPeriod> ClaimPeriods { get; set; }
    public DbSet<ClaimSku> ClaimSkus { get; set; }
    public DbSet<ClaimSkuType> ClaimSkuTypes { get; set; }
    public DbSet<ClaimStatusUser> ClaimStatusUsers { get; set; }
    public DbSet<ClaimType> ClaimTypes { get; set; }
    public DbSet<ClaimYear> ClaimYears { get; set; }
    public DbSet<CommunicationHistory> CommunicationHistories { get; set; }
    public DbSet<CommunicationHistoryClaim> CommunicationHistoryClaims { get; set; }
    public DbSet<CommunicationHistoryClaimFloat> CommunicationHistoryClaimFloats { get; set; }
    public DbSet<CommunicationHistoryEmployee> CommunicationHistoryEmployees { get; set; }
    public DbSet<CommunicationHistoryEmployeeStoreCalendar> CommunicationHistoryEmployeeStoreCalendars { get; set; }
    public DbSet<CommunicationHistoryOrder> CommunicationHistoryOrders { get; set; }
    public DbSet<CommunicationHistoryProject> CommunicationHistoryProjects { get; set; }
    public DbSet<CommunicationHistoryStore> CommunicationHistoryStores { get; set; }
    public DbSet<CommunicationTemplate> CommunicationTemplates { get; set; }
    public DbSet<CommunicationTemplateType> CommunicationTemplateTypes { get; set; }
    public DbSet<CommunicationType> CommunicationTypes { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactItem> ContactItems { get; set; }
    public DbSet<ContactEvent> ContactEvents { get; set; }
    public DbSet<CostCenter> CostCenters { get; set; }
    public DbSet<CostCenterDepartment> CostCenterDepartments { get; set; }
    public DbSet<CostCenterEmployee> CostCenterEmployees { get; set; }
    public DbSet<CostDepartment> CostDepartments { get; set; }
    public DbSet<CostSubDepartment> CostSubDepartments { get; set; }
    public DbSet<CostType> CostTypes { get; set; }
    public DbSet<Creditor> Creditors { get; set; }
    public DbSet<CreditorCutOffSetting> CreditorCutOffSettings { get; set; }
    public DbSet<CreditorFile> CreditorFiles { get; set; }
    public DbSet<CreditorFileType> CreditorFileTypes { get; set; }
    public DbSet<CreditorNotificationStatusUser> CreditorNotificationStatusUsers { get; set; }
    public DbSet<CreditorStatus> CreditorStatuses { get; set; }
    public DbSet<CreditorStatusHistory> CreditorStatusHistories { get; set; }
    public DbSet<CreditorBankAccount> CreditorBankAccounts { get; set; }
    public DbSet<DCAccount> DCAccounts { get; set; }
    public DbSet<DCDepartment> DCDepartments { get; set; }
    public DbSet<DCProduct> DCProducts { get; set; }
    public DbSet<DCStockOnHand> DCStockOnHands { get; set; }
    public DbSet<DistributionCenter> DistributionCenters { get; set; }
    public DbSet<EmailHistory> EmailHistories { get; set; }
    public DbSet<EmailHistoryCCEmail> EmailHistoryCCEmails { get; set; }
    public DbSet<EmailHistoryTemplateVariable> EmailHistoryTemplateVariables { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<EmailTemplateHistory> EmailTemplateHistories { get; set; }
    public DbSet<EmailTemplateVariableClaimNumber> EmailTemplateVariableClaimNumbers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
    public DbSet<EmployeeAsset> EmployeeAssets { get; set; }
    public DbSet<EmployeeAssetHistory> EmployeeAssetHistories { get; set; }
    public DbSet<EmployeeBadge> EmployeeBadges { get; set; }
    public DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }
    public DbSet<EmployeeBenefit> EmployeeBenefits { get; set; }
    public DbSet<EmployeeCoolerBox> EmployeeCoolerBoxes { get; set; }
    public DbSet<EmployeeCoolerBoxHistory> EmployeeCoolerBoxHistories { get; set; }
    public DbSet<EmployeeDeduction> EmployeeDeductions { get; set; }
    public DbSet<EmployeeDisciplinaryProcedure> EmployeeDisciplinaryProcedures { get; set; }
    public DbSet<EmployeeDivision> EmployeeDivisions { get; set; }
    public DbSet<EmployeeEmployeeDivision> EmployeeEmployeeDivisions { get; set; }
    public DbSet<EmployeeExpenseClaim> EmployeeExpenseClaims { get; set; }
    public DbSet<EmployeeFile> EmployeeFiles { get; set; }
    public DbSet<EmployeeFileType> EmployeeFileTypes { get; set; }
    public DbSet<EmployeeFuel> EmployeeFuels { get; set; }
    public DbSet<EmployeeHealthCondition> EmployeeHealthConditions { get; set; }
    public DbSet<EmployeeJobTitle> EmployeeJobTitles { get; set; }
    public DbSet<EmployeeJobTitleTime> EmployeeJobTitleTimes { get; set; }
    public DbSet<EmployeeJobTitleType> EmployeeJobTitleTypes { get; set; }
    public DbSet<EmployeeKpi> EmployeeKpis { get; set; }
    public DbSet<EmployeeKpiTier> EmployeeKpiTiers { get; set; }
    public DbSet<EmployeeLeaveEntry> EmployeeLeaveEntries { get; set; }
    public DbSet<EmployeeLoan> EmployeeLoans { get; set; }
    public DbSet<EmployeePayRate> EmployeePayRates { get; set; }
    public DbSet<EmployeePension> EmployeePensions { get; set; }
    public DbSet<EmployeePopiConsent> EmployeePopiConsents { get; set; }
    public DbSet<EmployeeQualification> EmployeeQualifications { get; set; }
    public DbSet<EmployeeRecurringTransaction> EmployeeRecurringTransactions { get; set; }
    public DbSet<EmployeeRecurringTransactionStatus> EmployeeRecurringTransactionStatuses { get; set; }
    public DbSet<EmployeeRegionContact> EmployeeRegionContacts { get; set; }
    public DbSet<EmployeeReinstatementHistory> EmployeeReinstatementHistories { get; set; }
    public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
    public DbSet<EmployeeSkillsDevelopment> EmployeeSkillsDevelopment { get; set; }
    public DbSet<EmployeeStoreCalendar> EmployeeStoreCalendars { get; set; }
    public DbSet<EmployeeStoreCalendarGroup> EmployeeStoreCalendarGroups { get; set; }
    public DbSet<EmployeeStoreCalendarPeriod> EmployeeStoreCalendarPeriods { get; set; }
    public DbSet<EmployeeStoreCalendarYear> EmployeeStoreCalendarYears { get; set; }
    public DbSet<EmployeeStoreCalendarType> EmployeeStoreCalendarTypes { get; set; }
    public DbSet<EmployeeStoreCalendarStatus> EmployeeStoreCalendarStatuses { get; set; }
    public DbSet<EmployeeStoreCalendarBlockDay> EmployeeStoreCalendarBlockDays { get; set; }
    public DbSet<EmployeeStoreCalendarSurveyFormSubmission> EmployeeStoreCalendarSurveyFormSubmissions { get; set; }
    public DbSet<EmployeeStoreCheckIn> EmployeeStoreCheckIns { get; set; }
    public DbSet<EmployeeSuspension> EmployeeSuspensions { get; set; }
    public DbSet<EmployeeTerminationHistory> EmployeeTerminationHistories { get; set; }
    public DbSet<EmployeeTraining> EmployeeTrainings { get; set; }
    public DbSet<EmployeeTrainingRecord> EmployeeTrainingRecords { get; set; }
    public DbSet<EmployeeTransaction> EmployeeTransactions { get; set; }
    public DbSet<EmployeeTransactionStatus> EmployeeTransactionStatuses { get; set; }
    public DbSet<EmployeeTransactionType> EmployeeTransactionTypes { get; set; }
    public DbSet<EmployeeTransactionTypeGroup> EmployeeTransactionTypeGroups { get; set; }
    public DbSet<EmployeeTransactionRemunerationType> EmployeeTransactionRemunerationTypes { get; set; }
    public DbSet<EmployeeType> EmployeeTypes { get; set; }
    public DbSet<EmployeeVehicle> EmployeeVehicles { get; set; }
    public DbSet<EmployeeVehicleHistory> EmployeeVehicleHistories { get; set; }
    public DbSet<EmployeeWorkRole> EmployeeWorkRoles { get; set; }
    public DbSet<EmployeeWorkRoleContact> EmployeeWorkRoleContacts { get; set; }
    public DbSet<EngageMasterProduct> EngageMasterProducts { get; set; }
    public DbSet<EngageRegionContact> EngageRegionContacts { get; set; }
    public DbSet<EngageSubRegion> EngageSubRegions { get; set; }
    public DbSet<EngageVariantProduct> EngageVariantProducts { get; set; }
    public DbSet<EntityBlob> EntityBlobs { get; set; }
    public DbSet<EntityContact> EntityContacts { get; set; }
    public DbSet<EntityContactCommunicationType> EntityContactCommunicationTypes { get; set; }
    public DbSet<EntityContactRegion> EntityContactRegions { get; set; }
    public DbSet<ExpenseType> ExpenseTypes { get; set; }
    public DbSet<ExternalUserType> ExternalUserTypes { get; set; }
    public DbSet<FileContainer> FileContainers { get; set; }
    public DbSet<FileType> FileTypes { get; set; }
    public DbSet<FileUpload> FileUploads { get; set; }
    public DbSet<GLAccount> GLAccounts { get; set; }
    public DbSet<GLAccountType> GLAccountTypes { get; set; }
    public DbSet<GLAdjustment> GLAdjustments { get; set; }
    public DbSet<ImportFile> ImportFiles { get; set; }
    public DbSet<ImportPromotionStore> ImportPromotionStores { get; set; }
    public DbSet<ImportSurveyStore> ImportSurveyStores { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    public DbSet<IncidentSku> IncidentSkus { get; set; }
    public DbSet<IncidentSkuStatus> IncidentSkuStatuses { get; set; }
    public DbSet<IncidentSkuType> IncidentSkuTypes { get; set; }
    public DbSet<IncidentStatus> IncidentStatuses { get; set; }
    public DbSet<IncidentType> IncidentTypes { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryGroup> InventoryGroups { get; set; }
    public DbSet<InventoryPeriod> InventoryPeriods { get; set; }
    public DbSet<InventoryStatus> InventoryStatuses { get; set; }
    public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    public DbSet<InventoryTransactionStatus> InventoryTransactionStatuses { get; set; }
    public DbSet<InventoryTransactionType> InventoryTransactionTypes { get; set; }
    public DbSet<InventoryUnitType> InventoryUnitTypes { get; set; }
    public DbSet<InventoryWarehouse> InventoryWarehouses { get; set; }
    public DbSet<InventoryYear> InventoryYears { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationEmployee> NotificationEmployees { get; set; }
    public DbSet<NotificationEmployeeJobTitle> NotificationEmployeeJobTitles { get; set; }
    public DbSet<NotificationEngageRegion> NotificationEngageRegions { get; set; }
    public DbSet<NotificationStore> NotificationStores { get; set; }
    public DbSet<NotificationStoreFormat> NotificationStoreFormats { get; set; }
    public DbSet<NotificationTarget> NotificationTargets { get; set; }
    public DbSet<NPrinting> NPrintings { get; set; }
    public DbSet<NPrintingBatch> NPrintingBatches { get; set; }
    public DbSet<OrderStaging> OrderStagings { get; set; }
    public DbSet<OrderStagingSku> OrderStagingSkus { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderSku> OrderSkus { get; set; }
    public DbSet<OrderTemplate> OrderTemplates { get; set; }
    public DbSet<OrderTemplateGroup> OrderTemplateGroups { get; set; }
    public DbSet<OrderTemplateProduct> OrderTemplateProducts { get; set; }
    public DbSet<OrderTemplateStatus> OrderTemplateStatuses { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationSetting> OrganizationSettings { get; set; }
    public DbSet<ProductFilterUpload> ProductFilterUploads { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentArchive> PaymentArchives { get; set; }
    public DbSet<PaymentBatch> PaymentBatches { get; set; }
    public DbSet<PaymentBatchRegion> PaymentBatchRegions { get; set; }
    public DbSet<PaymentLine> PaymentLines { get; set; }
    public DbSet<PaymentNotificationStatusUser> PaymentNotificationStatusUsers { get; set; }
    public DbSet<PaymentLineCostCenter> PaymentLineCostCenters { get; set; }
    public DbSet<PaymentLineCostSubDepartment> PaymentLineCostSubDepartments { get; set; }
    public DbSet<PaymentLineDivision> PaymentLineDivisions { get; set; }
    public DbSet<PaymentLineEmployee> PaymentLineEmployees { get; set; }
    public DbSet<PaymentLineFile> PaymentLineFiles { get; set; }
    public DbSet<PaymentLineFileType> PaymentLineFileTypes { get; set; }
    public DbSet<PaymentPeriod> PaymentPeriods { get; set; }
    public DbSet<PaymentProof> PaymentProofs { get; set; }
    public DbSet<PaymentProofPayment> PaymentProofPayments { get; set; }
    public DbSet<PaymentStatus> PaymentStatuses { get; set; }
    public DbSet<PaymentStatusHistory> PaymentStatusHistories { get; set; }
    public DbSet<PaymentYear> PaymentYears { get; set; }
    public DbSet<PayrollPeriod> PayrollPeriods { get; set; }
    public DbSet<PayrollYear> PayrollYears { get; set; }
    public DbSet<ProductAnalysis> ProductAnalyses { get; set; }
    public DbSet<ProductFilter> ProductFilters { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductPrice> ProductPrices { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<ProductManufacturer> ProductManufacturers { get; set; }
    public DbSet<ProductMaster> ProductMasters { get; set; }
    public DbSet<ProductMasterColor> ProductMasterColors { get; set; }
    public DbSet<ProductMasterSize> ProductMasterSizes { get; set; }
    public DbSet<ProductMasterStatus> ProductMasterStatuses { get; set; }
    public DbSet<ProductMasterSystemStatus> ProductMasterSystemStatuses { get; set; }
    public DbSet<ProductModuleStatus> ProductModuleStatuses { get; set; }
    public DbSet<ProductPackSizeType> ProductPackSizeTypes { get; set; }
    public DbSet<ProductPeriod> ProductPeriods { get; set; }
    public DbSet<ProductReason> ProductReasons { get; set; }
    public DbSet<ProductSizeType> ProductSizeTypes { get; set; }
    public DbSet<ProductSubCategory> ProductSubCategories { get; set; }
    public DbSet<ProductSubGroup> ProductSubGroups { get; set; }
    public DbSet<ProductSupplier> ProductSuppliers { get; set; }
    public DbSet<ProductSystemStatus> ProductSystemStatuses { get; set; }
    public DbSet<ProductTransaction> ProductTransactions { get; set; }
    public DbSet<ProductTransactionStatus> ProductTransactionStatuses { get; set; }
    public DbSet<ProductTransactionType> ProductTransactionTypes { get; set; }
    public DbSet<ProductVendor> ProductVendors { get; set; }
    public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
    public DbSet<ProductYear> ProductYears { get; set; }
    public DbSet<ProductWarehouseSummary> ProductWarehouseSummaries { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }
    public DbSet<ProductOrderStatus> ProductOrderStatuses { get; set; }
    public DbSet<ProductOrderType> ProductOrderTypes { get; set; }
    public DbSet<ProductOrderLine> ProductOrderLines { get; set; }
    public DbSet<ProductOrderLineStatus> ProductOrderLineStatuses { get; set; }
    public DbSet<ProductOrderLineType> ProductOrderLineTypes { get; set; }
    public DbSet<ProductOrderHistory> ProductOrderHistories { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectCampaign> ProjectCampaigns { get; set; }
    public DbSet<ProjectFile> ProjectFiles { get; set; }
    public DbSet<ProjectFileType> ProjectFileTypes { get; set; }
    public DbSet<ProjectNote> ProjectNotes { get; set; }
    public DbSet<ProjectStatus> ProjectStatuses { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }
    public DbSet<ProjectPriority> ProjectPriorities { get; set; }
    public DbSet<ProjectUser> ProjectUsers { get; set; }
    public DbSet<ProjectStakeholder> ProjectStakeholders { get; set; }
    public DbSet<ProjectStakeholderEmployeeRegionContact> ProjectStakeholderEmployeeRegionContacts { get; set; }
    public DbSet<ProjectStakeholderStoreContact> ProjectStakeholderStoreContacts { get; set; }
    public DbSet<ProjectStakeholderSupplierContact> ProjectStakeholderSupplierContacts { get; set; }
    public DbSet<ProjectStakeholderUser> ProjectStakeholderUsers { get; set; }
    public DbSet<ProjectStore> ProjectStores { get; set; }
    public DbSet<ProjectTacOp> ProjectTacOps { get; set; }
    public DbSet<ProjectTacOpRegion> ProjectTacOpRegions { get; set; }
    public DbSet<ProjectProjectTag> ProjectProjectTags { get; set; }
    public DbSet<ProjectProjectTagClaim> ProjectProjectTagClaims { get; set; }
    public DbSet<ProjectProjectTagEmployeeJobTitle> ProjectProjectTagEmployeeJobTitles { get; set; }
    public DbSet<ProjectProjectTagEngageRegion> ProjectProjectTagEngageRegions { get; set; }
    public DbSet<ProjectProjectTagDCProduct> ProjectProjectTagDCProducts { get; set; }
    public DbSet<ProjectProjectTagOrder> ProjectProjectTagOrders { get; set; }
    public DbSet<ProjectProjectTagStore> ProjectProjectTagStores { get; set; }
    public DbSet<ProjectProjectTagStoreAsset> ProjectProjectTagStoreAssets { get; set; }
    public DbSet<ProjectProjectTagSupplier> ProjectProjectTagSuppliers { get; set; }
    public DbSet<ProjectProjectTagUser> ProjectProjectTagUsers { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<ProjectTaskStatus> ProjectTaskStatuses { get; set; }
    public DbSet<ProjectTaskType> ProjectTaskTypes { get; set; }
    public DbSet<ProjectTaskNote> ProjectTaskNotes { get; set; }
    public DbSet<ProjectTaskPriority> ProjectTaskPriorities { get; set; }
    public DbSet<ProjectTaskSeverity> ProjectTaskSeverities { get; set; }
    public DbSet<ProjectTaskStakeholder> ProjectTaskStakeholders { get; set; }
    public DbSet<ProjectTaskProjectStakeholderUser> ProjectTaskProjectStakeholderUsers { get; set; }
    public DbSet<ProjectAssignee> ProjectAssignees { get; set; }
    public DbSet<ProjectCategory> ProjectCategories { get; set; }
    public DbSet<ProjectSubCategory> ProjectSubCategories { get; set; }
    public DbSet<ProjectCategorySupplier> ProjectCategorySuppliers { get; set; }
    public DbSet<ProjectComment> ProjectComments { get; set; }
    public DbSet<ProjectDcProduct> ProjectDcProducts { get; set; }
    public DbSet<ProjectExternalUser> ProjectExternalUsers { get; set; }
    public DbSet<ProjectExternalUserCommunicationType> ProjectExternalUserCommunicationTypes { get; set; }
    public DbSet<ProjectStatusHistory> projectStatusHistories { get; set; }
    public DbSet<ProjectStoreAsset> ProjectStoreAssets { get; set; }
    public DbSet<ProjectSubType> ProjectSubTypes { get; set; }
    public DbSet<ProjectTaskAssignee> ProjectTaskAssignees { get; set; }
    public DbSet<ProjectTaskComment> ProjectTaskComments { get; set; }
    public DbSet<ProjectTaskStatusHistory> ProjectTaskStatusHistories { get; set; }
    public DbSet<ProjectStakeholderExternalUser> ProjectStakeholderExternalUsers { get; set; }
    public DbSet<ProjectSupplier> ProjectSuppliers { get; set; }
    public DbSet<ProjectEngageBrand> ProjectEngageBrands { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<PromotionProduct> PromotionProducts { get; set; }
    public DbSet<PromotionStore> PromotionStores { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleUserGroup> RoleUserGroups { get; set; }
    public DbSet<SupplierSalesLead> SupplierSalesLeads { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<SecurityOrganization> SecurityOrganizations { get; set; }
    public DbSet<SecurityPermission> SecurityPermissions { get; set; }
    public DbSet<SecurityRole> SecurityRoles { get; set; }
    public DbSet<SecurityPermissionRole> SecurityPermissionRoles { get; set; }
    public DbSet<SecurityRoleUser> SecurityRoleUsers { get; set; }
    public DbSet<AnalysisPillarGroup> AnalysisPillarGroups { get; set; }
    public DbSet<AnalysisPillarSubGroup> AnalysisPillarSubGroups { get; set; }
    public DbSet<EvoLedger> EvoLedgers { get; set; }
    public DbSet<SparAnalysisGroup> SparAnalysisGroups { get; set; }
    public DbSet<SparProduct> SparProducts { get; set; }
    public DbSet<SparProductStatus> SparProductStatuses { get; set; }
    public DbSet<SparSource> SparSources { get; set; }
    public DbSet<SparSubProduct> SparSubProducts { get; set; }
    public DbSet<SparSubProductStatus> SparSubProductStatuses { get; set; }
    public DbSet<SparSystemStatus> SparSystemStatuses { get; set; }
    public DbSet<SparUnitType> SparUnitTypes { get; set; }
    public DbSet<Stakeholder> Stakeholders { get; set; }
    public DbSet<StatsStoresByRegion> StatsStoresByRegions { get; set; }
    public DbSet<StatsOrdersByRegion> StatsOrdersByRegions { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<StoreAssetCondition> StoreAssetConditions { get; set; }
    public DbSet<StoreAsset> StoreAssets { get; set; }
    public DbSet<StoreAssetStoreAssetTypeContact> StoreAssetStoreAssetTypeContacts { get; set; }
    public DbSet<StoreAssetTypeStoreAssetTypeContact> StoreAssetTypeStoreAssetTypeContacts { get; set; }
    public DbSet<StoreAssetSubType> StoreAssetSubTypes { get; set; }
    public DbSet<StoreAssetTypeContact> StoreAssetTypeContacts { get; set; }
    public DbSet<StoreAssetOwnerStoreAssetType> StoreAssetOwnerStoreAssetTypes { get; set; }
    public DbSet<StoreAssetTypeStoreAssetSubType> StoreAssetTypeStoreAssetSubTypes { get; set; }
    public DbSet<StoreAssetStatus> StoreAssetStatuses { get; set; }
    public DbSet<StoreAssetFile> StoreAssetFiles { get; set; }
    public DbSet<StoreAssetFileType> StoreAssetFileTypes { get; set; }
    public DbSet<StoreAssetBlob> StoreAssetBlobs { get; set; }
    public DbSet<StoreBankDetail> StoreBankDetails { get; set; }
    public DbSet<StoreOwner> StoreOwners { get; set; }
    public DbSet<StoreOwnerType> StoreOwnerTypes { get; set; }
    public DbSet<StoreConceptAttribute> StoreConceptAttributes { get; set; }
    public DbSet<StoreConceptAttributeOption> StoreConceptAttributeOptions { get; set; }
    public DbSet<StoreConceptAttributeValue> StoreConceptAttributeValues { get; set; }
    public DbSet<StoreContact> StoreContacts { get; set; }
    public DbSet<StoreCycle> StoreCycles { get; set; }
    public DbSet<StoreFilter> StoreFilters { get; set; }
    public DbSet<StoreFilterUpload> StoreFilterUploads { get; set; }
    public DbSet<StorePOS> StorePOS { get; set; }
    public DbSet<StorePOSFreezerQuestion> StorePOSFreezerQuestions { get; set; }
    public DbSet<StorePOSQuestion> StorePOSQuestions { get; set; }
    public DbSet<SubContractorBrand> SubContractorBrands { get; set; }
    public DbSet<SubWarehouse> SubWarehouses { get; set; }
    public DbSet<SupplierAllowance> SupplierAllowances { get; set; }
    public DbSet<SupplierAllowanceContract> SupplierAllowanceContracts { get; set; }
    public DbSet<SupplierAllowanceSubContract> SupplierAllowanceSubContracts { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierContact> SupplierContacts { get; set; }
    public DbSet<SupplierSetting> SupplierSettings { get; set; }
    public DbSet<SupplierBudget> SupplierBudgets { get; set; }
    public DbSet<SupplierBudgetType> SupplierBudgetTypes { get; set; }
    public DbSet<SupplierBudgetVersion> SupplierBudgetVersions { get; set; }
    public DbSet<SupplierBudgetVersionType> SupplierBudgetVersionTypes { get; set; }
    public DbSet<SupplierContract> SupplierContracts { get; set; }
    public DbSet<SupplierContractDetail> SupplierContractDetails { get; set; }
    public DbSet<SupplierContractDetailType> SupplierContractDetailTypes { get; set; }
    public DbSet<SupplierContractGroup> SupplierContractGroups { get; set; }
    public DbSet<SupplierContractSubGroup> SupplierContractSubGroups { get; set; }
    public DbSet<SupplierContractType> SupplierContractTypes { get; set; }
    public DbSet<SupplierYear> SupplierYears { get; set; }
    public DbSet<SupplierPeriod> SupplierPeriods { get; set; }
    public DbSet<SupplierContractAmount> SupplierContractAmounts { get; set; }
    public DbSet<SupplierContractAmountType> SupplierContractAmountTypes { get; set; }
    public DbSet<SupplierContractSplit> SupplierContractSplits { get; set; }
    public DbSet<SupplierSubContract> SupplierSubContracts { get; set; }
    public DbSet<SupplierSubContractDetail> SupplierSubContractDetails { get; set; }
    public DbSet<SupplierSubContractDetailType> SupplierSubContractDetailTypes { get; set; }
    public DbSet<SupplierSubContractType> SupplierSubContractTypes { get; set; }
    public DbSet<SupplierSubRegion> SupplierSubRegions { get; set; }
    public DbSet<SurveyForm> SurveyForms { get; set; }
    public DbSet<SurveyFormAnswer> SurveyFormAnswers { get; set; }
    public DbSet<SurveyFormAnswerOption> SurveyFormAnswerOptions { get; set; }
    public DbSet<SurveyFormAnswerHistory> SurveyFormAnswerHistories { get; set; }
    public DbSet<SurveyFormAnswerOptionHistory> SurveyFormAnswerOptionHistories { get; set; }
    public DbSet<SurveyFormOption> SurveyFormOptions { get; set; }
    public DbSet<SurveyFormProduct> SurveyFormProducts { get; set; }
    public DbSet<SurveyFormQuestion> SurveyFormQuestions { get; set; }
    public DbSet<SurveyFormQuestionGroup> SurveyFormQuestionGroups { get; set; }
    public DbSet<SurveyFormQuestionGroupProduct> SurveyFormQuestionGroupProducts { get; set; }
    public DbSet<SurveyFormQuestionOption> SurveyFormQuestionOptions { get; set; }
    public DbSet<SurveyFormQuestionProduct> SurveyFormQuestionProducts { get; set; }
    public DbSet<SurveyFormQuestionReason> SurveyFormQuestionReasons { get; set; }
    public DbSet<SurveyFormQuestionType> SurveyFormQuestionTypes { get; set; }
    public DbSet<SurveyFormQuestionValueComparisonTargetType> SurveyFormQuestionValueComparisonTargetTypes { get; set; }
    public DbSet<SurveyFormQuestionValueComparisonOperation> SurveyFormQuestionValueComparisonOperations { get; set; }
    public DbSet<SurveyFormReason> SurveyFormReasons { get; set; }
    public DbSet<SurveyFormSubmission> SurveyFormSubmissions { get; set; }
    public DbSet<SurveyFormType> SurveyFormTypes { get; set; }
    public DbSet<SurveyFormTarget> SurveyFormTargets { get; set; }
    public DbSet<SurveyFormExcludedEmployee> SurveyFormExcludedEmployees { get; set; }
    public DbSet<SurveyFormExcludedStore> SurveyFormExcludedStores { get; set; }
    public DbSet<SurveyFormEmployee> SurveyFormEmployees { get; set; }
    public DbSet<SurveyFormEmployeeDivision> SurveyFormEmployeeDivisions { get; set; }
    public DbSet<SurveyFormEmployeeJobTitle> SurveyFormEmployeeJobTitles { get; set; }
    public DbSet<SurveyFormEmployeeEngageRegion> SurveyFormEmployeeEngageRegions { get; set; }
    public DbSet<SurveyFormEngageDepartment> SurveyFormEngageDepartments { get; set; }
    public DbSet<SurveyFormStore> SurveyFormStores { get; set; }
    public DbSet<SurveyFormStoreEngageRegion> SurveyFormStoreEngageRegions { get; set; }
    public DbSet<SurveyFormStoreCluster> SurveyFormStoreClusters { get; set; }
    public DbSet<SurveyFormStoreFormat> SurveyFormStoreFormats { get; set; }
    public DbSet<SurveyFormStoreLSM> SurveyFormStoreLSMs { get; set; }
    public DbSet<SurveyFormStoreType> SurveyFormStoreTypes { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
    public DbSet<SurveyAnswerOption> SurveyAnswerOptions { get; set; }
    public DbSet<SurveyAnswerPhoto> SurveyAnswerPhotos { get; set; }
    public DbSet<SurveyEmployeeJobTitleTarget> SurveyEmployeeJobTitleTargets { get; set; }
    public DbSet<SurveyEmployeeTarget> SurveyEmployeeTargets { get; set; }
    public DbSet<SurveyEngageRegionTarget> SurveyEngageRegionTargets { get; set; }
    public DbSet<SurveyInstance> SurveyInstances { get; set; }
    public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    public DbSet<SurveyQuestionRule> SurveyQuestionRules { get; set; }
    public DbSet<SurveyStoreFormatTarget> SurveyStoreFormatTargets { get; set; }
    public DbSet<SurveyStoreTarget> SurveyStoreTargets { get; set; }
    public DbSet<SurveyQuestionOption> SurveyQuestionOptions { get; set; }
    public DbSet<SurveyTarget> SurveyTargets { get; set; }
    public DbSet<Targeting> Targetings { get; set; }
    public DbSet<TargetStrategy> TargetStrategies { get; set; }
    public DbSet<TenantSetting> TenantSettings { get; set; }
    public DbSet<TrainingFile> TrainingFiles { get; set; }
    public DbSet<TrainingFileType> TrainingFileTypes { get; set; }
    public DbSet<TrainingProvider> TrainingProviders { get; set; }
    public DbSet<TrainingType> TrainingTypes { get; set; }
    public DbSet<TrainingCategory> TrainingCategories { get; set; }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<TrainingDuration> TrainingDurations { get; set; }
    public DbSet<TrainingFacilitator> TrainingFacilitators { get; set; }
    public DbSet<TrainingPeriod> TrainingPeriods { get; set; }
    public DbSet<TrainingYear> TrainingYears { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCommunicationType> UserCommunicationTypes { get; set; }
    public DbSet<UserEngageSubGroup> UserEngageSubGroups { get; set; }
    public DbSet<UserEntity> UserEntities { get; set; }
    public DbSet<UserEntityRecord> UserEntityRecords { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<UserOrganization> UserOrganizations { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<UserOrganizationRole> UserOrganizationRoles { get; set; }
    public DbSet<UserRegion> UserRegions { get; set; }
    public DbSet<UserRolePermission> UserRolePermissions { get; set; }
    public DbSet<UserStore> UserStores { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<Vat> Vat { get; set; }
    public DbSet<VatPeriod> VatPeriods { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<VoucherDetail> VoucherDetails { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<WebEvent> WebEvents { get; set; }
    public DbSet<WebFile> WebFiles { get; set; }
    public DbSet<WebFileCategory> WebFileCategories { get; set; }
    public DbSet<WebFileEmployee> WebFileEmployees { get; set; }
    public DbSet<WebFileEmployeeJobTitle> WebFileEmployeeJobTitles { get; set; }
    public DbSet<WebFileEmployeeDivision> WebFileEmployeeDivisions { get; set; }
    public DbSet<WebFileEngageDepartment> WebFileEngageDepartments { get; set; }
    public DbSet<WebFileEngageRegion> WebFileEngageRegions { get; set; }
    public DbSet<WebFileGroup> WebFileGroups { get; set; }
    public DbSet<WebFileStore> WebFileStores { get; set; }
    public DbSet<WebFileStoreFormat> WebFileStoreFormats { get; set; }
    public DbSet<WebFileTarget> WebFileTargets { get; set; }
    public DbSet<WebPage> WebPages { get; set; }
    public DbSet<WebPageEmployee> WebPageEmployees { get; set; }
    #endregion

    #region File Entities

    // Deprecate Use JSON file columns
    public DbSet<EmployeeQualificationFile> EmployeeQualificationFiles { get; set; }
    public DbSet<EmployeeBankDetailFile> EmployeeBankDetailFiles { get; set; }
    public DbSet<EmployeeSkillFile> EmployeeSkillFiles { get; set; }
    public DbSet<EmployeeCoolerBoxFile> EmployeeCoolerBoxFiles { get; set; }
    #endregion    

    #region Link Entities
    public DbSet<BudgetYearVersion> BudgetYearVersions { get; set; }
    public DbSet<ClaimClassificationType> ClaimClassificationTypes { get; set; }
    public DbSet<ClaimFloatClaim> ClaimFloatClaims { get; set; }
    public DbSet<ClaimTypeReportType> ClaimTypeReportTypes { get; set; }
    public DbSet<DCDept> DCDepts { get; set; }
    public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
    public DbSet<EmployeeNotification> EmployeeNotifications { get; set; }
    public DbSet<EmployeeRegion> EmployeeRegions { get; set; }
    public DbSet<EmployeeEmployeeHealthCondition> EmployeeEmployeeHealthConditions { get; set; }
    public DbSet<EmployeeEmployeeJobTitle> EmployeeEmployeeJobTitles { get; set; }
    public DbSet<EmployeeJobTitleUserGroup> EmployeeJobTitleUserGroups { get; set; }
    public DbSet<EmployeeReport> EmployeeReports { get; set; }
    public DbSet<EmployeeStore> EmployeeStores { get; set; }
    public DbSet<EmployeeStoreArchive> EmployeeStoreArchives { get; set; }
    public DbSet<EngageRegionClaimManager> EngageRegionClaimManagers { get; set; }
    public DbSet<EngageProductTag> EngageProductTags { get; set; }
    public DbSet<EngageSubGroupEngageBrand> EngageSubGroupEngageBrands { get; set; }
    public DbSet<NotificationEmployeeRead> NotificationEmployeeReads { get; set; }
    public DbSet<NotificationRegion> NotificationRegions { get; set; }
    public DbSet<OrderEngageDepartment> OrderEngageDepartments { get; set; }
    public DbSet<ProductWarehouseRegion> ProductWarehouseRegions { get; set; }
    public DbSet<StoreStoreConcept> StoreStoreConcepts { get; set; }
    public DbSet<StoreConceptLevel> StoreConceptLevels { get; set; }
    public DbSet<StoreStoreDepartment> StoreStoreDepartments { get; set; }
    public DbSet<EngageSubGroupSupplier> EngageSubGroupSuppliers { get; set; }
    public DbSet<SupplierClaimAccountManager> SupplierClaimAccountManagers { get; set; }
    public DbSet<SupplierClaimClassification> SupplierClaimClassifications { get; set; }
    public DbSet<SupplierEngageBrand> SupplierEngageBrands { get; set; }
    public DbSet<SupplierProduct> SupplierProducts { get; set; }
    public DbSet<SupplierStore> SupplierStores { get; set; }
    public DbSet<SupplierSupplierType> SupplierSupplierTypes { get; set; }
    public DbSet<SurveyEmployee> SurveyEmployees { get; set; }
    public DbSet<SurveyEngageRegion> SurveyEngageRegions { get; set; }
    public DbSet<SurveyQuestionFalseReason> SurveyQuestionFalseReasons { get; set; }
    public DbSet<SurveyStore> SurveyStores { get; set; }
    public DbSet<SurveyStoreFormat> SurveyStoreFormats { get; set; }
    public DbSet<UserUserGroup> UserUserGroups { get; set; }
    public DbSet<EmployeeEmployeeBadge> EmployeeEmployeeBadges { get; set; }
    public DbSet<EmployeeEmployeeKpi> EmployeeEmployeeKpis { get; set; }
    public DbSet<EmployeeStoreKpi> EmployeeStoreKpis { get; set; }
    public DbSet<EmployeeKpiScore> EmployeeKpiScores { get; set; }
    public DbSet<EmployeeStoreKpiScore> EmployeeStoreKpiScores { get; set; }
    public DbSet<NotificationNotificationChannel> NotificationNotificationChannels { get; set; }
    public DbSet<StoreConceptAttributeStoreAsset> StoreConceptAttributeStoreAssets { get; set; }
    public DbSet<StoreStoreConceptPerformance> StoreStoreConceptPerformances { get; set; }

    #endregion

    #region Option Entities
    public DbSet<ClientType> ClientTypes { get; set; }

    // Option Types
    public DbSet<OptionTypeGroup> OptionTypeGroups { get; set; }
    public DbSet<OptionType> OptionTypes { get; set; }

    // Assets
    public DbSet<AssetOwner> AssetOwners { get; set; }
    public DbSet<AssetStatus> AssetStatuses { get; set; }

    // Attachments
    public DbSet<AttachmentType> AttachmentTypes { get; set; }

    // Bank Accounts 
    public DbSet<BankAccountOwner> BankAccountOwners { get; set; }
    public DbSet<BankAccountType> BankAccountTypes { get; set; }
    public DbSet<BankPaymentMethod> BankPaymentMethods { get; set; }
    public DbSet<BankName> BankNames { get; set; }

    // CRM Types
    public DbSet<ContactType> ContactTypes { get; set; }
    public DbSet<EventType> EventTypes { get; set; }

    // Location Types
    public DbSet<LocationType> LocationTypes { get; set; }
    public DbSet<EngageRegion> EngageRegions { get; set; }
    public DbSet<EngageLocation> EngageLocations { get; set; }

    // Employee
    public DbSet<EmployeeAssetBrand> EmployeeAssetBrands { get; set; }
    public DbSet<EmployeeAssetType> EmployeeAssetTypes { get; set; }

    public DbSet<EmployeeBadgeType> EmployeeBadgeTypes { get; set; }
    public DbSet<EmployeeCoolerBoxCondition> EmployeeCoolerBoxConditions { get; set; }
    public DbSet<EmployeeKpiType> EmployeeKpiTypes { get; set; }
    public DbSet<EmployeeState> EmployeeStates { get; set; }
    public DbSet<EmployeeIncentiveTier> EmployeeIncentiveTiers { get; set; }

    // public DbSet<EmployeeJobTitleGroup> EmployeeJobTitleGroups { get; set; }
    public DbSet<EmployeeLanguage> EmployeeLanguages { get; set; }
    public DbSet<EmployeePaymentType> EmployeePaymentTypes { get; set; }
    public DbSet<EmployeePensionScheme> EmployeePensionSchemes { get; set; }
    public DbSet<EmployeePensionCategory> EmployeePensionCategories { get; set; }
    public DbSet<EmployeePensionContributionPercentage> EmployeePensionContributionPercentages { get; set; }
    public DbSet<EmployeeFuelExpenseType> EmployeeFuelExpenseTypes { get; set; }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<BenefitType> BenefitTypes { get; set; }
    public DbSet<DeductionType> DeductionTypes { get; set; }
    public DbSet<DeductionCycleType> DeductionCycleTypes { get; set; }
    public DbSet<EmploymentType> EmploymentTypes { get; set; }
    public DbSet<EmployeeDisabledType> EmployeeDisabledTypes { get; set; }
    public DbSet<EmployeeIdentificationType> EmployeeIdentificationTypes { get; set; }
    public DbSet<EmployeeNationality> EmployeeNationalities { get; set; }
    public DbSet<EmployeePersonType> EmployeePersonTypes { get; set; }
    public DbSet<EmployeeSDLExemption> EmployeeSDLExemptions { get; set; }
    public DbSet<EmployeeTaxStatus> EmployeeTaxStatuses { get; set; }
    public DbSet<EmployeeUIFExemption> EmployeeUIFExemptions { get; set; }
    public DbSet<EmployeeDefaultPayslip> EmployeeDefaultPayslips { get; set; }
    public DbSet<EmployeeReinstatementReason> EmployeeReinstatementReasons { get; set; }
    public DbSet<EmployeeSuspensionReason> EmployeeSuspensionReasons { get; set; }
    public DbSet<EmployeeTerminationReason> EmployeeTerminationReasons { get; set; }
    public DbSet<EmployeeStandardIndustryGroupCode> EmployeeStandardIndustryGroupCodes { get; set; }
    public DbSet<EmployeeStandardIndustryCode> EmployeeStandardIndustryCodes { get; set; }
    public DbSet<EmploymentAction> EmploymentActions { get; set; }
    public DbSet<ExpenseClaimStatus> ExpenseClaimStatuses { get; set; }
    public DbSet<WorkRoleStatus> WorkRoleStatuses { get; set; }

    public DbSet<Grade> Grades { get; set; }
    public DbSet<SkillCategory> SkillCategories { get; set; }
    public DbSet<Proficiency> Proficiencies { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<EducationLevel> EducationLevels { get; set; }
    public DbSet<InstitutionType> InstitutionTypes { get; set; }
    public DbSet<EmployeeTrainingStatus> EmployeeTrainingStatuses { get; set; }

    public DbSet<MaritalStatus> MaritalStatuses { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<NextOfKinType> NextOfKinTypes { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<UniformSize> UniformSizes { get; set; }

    public DbSet<VehicleBrand> VehicleBrands { get; set; }
    public DbSet<VehicleType> VehicleTypes { get; set; }
    // Store 
    public DbSet<StoreAssetOwner> StoreAssetOwners { get; set; }
    public DbSet<StoreAssetType> StoreAssetTypes { get; set; }
    public DbSet<StoreCycleOperation> StoreCycleOperations { get; set; }
    public DbSet<StoreDepartment> StoreDepartments { get; set; }
    public DbSet<StoreConcept> StoreConcepts { get; set; }
    public DbSet<StoreConceptType> StoreConceptTypes { get; set; }
    public DbSet<StoreConceptAttributeType> StoreConceptAttributeTypes { get; set; }
    public DbSet<FrequencyType> FrequencyTypes { get; set; }
    public DbSet<StoreClaimType> StoreClaimTypes { get; set; }
    public DbSet<StoreGroup> StoreGroups { get; set; }
    public DbSet<StoreType> StoreTypes { get; set; }
    public DbSet<StoreFormat> StoreFormats { get; set; }
    public DbSet<StoreCluster> StoreClusters { get; set; }
    public DbSet<StoreLSM> StoreLSMs { get; set; }
    public DbSet<StoreMediaGroup> StoreMediaGroups { get; set; }
    public DbSet<StoreSparRegion> StoreSparRegions { get; set; }
    public DbSet<StorePOSType> StorePOSTypes { get; set; }
    public DbSet<StorePOSFreezerType> StorePOSFreezerTypes { get; set; }

    // Products
    public DbSet<DCProductClass> DCProductClasses { get; set; }
    public DbSet<ProductClassification> ProductClassifications { get; set; }

    // Warehouse
    public DbSet<WarehouseType> WarehouseTypes { get; set; }

    // Supplier
    public DbSet<SupplierType> SupplierTypes { get; set; }
    public DbSet<SupplierGroup> SupplierGroups { get; set; }
    public DbSet<SupplierRegion> SupplierRegions { get; set; }

    // Product
    public DbSet<UnitType> UnitTypes { get; set; }
    public DbSet<EngageTag> EngageTags { get; set; }
    public DbSet<EngageDepartmentGroup> EngageDepartmentGroups { get; set; }
    public DbSet<EngageDepartment> EngageDepartments { get; set; }
    public DbSet<EngageDepartmentCategory> EngageDepartmentCategories { get; set; }
    public DbSet<EngageGroup> EngageGroups { get; set; }
    public DbSet<EngageSubGroup> EngageSubGroups { get; set; }
    public DbSet<EngageCategory> EngageCategories { get; set; }
    public DbSet<EngageSubCategory> EngageSubCategories { get; set; }
    public DbSet<EngageBrand> EngageBrands { get; set; }
    public DbSet<ProductActiveStatus> ProductActiveStatuses { get; set; }
    public DbSet<ProductStatus> ProductStatuses { get; set; }
    public DbSet<ProductWarehouseStatus> ProductWarehouseStatuses { get; set; }
    public DbSet<ProductAnalysisGroup> ProductAnalysisGroups { get; set; }
    public DbSet<ProductAnalysisDivision> ProductAnalysisDivisions { get; set; }

    // Notification
    public DbSet<NotificationType> NotificationTypes { get; set; }
    public DbSet<NotificationCategory> NotificationCategories { get; set; }
    public DbSet<NotificationChannel> NotificationChannels { get; set; }

    // Order
    public DbSet<OrderType> OrderTypes { get; set; }
    public DbSet<OrderSkuType> OrderSkuTypes { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<OrderSkuStatus> OrderSkuStatuses { get; set; }
    public DbSet<OrderQuantityType> OrderQuantityTypes { get; set; }

    // Surveys
    public DbSet<SurveyType> SurveyTypes { get; set; }
    public DbSet<QuestionType> QuestionTypes { get; set; }
    public DbSet<QuestionFalseReason> QuestionFalseReasons { get; set; }

    // Financial Reporting
    public DbSet<BudgetVersion> BudgetVersions { get; set; }
    public DbSet<BudgetType> BudgetTypes { get; set; }
    public DbSet<GLAdjustmentType> GLAdjustmentTypes { get; set; }

    //Claims
    public DbSet<ClaimStatus> ClaimStatuses { get; set; }
    public DbSet<ClaimSupplierStatus> ClaimSupplierStatuses { get; set; }
    public DbSet<ClaimSkuStatus> ClaimSkuStatuses { get; set; }
    public DbSet<EntityContactType> EntityContactTypes { get; set; }
    public DbSet<ClaimQuantityType> ClaimQuantityTypes { get; set; }
    public DbSet<ClaimRejectedReason> ClaimRejectedReasons { get; set; }
    public DbSet<ClaimPendingReason> ClaimPendingReasons { get; set; }
    public DbSet<ClaimReportType> ClaimReportTypes { get; set; }
    public DbSet<EmailTemplateType> EmailTemplateTypes { get; set; }
    public DbSet<EmailType> EmailTypes { get; set; }

    // Promotions
    public DbSet<PromotionType> PromotionTypes { get; set; }
    public DbSet<PromotionProductType> PromotionProductTypes { get; set; }

    public DbSet<EmployeePayRateFrequency> EmployeePayRateFrequencies { get; set; }
    public DbSet<EmployeePayRatePackage> EmployeePayRatePackages { get; set; }

    // Vouchers
    public DbSet<VoucherType> VoucherTypes { get; set; }
    public DbSet<VoucherStatus> VoucherStatuses { get; set; }
    public DbSet<VoucherDetailStatus> VoucherDetailStatuses { get; set; }

    public DbSet<WebEventType> WebEventTypes { get; set; }
    public DbSet<WhatsAppHistory> WhatsAppHistories { get; set; }
    #endregion

    #region Views   
    public DbSet<StatsByEngageRegionView> StatsByEngageRegionView { get; set; }
    public DbSet<SurveysByEmployeeStoreView> SurveysByEmployeeStoreView { get; set; }
    public DbSet<SurveysByEmployeePerRegionView2> SurveysByEmployeePerRegionView2 { get; set; }
    public DbSet<SurveysByEmployeePerStoreView> SurveysByEmployeePerStoreView { get; set; }
    public DbSet<SurveysByEmployeePerStoreView_> SurveysByEmployeePerStoreView_ { get; set; }
    public DbSet<SurveysByEmployeePerStoreFormatView> SurveysByEmployeePerStoreFormatView { get; set; }
    public DbSet<SurveysByEmployeePerRegionView> SurveysByEmployeePerRegionView { get; set; }
    public DbSet<SurveysByEmployeeSubGroupPerStoreView> SurveysByEmployeeSubGroupPerStoreView { get; set; }
    public DbSet<SurveysByEmployeeSubGroupPerStoreFormatView> SurveysByEmployeeSubGroupPerStoreFormatView { get; set; }
    public DbSet<SurveysByEmployeeSubGroupPerRegionView> SurveysByEmployeeSubGroupPerRegionView { get; set; }

    #endregion

    #region Auditing
    public DbSet<AuditEntry> AuditEntries { get; set; }
    public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }
    #endregion

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = string.IsNullOrWhiteSpace(_user.UserName) ? entry.Entity.CreatedBy : _user.UserName;
                    entry.Entity.Created = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _user.UserName;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        var audit = new AuditEntityId
        {
            CreatedBy = _user.UserName
        };
        audit.PreSaveChanges(this);
        this.EnforceMultiTenant();
        var opStatus = await base.SaveChangesAsync(cancellationToken);
        audit.PostSaveChanges();

        if (audit.Configuration.AutoSavePreAction != null)
        {
            audit.Configuration.AutoSavePreAction(this, audit);
            this.EnforceMultiTenant();
            await base.SaveChangesAsync(cancellationToken);
        }

        //return base.SaveChangesAsync(cancellationToken);
        return opStatus;
    }

    async Task<OperationStatus> IAppDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        var opStatus = new OperationStatus
        {
            RecordsAffected = await SaveChangesAsync(cancellationToken)
        };
        opStatus.Status = opStatus.RecordsAffected > 0;

        return opStatus;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Apply all IEntityTypeConfiguration configurations
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure base entities
        foreach (var entityType in builder.Model.GetEntityTypes()
                                                .Where(e => e.ClrType.IsSubclassOf(typeof(BaseEntity)) &&
                                                            !e.ClrType.IsSubclassOf(typeof(EntityBlob)) &&
                                                            !e.ClrType.IsSubclassOf(typeof(EntityContact)) &&
                                                            !e.ClrType.IsSubclassOf(typeof(NotificationTarget)) &&
                                                            !e.ClrType.IsSubclassOf(typeof(SurveyTarget)) &&
                                                            !e.ClrType.IsSubclassOf(typeof(WebFileTarget)) &&
                                                            !e.ClrType.IsSubclassOf(typeof(CategoryFileTarget)) &&
                                                            !e.ClrType.IsSubclassOf(typeof(SurveyFormTarget)) &&
                                                            !e.ClrType.IsSubclassOf(typeof(Project)) &&
                                                            !e.ClrType.IsSubclassOf(typeof(ProjectStakeholder)) &&
                                                            !e.ClrType.IsSubclassOf(typeof(ProjectProjectTag)) &&
                                                            !e.ClrType.IsSubclassOf(typeof(CommunicationHistory))
                                                            ))
        {
            // Soft delete. Automatically exclude deleted entities
            builder.Entity(entityType.Name, b =>
            {
                b.HasQueryFilter(DeletedLambdaExpression(entityType.ClrType));
            });
        }

        builder.Model.GetEntityTypes()
            .Where(e => e.ClrType.Name != typeof(JsonActiveColor).Name &&
                        e.ClrType.Name != typeof(JsonCssVars).Name &&
                        e.ClrType.Name != typeof(JsonEmployeeField).Name &&
                        e.ClrType.Name != typeof(JsonFile).Name &&
                        e.ClrType.Name != typeof(JsonRule).Name &&
                        e.ClrType.Name != typeof(JsonSetting).Name &&
                        e.ClrType.Name != typeof(JsonStoreConcept).Name &&
                        e.ClrType.Name != typeof(JsonText).Name &&
                        e.ClrType.Name != typeof(JsonThemeSetting).Name &&
                        e.ClrType.Name != typeof(JsonThemeValues).Name &&
                        e.ClrType.Name != typeof(JsonLink).Name &&
                        e.ClrType.Name != typeof(BooleanSetting).Name &&
                        e.ClrType.Name != typeof(NumberSetting).Name &&
                        e.ClrType.Name != typeof(StringSetting).Name &&
                        e.ClrType.Name != typeof(SurveyFormQuestionValueComparisonOperation).Name &&
                        e.ClrType.Name != typeof(SurveyFormQuestionValueComparisonTargetType).Name &&
                        !e.ClrType.IsSubclassOf(typeof(NotificationTarget)) &&
                        !e.ClrType.IsSubclassOf(typeof(SurveyTarget)) &&
                        !e.ClrType.IsSubclassOf(typeof(WebFileTarget)) &&
                        !e.ClrType.IsSubclassOf(typeof(CategoryFileTarget)) &&
                        !e.ClrType.IsSubclassOf(typeof(SurveyFormTarget)) &&
                        !e.ClrType.IsSubclassOf(typeof(EntityBlob)) &&
                        !e.ClrType.IsSubclassOf(typeof(EntityContact)))
            .ForEach(e =>
            {
                builder.Entity(e.Name, b =>
                {
                    b.IsMultiTenant();
                });
            });

        #region Add indexes to code and name columns

        builder.Entity<Contact>()
            .HasIndex(e => e.FullName);

        builder.Entity<DCProduct>()
            .HasIndex(e => e.Code).IsUnique();

        builder.Entity<DCProduct>()
           .HasIndex(e => e.Name);

        builder.Entity<Employee>()
           .HasIndex(e => e.Code).IsUnique();

        builder.Entity<Employee>()
           .HasIndex(e => e.FirstName);

        builder.Entity<Employee>()
           .HasIndex(e => e.LastName);

        builder.Entity<EngageMasterProduct>()
           .HasIndex(e => e.Code).IsUnique();

        builder.Entity<EngageMasterProduct>()
           .HasIndex(e => e.Name);

        builder.Entity<EngageVariantProduct>()
           .HasIndex(e => e.Code).IsUnique();

        builder.Entity<EngageVariantProduct>()
           .HasIndex(e => e.Name);

        builder.Entity<Store>()
            .HasIndex(e => e.Code).IsUnique();

        builder.Entity<Store>()
            .HasIndex(e => e.Name);

        builder.Entity<Supplier>()
            .HasIndex(e => e.Code).IsUnique();

        builder.Entity<Supplier>()
            .HasIndex(e => e.Name);

        builder.Entity<Vendor>()
            .HasIndex(e => e.Name);
        #endregion

        base.OnModelCreating(builder);

        builder.ConfigureMultiTenant();

        //builder.Entity<BaseEntity>().IsMultiTenant();
    }

    private static LambdaExpression DeletedLambdaExpression(Type type)
    {
        var parameter = Expression.Parameter(type);

        var falseConstant = Expression.Constant(false);
        var deletedProperty = Expression.Property(parameter, "Deleted");
        var body = Expression.Equal(deletedProperty, falseConstant);

        return Expression.Lambda(body, parameter);
    }
}