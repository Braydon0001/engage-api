namespace Engage.Application.Services.Mobile.SurveyForms.Queries;
public record SurveyFormShelfSpacingTargetedQuery(int EmployeeId, int? StoreId, DateTime Date) : IRequest<SurveyFormAdvancedTargetingVm>
{
}

public class SurveyFormShelfSpacingTargetedHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<SurveyFormShelfSpacingTargetedQuery, SurveyFormAdvancedTargetingVm>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<SurveyFormAdvancedTargetingVm> Handle(SurveyFormShelfSpacingTargetedQuery query, CancellationToken cancellationToken)
    {
        //This whole query's performance can be improved. Look at SurveyFormSupplierStoreHistoryQuery
        var employee = await _context.Employees.Include(e => e.EmployeeJobTitles).ThenInclude(e => e.EmployeeJobTitle)
                                               .Include(e => e.EmployeeRegions).ThenInclude(e => e.EngageRegion)
                                               .Include(e => e.EmployeeDepartments).ThenInclude(e => e.EngageDepartment)
                                               .Include(e => e.EmployeeDivisions).ThenInclude(e => e.EmployeeDivision)
                                               .Include(e => e.EmployeeStores)
                                               .SingleOrDefaultAsync(e => e.EmployeeId == query.EmployeeId, cancellationToken);

        if (employee == null)
        {
            return null;
        }

        var store = await _context.Stores.SingleOrDefaultAsync(e => e.StoreId == query.StoreId, cancellationToken);

        var employeeSubgroups = employee.EmployeeStores.Select(e => e.EngageSubGroupId).ToList();

        var employeeEngageRegionIds = employee.EmployeeRegions.Where(e => e.EngageRegion.Disabled == false)
                                                                  .Select(e => e.EngageRegionId)
                                                                  .ToList();

        var engageDepartmentIds = employee.EmployeeDepartments.Where(e => e.EngageDepartment.Disabled == false)
                                                              .Select(e => e.EngageDepartmentId)
                                                              .ToList();

        var jobTitleIds = employee.EmployeeJobTitles.Where(e => e.EmployeeJobTitle.Disabled == false
                                                        && e.EmployeeJobTitle.Level == 3)
                                                    .Select(e => e.EmployeeJobTitleId)
                                                    .ToList();

        var employeeDivisionIds = employee.EmployeeDivisions.Where(e => e.EmployeeDivision.Disabled == false)
                                                            .Select(e => e.EmployeeDivisionId)
                                                            .ToList();

        //all surveys that have targeting applied
        var targetedSurveyForms = await _context.SurveyFormTargets.Where(e => e.SurveyForm.Disabled == false && e.SurveyForm.IsDisabled == false &&
                                                                              query.Date.Date >= e.SurveyForm.StartDate.Value.Date
                                                                              && (!e.SurveyForm.EndDate.HasValue || query.Date.Date <= e.SurveyForm.EndDate.Value.Date))
                                                                  .Distinct()
                                                                  .ToListAsync(cancellationToken);

        var targetedSurveyFormIds = targetedSurveyForms.Select(e => e.SurveyFormId).Distinct().ToList();

        var nonTargetedSurveyFormIds = await _context.SurveyForms.Include(e => e.SurveyFormType).Where(e => !targetedSurveyFormIds.Contains(e.SurveyFormId)
                                                                               && e.Disabled == false && e.IsDisabled == false
                                                                               && DateTime.Now.Date >= e.StartDate.Value.Date
                                                                               && (!e.EndDate.HasValue || DateTime.Now.Date <= e.EndDate.Value.Date))
                                                                .Select(e => e.SurveyFormId)
                                                                .ToListAsync(cancellationToken);

        //surrveyIds of surveys that exclude this employee or store
        var excludedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormExcludedEmployee>()
                                                       .Where(e => e.ExcludedEmployeeId == query.EmployeeId)
                                                       .Select(e => e.SurveyFormId)
                                                       .Distinct()
                                                       .ToList();

        if (query.StoreId != null && query.StoreId > 0)
        {
            excludedSurveyFormIds.AddRange(targetedSurveyForms.OfType<SurveyFormExcludedStore>()
                                                              .Where(e => e.ExcludedStoreId == query.StoreId)
                                                              .Select(e => e.SurveyFormId)
                                                              .Distinct()
            .ToList());
        }

        var employeeTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployee>().Select(e => e.SurveyFormId).ToList();
        var employeeEngageRegionTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployeeEngageRegion>().Select(e => e.SurveyFormId).ToList();
        var engageDepartmentTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEngageDepartment>().Select(e => e.SurveyFormId).ToList();
        var employeeJobTitleTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployeeJobTitle>().Select(e => e.SurveyFormId).ToList();
        var employeeDivisionTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployeeDivision>().Select(e => e.SurveyFormId).ToList();

        //employee targeted surveys
        var employeeTargetingSurveyFormIds = employeeTargetedSurveyFormIds.Union(employeeEngageRegionTargetedSurveyFormIds)
                                                                          .Union(employeeEngageRegionTargetedSurveyFormIds)
                                                                          .Union(engageDepartmentTargetedSurveyFormIds)
                                                                          .Union(employeeJobTitleTargetedSurveyFormIds)
                                                                          .Union(employeeDivisionTargetedSurveyFormIds)
                                                                          .Distinct()
                                                                          .ToList();

        var storeTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStore>().Select(e => e.SurveyFormId).ToList();
        var storeEngageRegionTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStoreEngageRegion>().Select(e => e.SurveyFormId).ToList();
        var storeClusterTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStoreCluster>().Select(e => e.SurveyFormId).ToList();
        var storeFormatTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStoreFormat>().Select(e => e.SurveyFormId).ToList();
        var storeLSMTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStoreLSM>().Select(e => e.SurveyFormId).ToList();
        var storeTypeTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStoreType>().Select(e => e.SurveyFormId).ToList();

        //store targeted surveys
        var storeTargetingSurveyFormIds = storeTargetedSurveyFormIds.Union(storeEngageRegionTargetedSurveyFormIds)
                                                                    .Union(storeClusterTargetedSurveyFormIds)
                                                                    .Union(storeFormatTargetedSurveyFormIds)
                                                                    .Union(storeLSMTargetedSurveyFormIds)
                                                                    .Union(storeTypeTargetedSurveyFormIds)
                                                                    .Distinct()
                                                                    .ToList();

        //surveys that have employee and store targeting
        var combinedTargetingSurveyFormIds = employeeTargetingSurveyFormIds.Intersect(storeTargetingSurveyFormIds).Distinct().ToList();

        //employee-only targeted surveys
        var employeeOnlyTargetedSurveyFormIds = employeeTargetingSurveyFormIds.Where(e => !combinedTargetingSurveyFormIds.Contains(e)).Distinct().ToList();

        //store-only targeted surveys
        var storeOnlyTargetedSurveyFormIds = storeTargetingSurveyFormIds.Where(e => !combinedTargetingSurveyFormIds.Contains(e)).Distinct().ToList();

        var targetingIdVm = new SurveyFormAdvancedTargetingIdVm()
        {
            Employees = [employee.EmployeeId],
            EmployeeEngageRegions = employeeEngageRegionIds,
            EmployeeJobTitles = jobTitleIds,
            EmployeeEngageDepartments = engageDepartmentIds,
            EmployeeDivisions = employeeDivisionIds,
            Stores = store != null ? [store.StoreId] : [],
            StoreEngageRegions = store != null ? [store.EngageRegionId] : [],
            StoreFormats = store != null ? [store.StoreFormatId] : [],
            StoreClusters = store != null ? [store.StoreClusterId] : [],
            StoreLSMs = store != null ? [store.StoreLSMId] : [],
            StoreTypes = store != null ? [store.StoreTypeId] : [],
            SurveyFormIds = []
        };

        var employeeOnlyTargetedSurveyForms = targetedSurveyForms.Where(e => employeeOnlyTargetedSurveyFormIds.Contains(e.SurveyFormId)).GroupBy(e => e.SurveyFormId).ToList();

        foreach (var surveyTarget in employeeOnlyTargetedSurveyForms)
        {
            var hasEmployeeTarget = surveyTarget.OfType<SurveyFormEmployee>().Any();
            var hasRegionTarget = surveyTarget.OfType<SurveyFormEmployeeEngageRegion>().Any();
            var hasDepartmentTarget = surveyTarget.OfType<SurveyFormEngageDepartment>().Any();
            var hasJobTitleTarget = surveyTarget.OfType<SurveyFormEmployeeJobTitle>().Any();
            var hasDivisionTarget = surveyTarget.OfType<SurveyFormEmployeeDivision>().Any();

            var hasCriteriaTarget = hasRegionTarget || hasDepartmentTarget || hasJobTitleTarget || hasDivisionTarget;

            if ((hasEmployeeTarget ? surveyTarget.OfType<SurveyFormEmployee>().Select(e => e.EmployeeId).Contains(targetingIdVm.Employees[0]) : false)
                                                || (hasCriteriaTarget
                                                    ? ((hasRegionTarget ? surveyTarget.OfType<SurveyFormEmployeeEngageRegion>().Select(e => e.EmployeeEngageRegionId).Any(x => targetingIdVm.EmployeeEngageRegions.Contains(x)) : true)
                                                        && (hasDepartmentTarget ? surveyTarget.OfType<SurveyFormEngageDepartment>().Select(e => e.EngageDepartmentId).Any(x => targetingIdVm.EmployeeEngageDepartments.Contains(x)) : true)
                                                        && (hasJobTitleTarget ? surveyTarget.OfType<SurveyFormEmployeeJobTitle>().Select(e => e.EmployeeJobTitleId).Any(x => targetingIdVm.EmployeeJobTitles.Contains(x)) : true)
                                                        && (hasDivisionTarget ? surveyTarget.OfType<SurveyFormEmployeeDivision>().Select(e => e.EmployeeDivisionId).Any(x => targetingIdVm.EmployeeDivisions.Contains(x)) : true))
                                                    : false
                                                    ))
            {

                targetingIdVm.SurveyFormIds.AddIfNotContains(surveyTarget.Key);
                var rules = surveyTarget.Select(e => e.SurveyForm.Rules).FirstOrDefault();
                if (rules != null)
                {
                    targetingIdVm.HasAdvancedTargeting = targetingIdVm.HasAdvancedTargeting || rules.Any(r => r.Type == "TargetRule");
                }

            }
        }

        if (store != null)
        {
            var storeOnlyTargetedSurveyForms = targetedSurveyForms.Where(e => storeOnlyTargetedSurveyFormIds.Contains(e.SurveyFormId)).GroupBy(e => e.SurveyFormId).ToList();

            foreach (var surveyTarget in storeOnlyTargetedSurveyForms)
            {
                var hasStoreTarget = surveyTarget.OfType<SurveyFormStore>().Any();
                var hasRegionTarget = surveyTarget.OfType<SurveyFormStoreEngageRegion>().Any();
                var hasClusterTarget = surveyTarget.OfType<SurveyFormStoreCluster>().Any();
                var hasFormatTarget = surveyTarget.OfType<SurveyFormStoreFormat>().Any();
                var hasLSMTarget = surveyTarget.OfType<SurveyFormStoreLSM>().Any();
                var hasTypeTarget = surveyTarget.OfType<SurveyFormStoreType>().Any();

                var hasCriteriaTarget = hasRegionTarget || hasClusterTarget || hasFormatTarget || hasLSMTarget || hasTypeTarget;

                if ((hasStoreTarget ? surveyTarget.OfType<SurveyFormStore>().Select(e => e.StoreId).Contains(targetingIdVm.Stores[0]) : false)
                                                || (hasCriteriaTarget
                                                    ? ((hasRegionTarget ? surveyTarget.OfType<SurveyFormStoreEngageRegion>().Select(e => e.StoreEngageRegionId).Contains(targetingIdVm.StoreEngageRegions[0]) : true)
                                                        && (hasClusterTarget ? surveyTarget.OfType<SurveyFormStoreCluster>().Select(e => e.StoreClusterId).Contains(targetingIdVm.StoreClusters[0]) : true)
                                                        && (hasFormatTarget ? surveyTarget.OfType<SurveyFormStoreFormat>().Select(e => e.StoreFormatId).Contains(targetingIdVm.StoreFormats[0]) : true)
                                                        && (hasLSMTarget ? surveyTarget.OfType<SurveyFormStoreLSM>().Select(e => e.StoreLSMId).Contains(targetingIdVm.StoreLSMs[0]) : true)
                                                        && (hasTypeTarget ? surveyTarget.OfType<SurveyFormStoreType>().Select(e => e.StoreTypeId).Contains(targetingIdVm.StoreTypes[0]) : true))
                                                    : false
                                                    ))
                {
                    targetingIdVm.SurveyFormIds.AddIfNotContains(surveyTarget.Key);
                    var rules = surveyTarget.Select(e => e.SurveyForm.Rules).FirstOrDefault();
                    if (rules != null)
                    {
                        targetingIdVm.HasAdvancedTargeting = targetingIdVm.HasAdvancedTargeting || rules.Any(r => r.Type == "TargetRule");
                    }
                }
            }

            var combinedTargetedSurveyForms = targetedSurveyForms.Where(e => combinedTargetingSurveyFormIds.Contains(e.SurveyFormId)).GroupBy(e => e.SurveyFormId).ToList();

            foreach (var surveyTarget in combinedTargetedSurveyForms)
            {
                var hasEmployeeTarget = surveyTarget.OfType<SurveyFormEmployee>().Any();
                var hasEmployeeRegionTarget = surveyTarget.OfType<SurveyFormEmployeeEngageRegion>().Any();
                var hasDepartmentTarget = surveyTarget.OfType<SurveyFormEngageDepartment>().Any();
                var hasJobTitleTarget = surveyTarget.OfType<SurveyFormEmployeeJobTitle>().Any();
                var hasDivisionTarget = surveyTarget.OfType<SurveyFormEmployeeDivision>().Any();

                var hasEmployeeCriteriaTarget = hasEmployeeRegionTarget || hasDepartmentTarget || hasJobTitleTarget || hasDivisionTarget;

                var hasStoreTarget = surveyTarget.OfType<SurveyFormStore>().Any();
                var hasStoreRegionTarget = surveyTarget.OfType<SurveyFormStoreEngageRegion>().Any();
                var hasClusterTarget = surveyTarget.OfType<SurveyFormStoreCluster>().Any();
                var hasFormatTarget = surveyTarget.OfType<SurveyFormStoreFormat>().Any();
                var hasLSMTarget = surveyTarget.OfType<SurveyFormStoreLSM>().Any();
                var hasTypeTarget = surveyTarget.OfType<SurveyFormStoreType>().Any();

                var hasStoreCriteriaTarget = hasStoreRegionTarget || hasClusterTarget || hasFormatTarget || hasLSMTarget || hasTypeTarget;

                if (((hasEmployeeTarget ? surveyTarget.OfType<SurveyFormEmployee>().Select(e => e.EmployeeId).Contains(targetingIdVm.Employees[0]) : false)
                                                || (hasEmployeeCriteriaTarget
                                                    ? ((hasEmployeeRegionTarget ? surveyTarget.OfType<SurveyFormEmployeeEngageRegion>().Select(e => e.EmployeeEngageRegionId).Any(x => targetingIdVm.EmployeeEngageRegions.Contains(x)) : true)
                                                        && (hasDepartmentTarget ? surveyTarget.OfType<SurveyFormEngageDepartment>().Select(e => e.EngageDepartmentId).Any(x => targetingIdVm.EmployeeEngageDepartments.Contains(x)) : true)
                                                        && (hasJobTitleTarget ? surveyTarget.OfType<SurveyFormEmployeeJobTitle>().Select(e => e.EmployeeJobTitleId).Any(x => targetingIdVm.EmployeeJobTitles.Contains(x)) : true)
                                                        && (hasDivisionTarget ? surveyTarget.OfType<SurveyFormEmployeeDivision>().Select(e => e.EmployeeDivisionId).Any(x => targetingIdVm.EmployeeDivisions.Contains(x)) : true))
                                                    : false
                                                    ))
                                   && ((hasStoreTarget ? surveyTarget.OfType<SurveyFormStore>().Select(e => e.StoreId).Contains(targetingIdVm.Stores[0]) : false)
                                                || (hasStoreCriteriaTarget
                                                    ? ((hasStoreRegionTarget ? surveyTarget.OfType<SurveyFormStoreEngageRegion>().Select(e => e.StoreEngageRegionId).Contains(targetingIdVm.StoreEngageRegions[0]) : true)
                                                        && (hasClusterTarget ? surveyTarget.OfType<SurveyFormStoreCluster>().Select(e => e.StoreClusterId).Contains(targetingIdVm.StoreClusters[0]) : true)
                                                        && (hasFormatTarget ? surveyTarget.OfType<SurveyFormStoreFormat>().Select(e => e.StoreFormatId).Contains(targetingIdVm.StoreFormats[0]) : true)
                                                        && (hasLSMTarget ? surveyTarget.OfType<SurveyFormStoreLSM>().Select(e => e.StoreLSMId).Contains(targetingIdVm.StoreLSMs[0]) : true)
                                                        && (hasTypeTarget ? surveyTarget.OfType<SurveyFormStoreType>().Select(e => e.StoreTypeId).Contains(targetingIdVm.StoreTypes[0]) : true))
                                                    : false
                                                    )))
                {
                    targetingIdVm.SurveyFormIds.AddIfNotContains(surveyTarget.Key);
                    var rules = surveyTarget.Select(e => e.SurveyForm.Rules).FirstOrDefault();
                    if (rules != null)
                    {
                        targetingIdVm.HasAdvancedTargeting = targetingIdVm.HasAdvancedTargeting || rules.Any(r => r.Type == "TargetRule");
                    }
                }
            }
        }

        targetingIdVm.SurveyFormIds.AddRangeIfNotContains(nonTargetedSurveyFormIds.ToArray());

        var queryable = _context.SurveyForms.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SurveyFormProducts)
                             .Include(e => e.SurveyFormType)
                             .Include(e => e.Supplier)
                             .Include(e => e.EngageSubGroup)
                             .Include(e => e.EngageBrand)
                             .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Count != 0))
                                 .ThenInclude(e => e.SurveyFormQuestionGroupProducts)
                                    .ThenInclude(e => e.EngageVariantProduct)
                             .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Count != 0))
                                 .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                     .ThenInclude(e => e.SurveyFormQuestionProducts)
                                         .ThenInclude(e => e.EngageVariantProduct)
                             .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Count != 0))
                                 .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                     .ThenInclude(e => e.SurveyFormQuestionOptions)
                                         .ThenInclude(e => e.SurveyFormOption)
                             .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Count != 0))
                                 .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                     .ThenInclude(e => e.SurveyFormQuestionReasons)
                                         .ThenInclude(e => e.SurveyFormReason)
                             .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Count != 0))
                                 .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                     .ThenInclude(e => e.SurveyFormQuestionType);

        var entites = await queryable.Where(e => e.Disabled == false && e.Deleted == false && e.IsDisabled == false
                                                 // surveys which are still active
                                                 && query.Date.Date >= e.StartDate.Value.Date && (!e.EndDate.HasValue || query.Date.Date <= e.EndDate.Value.Date)
                                                 // only include surveys that have questions
                                                 && e.SurveyFormQuestionGroups.Any(e => e.SurveyFormQuestions.Count > 0)
                                                 // only get the "normal" surveys
                                                 && (e.SurveyFormType.Name == "Shelf Spacing")
                                                 // only get the surveys that are set to ignore user subgroups or the ones that align with the user subgroups
                                                 && (
                                                     (employeeTargetingSurveyFormIds.Contains(e.SurveyFormId))
                                                     || (e.IgnoreSubgroup || !e.EngageSubgroupId.HasValue)
                                                     || (!e.IgnoreSubgroup && e.EngageSubgroupId.HasValue && employeeSubgroups.Contains(e.EngageSubgroupId.Value)))
                                                 // all the surveys that have targeting applied and target the user and/or store
                                                 && ((targetedSurveyFormIds.Contains(e.SurveyFormId) && targetingIdVm.SurveyFormIds.Contains(e.SurveyFormId))
                                                 // Include all surveys which are not targted
                                                 || !targetedSurveyFormIds.Contains(e.SurveyFormId))

                                            )
                                     .Where(e => !excludedSurveyFormIds.Contains(e.SurveyFormId))
                                     .ToListAsync(cancellationToken);


        foreach (var survey in entites)
        {
            foreach (var group in survey.SurveyFormQuestionGroups)
            {
                group.SurveyFormQuestions = group.SurveyFormQuestions.Where(e => e.Disabled == false && e.Deleted == false).ToList();
            }
        }

        var surveyForms = _mapper.Map<List<SurveyFormWithQuestions>>(entites);

        var targetingVm = new SurveyFormAdvancedTargetingVm()
        {
            Employees = [employee.EmployeeId],
            EmployeeEngageRegions = employeeEngageRegionIds,
            EmployeeJobTitles = jobTitleIds,
            EmployeeEngageDepartments = engageDepartmentIds,
            EmployeeDivisions = employeeDivisionIds,
            Stores = store != null ? [store.StoreId] : [],
            StoreEngageRegions = store != null ? [store.EngageRegionId] : [],
            StoreFormats = store != null ? [store.StoreFormatId] : [],
            StoreClusters = store != null ? [store.StoreClusterId] : [],
            StoreLSMs = store != null ? [store.StoreLSMId] : [],
            StoreTypes = store != null ? [store.StoreTypeId] : [],
            SurveyForms = new ListResult<SurveyFormWithQuestions>(surveyForms)
        };

        return targetingVm;
    }
}