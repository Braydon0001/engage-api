namespace Engage.Application.Services.Shared.AssignCommands;

public class AssignCommand : IRequest<OperationStatus>
{
    public string Mapping { get; set; }
    public int ToId { get; set; }
    public int AssignedId { get; set; }
    public bool SaveChanges { get; set; } = true;

    public AssignCommand()
    {
    }

    public AssignCommand(string mapping, int toId, int assignedId, bool saveChanges = true)
    {
        Mapping = mapping;
        AssignedId = assignedId;
        ToId = toId;
        SaveChanges = saveChanges;
    }
}

public class AssignCommandHandler : IRequestHandler<AssignCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public AssignCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(AssignCommand command, CancellationToken cancellationToken)
    {
        var mapping = command.Mapping.ToUpper();

        switch (mapping)
        {
            case AssignDesc.WAREHOUSE_DC:
                _context.Warehouses.Add(new Warehouse { DCId = command.ToId, WarehouseId = command.AssignedId });
                break;
            case AssignDesc.DEPT_DC:
                _context.DCDepts.Add(new DCDept { DistributionCenterId = command.ToId, DCDepartmentId = command.AssignedId });
                break;
            case AssignDesc.CLAIM_REPORT_TYPE_CLAIM_TYPE:
                _context.ClaimTypeReportTypes.Add(new ClaimTypeReportType { ClaimTypeId = command.ToId, ClaimReportTypeId = command.AssignedId });
                break;
            case AssignDesc.CLAIM_ACCOUNT_MANAGER_SUPPLIER:
                _context.SupplierClaimAccountManagers.Add(new SupplierClaimAccountManager { SupplierId = command.ToId, UserId = command.AssignedId });
                break;
            case AssignDesc.CLAIM_CLASSIFICATION_SUPPLIER:
                _context.SupplierClaimClassifications.Add(new SupplierClaimClassification { SupplierId = command.ToId, ClaimClassificationId = command.AssignedId });
                break;
            case AssignDesc.CLAIM_CLASSIFICATION_TYPE:
                _context.ClaimClassificationTypes.Add(new ClaimClassificationType { ClaimClassificationId = command.ToId, ClaimTypeId = command.AssignedId });
                break;
            case AssignDesc.CLAIM_MANAGER_ENGAGE_REGION:
                _context.EngageRegionClaimManagers.Add(new EngageRegionClaimManager { EngageRegionId = command.ToId, UserId = command.AssignedId });
                break;
            case AssignDesc.DEPT_EMPLOYEE:
                _context.EmployeeDepartments.Add(new EmployeeDepartment { EmployeeId = command.ToId, EngageDepartmentId = command.AssignedId });
                break;
            case AssignDesc.DIVISION_EMPLOYEE:
                _context.EmployeeEmployeeDivisions.Add(new EmployeeEmployeeDivision { EmployeeId = command.ToId, EmployeeDivisionId = command.AssignedId });
                break;
            case AssignDesc.PROJECT_USER:
                _context.ProjectUsers.Add(new ProjectUser { ProjectId = command.ToId, UserId = command.AssignedId });
                break;
            //case AssignDesc.PROJECT_TAG:
            //    _context.ProjectProjectTags.Add(new ProjectProjectTag { ProjectId = command.ToId, ProjectTagId = command.AssignedId });
            //    break;
            case AssignDesc.HEALTH_CONDITION_EMPLOYEE:
                _context.EmployeeEmployeeHealthConditions.Add(new EmployeeEmployeeHealthCondition { EmployeeId = command.ToId, EmployeeHealthConditionId = command.AssignedId });
                break;
            case AssignDesc.JOB_TITLE_EMPLOYEE:
                _context.EmployeeEmployeeJobTitles.Add(new EmployeeEmployeeJobTitle { EmployeeId = command.ToId, EmployeeJobTitleId = command.AssignedId });
                break;
            case AssignDesc.EMPLOYEE_NOTIFICATION:
                _context.NotificationEmployees.Add(new NotificationEmployee { NotificationId = command.ToId, EmployeeId = command.AssignedId });
                break;
            case AssignDesc.EMPLOYEE_SURVEY:
                _context.SurveyEmployees.Add(new SurveyEmployee { SurveyId = command.ToId, EmployeeId = command.AssignedId });
                break;
            case AssignDesc.ENGAGE_DEPARTMENT_ORDER:
                _context.OrderEngageDepartments.Add(new OrderEngageDepartment { OrderId = command.ToId, EngageDepartmentId = command.AssignedId });
                break;
            case AssignDesc.FALSEREASON_SURVEYQUESTION:
                _context.SurveyQuestionFalseReasons.Add(new SurveyQuestionFalseReason { SurveyQuestionId = command.ToId, QuestionFalseReasonId = command.AssignedId });
                break;
            case AssignDesc.PRODUCT_SUPPLIER:
                _context.SupplierProducts.Add(new SupplierProduct { SupplierId = command.ToId, EngageMasterProductId = command.AssignedId });
                break;
            case AssignDesc.NOTIFICATION_CHANNEL_NOTIFICATION:
                _context.NotificationNotificationChannels.Add(new NotificationNotificationChannel { NotificationId = command.ToId, NotificationChannelId = command.AssignedId });
                break;
            case AssignDesc.REGION_EMPLOYEE:
                _context.EmployeeRegions.Add(new EmployeeRegion { EmployeeId = command.ToId, EngageRegionId = command.AssignedId });
                break;
            case AssignDesc.REGION_NOTIFICATION:
                _context.NotificationRegions.Add(new NotificationRegion { NotificationId = command.ToId, EngageRegionId = command.AssignedId });
                break;
            case AssignDesc.REGION_SURVEY:
                _context.SurveyEngageRegions.Add(new SurveyEngageRegion { SurveyId = command.ToId, EngageRegionId = command.AssignedId });
                break;
            case AssignDesc.STORE_ASSET_ATTRIBUTE:
                _context.StoreConceptAttributeStoreAssets.Add(new StoreConceptAttributeStoreAsset { StoreAssetId = command.ToId, StoreConceptAttributeId = command.AssignedId });
                break;
            case AssignDesc.STORE_CONCEPT_ATTRIBUTE_ASSET:
                _context.StoreConceptAttributeStoreAssets.Add(new StoreConceptAttributeStoreAsset { StoreConceptAttributeId = command.ToId, StoreAssetId = command.AssignedId });
                break;
            case AssignDesc.TAG_PRODUCT:
                _context.EngageProductTags.Add(new EngageProductTag { EngageMasterProductId = command.ToId, EngageTagId = command.AssignedId });
                break;
            case AssignDesc.DEPT_STORE:
                _context.StoreStoreDepartments.Add(new StoreStoreDepartment { StoreId = command.ToId, StoreDepartmentId = command.AssignedId });
                break;
            case AssignDesc.CONCEPT_LEVEL_STORE:
                _context.StoreConceptLevels.Add(new StoreConceptLevel { StoreId = command.ToId, StoreConceptId = command.AssignedId });
                break;
            case AssignDesc.TYPE_SUPPLIER:
                _context.SupplierSupplierTypes.Add(new SupplierSupplierType { SupplierId = command.ToId, SupplierTypeId = command.AssignedId });
                break;
            case AssignDesc.BRAND_SUPPLIER:
                _context.SupplierEngageBrands.Add(new SupplierEngageBrand { SupplierId = command.ToId, EngageBrandId = command.AssignedId });
                break;
            case AssignDesc.STORE_SURVEY:
                _context.SurveyStores.Add(new SurveyStore { SurveyId = command.ToId, StoreId = command.AssignedId });
                break;
            case AssignDesc.SUPPLIER_ENGAGE_SUB_GROUP:
                _context.EngageSubGroupSuppliers.Add(new EngageSubGroupSupplier { EngageSubGroupId = command.ToId, SupplierId = command.AssignedId });
                break;
            default:
                throw new UnknownAssignMapException(mapping);
        }

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = new
            {
                command.ToId,
                command.AssignedId
            };
            return opStatus;
        }

        return new OperationStatus();
    }
}
