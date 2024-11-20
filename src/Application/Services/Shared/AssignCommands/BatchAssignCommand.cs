namespace Engage.Application.Services.Shared.AssignCommands;

public class BatchAssignCommand : IRequest<OperationStatus>
{
    public string Mapping { get; set; }
    public int ToId { get; set; }
    public List<int> AssignedIds { get; set; }

    public BatchAssignCommand()
    {
    }

    public BatchAssignCommand(string mapping, int toId, List<int> assignedIds)
    {
        Mapping = mapping;
        ToId = toId;
        AssignedIds = assignedIds;
    }
}

public class BatchAssignCommandHandler : IRequestHandler<BatchAssignCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public BatchAssignCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        this._mediator = mediator;
    }

    public async Task<OperationStatus> Handle(BatchAssignCommand command, CancellationToken cancellationToken)
    {
        var mapping = command.Mapping.ToUpper();

        var currentIds = mapping switch
        {
            AssignDesc.WAREHOUSE_DC => _context.Warehouses.Where(e => e.DCId == command.ToId).Select(e => e.WarehouseId).ToList(),
            AssignDesc.CLAIM_REPORT_TYPE_CLAIM_TYPE => _context.ClaimTypeReportTypes.Where(e => e.ClaimTypeId == command.ToId).Select(e => e.ClaimReportTypeId).ToList(),
            AssignDesc.CLAIM_ACCOUNT_MANAGER_SUPPLIER => _context.SupplierClaimAccountManagers.Where(e => e.SupplierId == command.ToId).Select(e => e.UserId).ToList(),
            AssignDesc.CLAIM_CLASSIFICATION_SUPPLIER => _context.SupplierClaimClassifications.Where(e => e.SupplierId == command.ToId).Select(e => e.ClaimClassificationId).ToList(),
            AssignDesc.CLAIM_CLASSIFICATION_TYPE => _context.ClaimClassificationTypes.Where(e => e.ClaimClassificationId == command.ToId).Select(e => e.ClaimTypeId).ToList(),
            AssignDesc.CLAIM_MANAGER_ENGAGE_REGION => _context.EngageRegionClaimManagers.Where(e => e.UserId == command.ToId).Select(e => e.UserId).ToList(),
            AssignDesc.DEPT_DC => _context.DCDepts.Where(e => e.DistributionCenterId == command.ToId).Select(e => e.DCDepartmentId).ToList(),
            AssignDesc.DEPT_EMPLOYEE => _context.EmployeeDepartments.Where(e => e.EmployeeId == command.ToId).Select(e => e.EngageDepartmentId).ToList(),
            AssignDesc.DIVISION_EMPLOYEE => _context.EmployeeEmployeeDivisions.Where(e => e.EmployeeId == command.ToId).Select(e => e.EmployeeDivisionId).ToList(),
            AssignDesc.EMPLOYEE_NOTIFICATION => _context.NotificationEmployees.Where(e => e.NotificationId == command.ToId).Select(e => e.EmployeeId).ToList(),
            AssignDesc.EMPLOYEE_SURVEY => _context.SurveyEmployees.Where(e => e.SurveyId == command.ToId).Select(e => e.EmployeeId).ToList(),
            AssignDesc.ENGAGE_DEPARTMENT_ORDER => _context.OrderEngageDepartments.Where(e => e.OrderId == command.ToId).Select(e => e.EngageDepartmentId).ToList(),
            AssignDesc.PRODUCT_SUPPLIER => _context.SupplierProducts.Where(e => e.SupplierId == command.ToId).Select(e => e.EngageMasterProductId).ToList(),
            AssignDesc.PROJECT_USER => _context.ProjectUsers.Where(e => e.ProjectId == command.ToId).Select(e => e.UserId).ToList(),
            //AssignDesc.PROJECT_TAG => _context.ProjectProjectTags.Where(e => e.ProjectId == command.ToId).Select(e => e.ProjectTagId).ToList(),
            AssignDesc.NOTIFICATION_CHANNEL_NOTIFICATION => _context.NotificationNotificationChannels.Where(e => e.NotificationId == command.ToId).Select(e => e.NotificationChannelId).ToList(),
            AssignDesc.REGION_EMPLOYEE => _context.EmployeeRegions.Where(e => e.EmployeeId == command.ToId).Select(e => e.EngageRegionId).ToList(),
            AssignDesc.HEALTH_CONDITION_EMPLOYEE => _context.EmployeeEmployeeHealthConditions.Where(e => e.EmployeeId == command.ToId).Select(e => e.EmployeeHealthConditionId).ToList(),
            AssignDesc.JOB_TITLE_EMPLOYEE => _context.EmployeeEmployeeJobTitles.Where(e => e.EmployeeId == command.ToId).Select(e => e.EmployeeJobTitleId).ToList(),
            AssignDesc.REGION_NOTIFICATION => _context.NotificationRegions.Where(e => e.NotificationId == command.ToId).Select(e => e.EngageRegionId).ToList(),
            AssignDesc.REGION_SURVEY => _context.SurveyEngageRegions.Where(e => e.SurveyId == command.ToId).Select(e => e.EngageRegionId).ToList(),
            AssignDesc.STORE_ASSET_ATTRIBUTE => _context.StoreConceptAttributeStoreAssets.Where(e => e.StoreAssetId == command.ToId).Select(e => e.StoreConceptAttributeId).ToList(),
            AssignDesc.STORE_CONCEPT_ATTRIBUTE_ASSET => _context.StoreConceptAttributeStoreAssets.Where(e => e.StoreConceptAttributeId == command.ToId).Select(e => e.StoreAssetId).ToList(),
            AssignDesc.TAG_PRODUCT => _context.EngageProductTags.Where(e => e.EngageMasterProductId == command.ToId).Select(e => e.EngageTagId).ToList(),
            AssignDesc.DEPT_STORE => _context.StoreStoreDepartments.Where(e => e.StoreId == command.ToId).Select(e => e.StoreDepartmentId).ToList(),
            AssignDesc.CONCEPT_LEVEL_STORE => _context.StoreConceptLevels.Where(e => e.StoreId == command.ToId).Select(e => e.StoreConceptId).ToList(),
            AssignDesc.TYPE_SUPPLIER => _context.SupplierSupplierTypes.Where(e => e.SupplierId == command.ToId).Select(e => e.SupplierTypeId).ToList(),
            AssignDesc.BRAND_SUPPLIER => _context.SupplierEngageBrands.Where(e => e.SupplierId == command.ToId).Select(e => e.EngageBrandId).ToList(),
            AssignDesc.STORE_SURVEY => _context.SurveyStores.Where(e => e.SurveyId == command.ToId).Select(e => e.StoreId).ToList(),
            AssignDesc.FALSEREASON_SURVEYQUESTION => _context.SurveyQuestionFalseReasons.Where(e => e.SurveyQuestionId == command.ToId).Select(e => e.QuestionFalseReasonId).ToList(),
            AssignDesc.SUPPLIER_ENGAGE_SUB_GROUP => _context.EngageSubGroupSuppliers.Where(e => e.EngageSubGroupId == command.ToId).Select(e => e.SupplierId).ToList(),
            _ => throw new UnknownAssignMapException(mapping),
        };

        // remove currentId's not in assignedIds
        currentIds
            .Where(currentId => !command.AssignedIds.Contains(currentId))
            .ToList()
            .ForEach(currentId =>
            {
                var unassignCommand = new UnassignCommand(
                    command.Mapping, command.ToId, currentId, false);
                _mediator.Send(unassignCommand);
            });

        // add assignedId's not in currentIds
        command.AssignedIds
            .Where(assignedId => !currentIds.Contains(assignedId))
            .ToList()
            .ForEach(assignedId =>
            {
                var assignCommand = new AssignCommand(
                     command.Mapping, command.ToId, assignedId, false);
                _mediator.Send(assignCommand);
            });

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = new
        {
            command.ToId,
            command.AssignedIds
        };
        return opStatus;
    }
}
