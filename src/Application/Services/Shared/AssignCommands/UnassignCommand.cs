namespace Engage.Application.Services.Shared.AssignCommands;

public class UnassignCommand : AssignCommand, IRequest<OperationStatus>
{
    public UnassignCommand() { }

    public UnassignCommand(string mapping, int assigneeid, int toid, bool saveChanges = true) :
        base(mapping, assigneeid, toid, saveChanges)
    {
    }
}

public class UnassignCommandHandler : IRequestHandler<UnassignCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public UnassignCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(UnassignCommand command, CancellationToken cancellationToken)
    {
        var mapping = command.Mapping.ToUpper();

        switch (mapping)
        {
            case AssignDesc.WAREHOUSE_DC:
                var warehouse = _context.Warehouses.FirstOrDefault(e => e.DCId == command.ToId && e.WarehouseId == command.AssignedId);
                if (warehouse == null)
                    throw new NotFoundException(AssignDesc.WAREHOUSE_DC, command.AssignedId);
                _context.Warehouses.Remove(warehouse);
                break;
            case AssignDesc.CLAIM_REPORT_TYPE_CLAIM_TYPE:
                var claimTypeReportType = _context.ClaimTypeReportTypes.FirstOrDefault(e => e.ClaimTypeId == command.ToId && e.ClaimReportTypeId == command.AssignedId);
                if (claimTypeReportType == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.ClaimTypeReportTypes.Remove(claimTypeReportType);
                break;
            case AssignDesc.CLAIM_ACCOUNT_MANAGER_SUPPLIER:
                var supplierClaimAccountManager = _context.SupplierClaimAccountManagers.FirstOrDefault(e => e.SupplierId == command.ToId && e.UserId == command.AssignedId);
                if (supplierClaimAccountManager == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.SupplierClaimAccountManagers.Remove(supplierClaimAccountManager);
                break;
            case AssignDesc.CLAIM_CLASSIFICATION_SUPPLIER:
                var supplierClaimClassification = _context.SupplierClaimClassifications.FirstOrDefault(e => e.SupplierId == command.ToId && e.ClaimClassificationId == command.AssignedId);
                if (supplierClaimClassification == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.SupplierClaimClassifications.Remove(supplierClaimClassification);
                break;
            case AssignDesc.CLAIM_CLASSIFICATION_TYPE:
                var claimClassificationType = _context.ClaimClassificationTypes.FirstOrDefault(e => e.ClaimClassificationId == command.ToId && e.ClaimTypeId == command.AssignedId);
                if (claimClassificationType == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.ClaimClassificationTypes.Remove(claimClassificationType);
                break;
            case AssignDesc.CLAIM_MANAGER_ENGAGE_REGION:
                var engageRegionClaimManager = _context.EngageRegionClaimManagers.FirstOrDefault(e => e.EngageRegionId == command.ToId && e.UserId == command.AssignedId);
                if (engageRegionClaimManager == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.EngageRegionClaimManagers.Remove(engageRegionClaimManager);
                break;
            case AssignDesc.DEPT_DC:
                var dcDept = _context.DCDepts.FirstOrDefault(e => e.DistributionCenterId == command.ToId && e.DCDepartmentId == command.AssignedId);
                if (dcDept == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.DCDepts.Remove(dcDept);
                break;
            case AssignDesc.DEPT_EMPLOYEE:
                var employeeDepartment = _context.EmployeeDepartments.FirstOrDefault(e => e.EmployeeId == command.ToId && e.EngageDepartmentId == command.AssignedId);
                if (employeeDepartment == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.EmployeeDepartments.Remove(employeeDepartment);
                break;
            case AssignDesc.DIVISION_EMPLOYEE:
                var employeeDivision = _context.EmployeeEmployeeDivisions.FirstOrDefault(e => e.EmployeeId == command.ToId && e.EmployeeDivisionId == command.AssignedId);
                if (employeeDivision == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.EmployeeEmployeeDivisions.Remove(employeeDivision);
                break;
            case AssignDesc.PROJECT_USER:
                var projectUser = _context.ProjectUsers.FirstOrDefault(e => e.ProjectId == command.ToId && e.UserId == command.AssignedId);
                if (projectUser == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.ProjectUsers.Remove(projectUser);
                break;
            //case AssignDesc.PROJECT_TAG:
            //    var projectTag = _context.ProjectProjectTags.FirstOrDefault(e => e.ProjectId == command.ToId && e.ProjectTagId == command.AssignedId);
            //    if (projectTag == null)
            //        throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
            //    _context.ProjectProjectTags.Remove(projectTag);
            //    break;
            case AssignDesc.HEALTH_CONDITION_EMPLOYEE:
                var employeeEmployeeHealthCondition = _context.EmployeeEmployeeHealthConditions.FirstOrDefault(e => e.EmployeeId == command.ToId && e.EmployeeHealthConditionId == command.AssignedId);
                if (employeeEmployeeHealthCondition == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.EmployeeEmployeeHealthConditions.Remove(employeeEmployeeHealthCondition);
                break;
            case AssignDesc.JOB_TITLE_EMPLOYEE:
                var employeeEmployeeJobTitle = _context.EmployeeEmployeeJobTitles.FirstOrDefault(e => e.EmployeeId == command.ToId && e.EmployeeJobTitleId == command.AssignedId);
                if (employeeEmployeeJobTitle == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.EmployeeEmployeeJobTitles.Remove(employeeEmployeeJobTitle);
                break;
            case AssignDesc.STORE_ASSET_ATTRIBUTE:
                var storeAssetStoreConceptAttribute = _context.StoreConceptAttributeStoreAssets.FirstOrDefault(e => e.StoreAssetId == command.ToId && e.StoreConceptAttributeId == command.AssignedId);
                if (storeAssetStoreConceptAttribute == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.StoreConceptAttributeStoreAssets.Remove(storeAssetStoreConceptAttribute);
                break;
            case AssignDesc.STORE_CONCEPT_ATTRIBUTE_ASSET:
                var storeConceptAttributeStoreAsset = _context.StoreConceptAttributeStoreAssets.FirstOrDefault(e => e.StoreConceptAttributeId == command.ToId && e.StoreAssetId == command.AssignedId);
                if (storeConceptAttributeStoreAsset == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.StoreConceptAttributeStoreAssets.Remove(storeConceptAttributeStoreAsset);
                break;
            case AssignDesc.EMPLOYEE_NOTIFICATION:
                var employeeNotifications = _context.NotificationEmployees.FirstOrDefault(e => e.NotificationId == command.ToId && e.EmployeeId == command.AssignedId);
                if (employeeNotifications == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.NotificationEmployees.Remove(employeeNotifications);
                break;
            case AssignDesc.EMPLOYEE_SURVEY:
                var employeeSurveys = _context.SurveyEmployees.FirstOrDefault(e => e.SurveyId == command.ToId && e.EmployeeId == command.AssignedId);
                if (employeeSurveys == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.SurveyEmployees.Remove(employeeSurveys);
                break;
            case AssignDesc.ENGAGE_DEPARTMENT_ORDER:
                var engageDepartmentOrder = _context.OrderEngageDepartments.FirstOrDefault(e => e.OrderId == command.ToId && e.EngageDepartmentId == command.AssignedId);
                if (engageDepartmentOrder == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.OrderEngageDepartments.Remove(engageDepartmentOrder);
                break;
            case AssignDesc.PRODUCT_SUPPLIER:
                var productSupplier = _context.SupplierProducts.FirstOrDefault(e => e.SupplierId == command.ToId && e.EngageMasterProductId == command.AssignedId);
                if (productSupplier == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.SupplierProducts.Remove(productSupplier);
                break;
            case AssignDesc.NOTIFICATION_CHANNEL_NOTIFICATION:
                var notificationChannelNotification = _context.NotificationNotificationChannels.FirstOrDefault(e => e.NotificationId == command.ToId && e.NotificationChannelId == command.AssignedId);
                if (notificationChannelNotification == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.NotificationNotificationChannels.Remove(notificationChannelNotification);
                break;
            case AssignDesc.REGION_EMPLOYEE:
                var employeeRegions = _context.EmployeeRegions.FirstOrDefault(e => e.EmployeeId == command.ToId && e.EngageRegionId == command.AssignedId);
                if (employeeRegions == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.EmployeeRegions.Remove(employeeRegions);
                break;
            case AssignDesc.REGION_NOTIFICATION:
                var notificationRegions = _context.NotificationRegions.FirstOrDefault(e => e.NotificationId == command.ToId && e.EngageRegionId == command.AssignedId);
                if (notificationRegions == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.NotificationRegions.Remove(notificationRegions);
                break;
            case AssignDesc.REGION_SURVEY:
                var surveyRegions = _context.SurveyEngageRegions.FirstOrDefault(e => e.SurveyId == command.ToId && e.EngageRegionId == command.AssignedId);
                if (surveyRegions == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.SurveyEngageRegions.Remove(surveyRegions);
                break;
            case AssignDesc.TAG_PRODUCT:
                var engageProductTags = _context.EngageProductTags.FirstOrDefault(e => e.EngageMasterProductId == command.ToId && e.EngageTagId == command.AssignedId);
                if (engageProductTags == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.EngageProductTags.Remove(engageProductTags);
                break;
            case AssignDesc.DEPT_STORE:
                var storeStoreDepartments = _context.StoreStoreDepartments.FirstOrDefault(e => e.StoreId == command.ToId && e.StoreDepartmentId == command.AssignedId);
                if (storeStoreDepartments == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.StoreStoreDepartments.Remove(storeStoreDepartments);
                break;
            case AssignDesc.CONCEPT_LEVEL_STORE:
                var storeConceptLevels = _context.StoreConceptLevels.FirstOrDefault(e => e.StoreId == command.ToId && e.StoreConceptId == command.AssignedId);
                if (storeConceptLevels == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.StoreConceptLevels.Remove(storeConceptLevels);
                break;
            case AssignDesc.TYPE_SUPPLIER:
                var supplierSupplierTypes = _context.SupplierSupplierTypes.FirstOrDefault(e => e.SupplierId == command.ToId && e.SupplierTypeId == command.AssignedId);
                if (supplierSupplierTypes == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.SupplierSupplierTypes.Remove(supplierSupplierTypes);
                break;
            case AssignDesc.BRAND_SUPPLIER:
                var supplierEngageBrands = _context.SupplierEngageBrands.FirstOrDefault(e => e.SupplierId == command.ToId && e.EngageBrandId == command.AssignedId);
                if (supplierEngageBrands == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.SupplierEngageBrands.Remove(supplierEngageBrands);
                break;
            case AssignDesc.STORE_SURVEY:
                var surveyStores = _context.SurveyStores.FirstOrDefault(e => e.SurveyId == command.ToId && e.StoreId == command.AssignedId);
                if (surveyStores == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.SurveyStores.Remove(surveyStores);
                break;
            case AssignDesc.FALSEREASON_SURVEYQUESTION:
                var falseReasons = _context.SurveyQuestionFalseReasons.FirstOrDefault(e => e.SurveyQuestionId == command.ToId && e.QuestionFalseReasonId == command.AssignedId);
                if (falseReasons == null)
                    throw new NotFoundAssignException(mapping, command.ToId, command.AssignedId);
                _context.SurveyQuestionFalseReasons.Remove(falseReasons);
                break;
            case AssignDesc.SUPPLIER_ENGAGE_SUB_GROUP:
                var engageSubGroupSuppliers = _context.EngageSubGroupSuppliers.Single(e => e.EngageSubGroupId == command.ToId && e.SupplierId == command.AssignedId);
                _context.EngageSubGroupSuppliers.Remove(engageSubGroupSuppliers);
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
