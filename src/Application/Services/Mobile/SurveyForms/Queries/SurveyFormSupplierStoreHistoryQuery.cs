namespace Engage.Application.Services.Mobile.SurveyForms.Queries;

public class SurveyFormSupplierStoreHistoryQuery : IRequest<SurveyFormMobileOfflineDto>
{
    public int EmployeeId { get; set; }
    public List<string> SurveyTypes { get; set; }
}

public record SurveyFormSupplierStoreHistoryHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IUserService User) : IRequestHandler<SurveyFormSupplierStoreHistoryQuery, SurveyFormMobileOfflineDto>
{
    public async Task<SurveyFormMobileOfflineDto> Handle(SurveyFormSupplierStoreHistoryQuery query, CancellationToken cancellationToken)
    {
        var employee = await Context.Employees.Include(e => e.EmployeeJobTitles).ThenInclude(e => e.EmployeeJobTitle)
                                               .Include(e => e.EmployeeRegions).ThenInclude(e => e.EngageRegion)
                                               .Include(e => e.EmployeeDepartments).ThenInclude(e => e.EngageDepartment)
                                               .Include(e => e.EmployeeDivisions).ThenInclude(e => e.EmployeeDivision)
                                               .Include(e => e.EmployeeStores)
                                               .SingleOrDefaultAsync(e => e.EmployeeId == query.EmployeeId, cancellationToken);

        if (employee == null)
        {
            return null;
        }

        var storeIds = new List<int>();

        var employeeStoreIds = await Context.EmployeeStores.Where(e => e.EmployeeId == query.EmployeeId).Select(s => s.StoreId).ToListAsync(cancellationToken);

        storeIds.AddRange(employeeStoreIds);

        var employeeSurveyHistoryStoreIds = await Context.SurveyFormSubmissions.Where(e => e.EmployeeId == query.EmployeeId).Select(s => s.StoreId.Value).ToListAsync(cancellationToken);

        storeIds.AddRange(employeeSurveyHistoryStoreIds);

        var employeeOrderHistoryStoreIds = await Context.Orders.Where(e => e.CreatedBy == User.UserName).Select(s => s.StoreId).ToListAsync(cancellationToken);

        storeIds.AddRange(employeeOrderHistoryStoreIds);

        storeIds = storeIds.Distinct().Order().ToList();

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

        var stores = await Context.Stores.Include(e => e.EngageRegion)
                                                       .Include(e => e.StoreCluster)
                                                       .Include(e => e.StoreFormat)
                                                       .Include(e => e.StoreLSM)
                                                       .Where(e => storeIds.Contains(e.StoreId))
                                                       .ToListAsync(cancellationToken);

        //all surveys that have targeting applied
        var targetedSurveyForms = await Context.SurveyFormTargets.Include(e => e.SurveyForm).ThenInclude(e => e.SurveyFormType)
                                                                 .Where(e => e.SurveyForm.Disabled == false && e.SurveyForm.IsDisabled == false
                                                                              && DateTime.Now.Date >= e.SurveyForm.StartDate.Value.Date
                                                                              && (!e.SurveyForm.EndDate.HasValue || DateTime.Now.Date <= e.SurveyForm.EndDate.Value.Date)
                                                                              && query.SurveyTypes.Contains(e.SurveyForm.SurveyFormType.Name)
                                                                              && e.SurveyForm.SurveyFormQuestionGroups.Any(g => g.SurveyFormQuestions.Count != 0)
                                                                              && e.SurveyForm.IsTemplate == false)
                                                                 .Distinct()
                                                                 .ToListAsync(cancellationToken);

        //surrveyIds of surveys that exclude this employee or store
        var excludedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormExcludedEmployee>()
                                                       .Where(e => e.ExcludedEmployeeId == query.EmployeeId)
                                                       .Select(e => e.SurveyFormId)
                                                       .Distinct()
                                                       .ToList();

        var targetedSurveyFormIds = targetedSurveyForms.Select(e => e.SurveyFormId).Distinct().ToList();

        //store-recurring surveys
        var storeRecurringSurveyFormIds = await Context.SurveyForms.Include(e => e.SurveyFormType)
                                                                   .Where(e => e.IsStoreRecurring == true
                                                                                      && e.Disabled == false && e.IsDisabled == false
                                                                                      && DateTime.Now.Date >= e.StartDate.Value.Date
                                                                                      && (!e.EndDate.HasValue || DateTime.Now.Date <= e.EndDate.Value.Date)
                                                                                      && query.SurveyTypes.Contains(e.SurveyFormType.Name)
                                                                                      && e.SurveyFormQuestionGroups.Any(g => g.SurveyFormQuestions.Count != 0)
                                                                                      && e.IsTemplate == false)
                                                                   .Select(e => e.SurveyFormId)
                                                                   .ToListAsync(cancellationToken);

        var completedSurveys = await Context.SurveyFormSubmissions.Include(e => e.SurveyForm).Where(e => e.IsComplete
                                                                         //get the surveys completed by the user
                                                                         && (e.EmployeeId == query.EmployeeId
                                                                              //or the store recurring surveys completed by others
                                                                              || storeRecurringSurveyFormIds.Contains(e.SurveyFormId))
                                                                         && e.CompletedDate.HasValue && (e.SurveyForm.IsRecurring ? e.CompletedDate.Value.Date == DateTime.Now.Date : e.CompletedDate.Value.Date <= DateTime.Now.Date))
                                                                   .ToListAsync(cancellationToken);

        //non-targeted surveys
        var nonTargetedSurveyFormIds = await Context.SurveyForms.Include(e => e.SurveyFormType).Where(e => !targetedSurveyFormIds.Contains(e.SurveyFormId)
                                                                               && e.Disabled == false && e.IsDisabled == false
                                                                               && DateTime.Now.Date >= e.StartDate.Value.Date
                                                                               && (!e.EndDate.HasValue || DateTime.Now.Date <= e.EndDate.Value.Date)
                                                                               && query.SurveyTypes.Contains(e.SurveyFormType.Name)
                                                                               && e.SurveyFormQuestionGroups.Any(g => g.SurveyFormQuestions.Count != 0)
                                                                               && e.IsTemplate == false)
                                                                .Select(e => e.SurveyFormId)
                                                                .ToListAsync(cancellationToken);

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

        var surveyFormIds = new List<int>();

        var targetingVm = stores.Select(e => new SurveyFormAdvancedTargetingIdVm()
        {
            Employees = [employee.EmployeeId],
            EmployeeEngageRegions = employeeEngageRegionIds,
            EmployeeJobTitles = jobTitleIds,
            EmployeeEngageDepartments = engageDepartmentIds,
            EmployeeDivisions = employeeDivisionIds,
            Stores = [e.StoreId],
            StoreEngageRegions = [e.EngageRegionId],
            StoreFormats = [e.StoreFormatId],
            StoreClusters = [e.StoreClusterId],
            StoreLSMs = [e.StoreLSMId],
            StoreTypes = [e.StoreTypeId],
            SurveyFormIds = []
        }).ToList();

        //--------------------Employee Only Targeted Surveys--------------------

        var employeeOnlyTargetedSurveyForms = targetedSurveyForms.Where(e => employeeOnlyTargetedSurveyFormIds.Contains(e.SurveyFormId)).GroupBy(e => e.SurveyFormId).ToList();

        foreach (var surveyTarget in employeeOnlyTargetedSurveyForms)
        {
            var hasEmployeeTarget = surveyTarget.OfType<SurveyFormEmployee>().Any();
            var hasRegionTarget = surveyTarget.OfType<SurveyFormEmployeeEngageRegion>().Any();
            var hasDepartmentTarget = surveyTarget.OfType<SurveyFormEngageDepartment>().Any();
            var hasJobTitleTarget = surveyTarget.OfType<SurveyFormEmployeeJobTitle>().Any();
            var hasDivisionTarget = surveyTarget.OfType<SurveyFormEmployeeDivision>().Any();

            var hasCriteriaTarget = hasRegionTarget || hasDepartmentTarget || hasJobTitleTarget || hasDivisionTarget;

            targetingVm.Where(e => (hasEmployeeTarget ? surveyTarget.OfType<SurveyFormEmployee>().Select(e => e.EmployeeId).Contains(e.Employees[0]) : false)
                                                || (hasCriteriaTarget
                                                    ? ((hasRegionTarget ? surveyTarget.OfType<SurveyFormEmployeeEngageRegion>().Select(e => e.EmployeeEngageRegionId).Any(x => e.EmployeeEngageRegions.Contains(x)) : true)
                                                        && (hasDepartmentTarget ? surveyTarget.OfType<SurveyFormEngageDepartment>().Select(e => e.EngageDepartmentId).Any(x => e.EmployeeEngageDepartments.Contains(x)) : true)
                                                        && (hasJobTitleTarget ? surveyTarget.OfType<SurveyFormEmployeeJobTitle>().Select(e => e.EmployeeJobTitleId).Any(x => e.EmployeeJobTitles.Contains(x)) : true)
                                                        && (hasDivisionTarget ? surveyTarget.OfType<SurveyFormEmployeeDivision>().Select(e => e.EmployeeDivisionId).Any(x => e.EmployeeDivisions.Contains(x)) : true))
                                                    : false
                                                    ))
                                    //only add the survey form id if it is not already completed
                                    .ForEach(e =>
                                    {
                                        if (!completedSurveys.Any(c => c.StoreId == e.Stores[0] && c.SurveyFormId == surveyTarget.Key))
                                        {
                                            e.SurveyFormIds.AddIfNotContains(surveyTarget.Key);
                                            surveyFormIds.AddIfNotContains(surveyTarget.Key);
                                            var rules = surveyTarget.Select(e => e.SurveyForm.Rules).FirstOrDefault();
                                            if (rules != null)
                                            {
                                                e.HasAdvancedTargeting = e.HasAdvancedTargeting || rules.Any(r => r.Type == "TargetRule");
                                            }
                                        }
                                    });
        }

        //--------------------------------------Store Only Targeted Surveys--------------------

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

            targetingVm.Where(e => (hasStoreTarget ? surveyTarget.OfType<SurveyFormStore>().Select(e => e.StoreId).Contains(e.Stores[0]) : false)
                                                || (hasCriteriaTarget
                                                    ? ((hasRegionTarget ? surveyTarget.OfType<SurveyFormStoreEngageRegion>().Select(e => e.StoreEngageRegionId).Contains(e.StoreEngageRegions[0]) : true)
                                                        && (hasClusterTarget ? surveyTarget.OfType<SurveyFormStoreCluster>().Select(e => e.StoreClusterId).Contains(e.StoreClusters[0]) : true)
                                                        && (hasFormatTarget ? surveyTarget.OfType<SurveyFormStoreFormat>().Select(e => e.StoreFormatId).Contains(e.StoreFormats[0]) : true)
                                                        && (hasLSMTarget ? surveyTarget.OfType<SurveyFormStoreLSM>().Select(e => e.StoreLSMId).Contains(e.StoreLSMs[0]) : true)
                                                        && (hasTypeTarget ? surveyTarget.OfType<SurveyFormStoreType>().Select(e => e.StoreTypeId).Contains(e.StoreTypes[0]) : true))
                                                    : false
                                                    ))
                                    //.ForEach(e => e.SurveyFormIds.AddIfNotContains(surveyTarget.Key));
                                    //only add the survey form id if it is not already completed
                                    .ForEach(e =>
                                    {
                                        if (!completedSurveys.Any(c => c.StoreId == e.Stores[0] && c.SurveyFormId == surveyTarget.Key))
                                        {
                                            e.SurveyFormIds.AddIfNotContains(surveyTarget.Key);
                                            surveyFormIds.AddIfNotContains(surveyTarget.Key);
                                            var rules = surveyTarget.Select(e => e.SurveyForm.Rules).FirstOrDefault();
                                            if (rules != null)
                                            {
                                                e.HasAdvancedTargeting = e.HasAdvancedTargeting || rules.Any(r => r.Type == "TargetRule");
                                            }
                                        }
                                    });
        }

        //--------------------------------------Combined Targeted Surveys--------------------

        var combinedTargetedSurveyForms = targetedSurveyForms.Where(e => combinedTargetingSurveyFormIds.Contains(e.SurveyFormId)).GroupBy(e => e.SurveyFormId).ToList();

        foreach (var surveyTarget in combinedTargetedSurveyForms)
        {
            var hasEmployeeTarget = surveyTarget.OfType<SurveyFormEmployee>().Any();
            var hasEmployeeRegionTarget = surveyTarget.OfType<SurveyFormEmployeeEngageRegion>().Any();
            var hasDepartmentTarget = surveyTarget.OfType<SurveyFormEngageDepartment>().Any();
            var hasJobTitleTarget = surveyTarget.OfType<SurveyFormEmployeeJobTitle>().Any();
            var hasEmployeeDivisionTarget = surveyTarget.OfType<SurveyFormEmployeeDivision>().Any();

            var hasEmployeeCriteriaTarget = hasEmployeeRegionTarget || hasDepartmentTarget || hasJobTitleTarget || hasEmployeeDivisionTarget;

            var hasStoreTarget = surveyTarget.OfType<SurveyFormStore>().Any();
            var hasStoreRegionTarget = surveyTarget.OfType<SurveyFormStoreEngageRegion>().Any();
            var hasClusterTarget = surveyTarget.OfType<SurveyFormStoreCluster>().Any();
            var hasFormatTarget = surveyTarget.OfType<SurveyFormStoreFormat>().Any();
            var hasLSMTarget = surveyTarget.OfType<SurveyFormStoreLSM>().Any();
            var hasTypeTarget = surveyTarget.OfType<SurveyFormStoreType>().Any();

            var hasStoreCriteriaTarget = hasStoreRegionTarget || hasClusterTarget || hasFormatTarget || hasLSMTarget || hasTypeTarget;

            targetingVm.Where(e => ((hasEmployeeTarget ? surveyTarget.OfType<SurveyFormEmployee>().Select(e => e.EmployeeId).Contains(e.Employees[0]) : false)
                                                || (hasEmployeeCriteriaTarget
                                                    ? ((hasEmployeeRegionTarget ? surveyTarget.OfType<SurveyFormEmployeeEngageRegion>().Select(e => e.EmployeeEngageRegionId).Any(x => e.EmployeeEngageRegions.Contains(x)) : true)
                                                        && (hasDepartmentTarget ? surveyTarget.OfType<SurveyFormEngageDepartment>().Select(e => e.EngageDepartmentId).Any(x => e.EmployeeEngageDepartments.Contains(x)) : true)
                                                        && (hasJobTitleTarget ? surveyTarget.OfType<SurveyFormEmployeeJobTitle>().Select(e => e.EmployeeJobTitleId).Any(x => e.EmployeeJobTitles.Contains(x)) : true)
                                                        && (hasEmployeeDivisionTarget ? surveyTarget.OfType<SurveyFormEmployeeDivision>().Select(e => e.EmployeeDivisionId).Any(x => e.EmployeeDivisions.Contains(x)) : true))
                                                    : false
                                                    ))
                                   && ((hasStoreTarget ? surveyTarget.OfType<SurveyFormStore>().Select(e => e.StoreId).Contains(e.Stores[0]) : false)
                                                || (hasStoreCriteriaTarget
                                                    ? ((hasStoreRegionTarget ? surveyTarget.OfType<SurveyFormStoreEngageRegion>().Select(e => e.StoreEngageRegionId).Contains(e.StoreEngageRegions[0]) : true)
                                                        && (hasClusterTarget ? surveyTarget.OfType<SurveyFormStoreCluster>().Select(e => e.StoreClusterId).Contains(e.StoreClusters[0]) : true)
                                                        && (hasFormatTarget ? surveyTarget.OfType<SurveyFormStoreFormat>().Select(e => e.StoreFormatId).Contains(e.StoreFormats[0]) : true)
                                                        && (hasLSMTarget ? surveyTarget.OfType<SurveyFormStoreLSM>().Select(e => e.StoreLSMId).Contains(e.StoreLSMs[0]) : true)
                                                        && (hasTypeTarget ? surveyTarget.OfType<SurveyFormStoreType>().Select(e => e.StoreTypeId).Contains(e.StoreTypes[0]) : true))
                                                    : false
                                                    )))
                                    //.ForEach(e => e.SurveyFormIds.AddIfNotContains(surveyTarget.Key));
                                    //only add the survey form id if it is not already completed
                                    .ForEach(e =>
                                    {
                                        if (!completedSurveys.Any(c => c.StoreId == e.Stores[0] && c.SurveyFormId == surveyTarget.Key))
                                        {
                                            e.SurveyFormIds.AddIfNotContains(surveyTarget.Key);
                                            surveyFormIds.AddIfNotContains(surveyTarget.Key);
                                            var rules = surveyTarget.Select(e => e.SurveyForm.Rules).FirstOrDefault();
                                            if (rules != null)
                                            {
                                                e.HasAdvancedTargeting = e.HasAdvancedTargeting || rules.Any(r => r.Type == "TargetRule");
                                            }
                                        }
                                    });
        }

        targetingVm.ForEach(e =>
        {
            if (!completedSurveys.Any(c => nonTargetedSurveyFormIds.Contains(c.SurveyFormId)))
            {
                e.SurveyFormIds.AddRangeIfNotContains(nonTargetedSurveyFormIds.Where(e => !completedSurveys.Select(e => e.SurveyFormId).Contains(e)).ToArray());
                surveyFormIds.AddRangeIfNotContains(nonTargetedSurveyFormIds.Where(e => !completedSurveys.Select(e => e.SurveyFormId).Contains(e)).ToArray());
            }
        });

        var queryable = Context.SurveyForms.AsQueryable().AsNoTracking().Where(e => !excludedSurveyFormIds.Contains(e.SurveyFormId));

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

        var surveys = await queryable.Where(e => !e.IsDisabled && surveyFormIds.Contains(e.SurveyFormId)
                                                 && query.SurveyTypes.Contains(e.SurveyFormType.Name)
                                                 && (
                                                     (employeeTargetingSurveyFormIds.Contains(e.SurveyFormId))
                                                     || (e.IgnoreSubgroup || !e.EngageSubgroupId.HasValue)
                                                     || (!e.IgnoreSubgroup && e.EngageSubgroupId.HasValue && employeeSubgroups.Contains(e.EngageSubgroupId.Value)))
                                                 && e.IsTemplate == false)
                                     .ToListAsync(cancellationToken);

        var surveyForms = Mapper.Map<List<SurveyFormWithQuestions>>(surveys);

        var surveyFormStores = new List<SurveyFormStoreOfflineDto>();
        foreach (var item in targetingVm)
        {
            var storeSurveys = surveyForms.Where(e => item.SurveyFormIds.Contains(e.Id)).ToList();
            var surveyFormStore = new SurveyFormStoreOfflineDto()
            {
                SurveyFormAdvancedTargeting = item,
                StoreId = item.Stores[0],
                //Suppliers = storeSurveys.Where(e => e.SupplierId != null).DistinctBy(e => e.SupplierId.Id).Select(e => new OptionDto() { Name = e.SupplierId.Name, Id = e.SupplierId.Id }).ToList(),
                //RequiredSurveyIds = storeSurveys.Where(e => e.IsRequired == true).Select(e => e.Id).ToList(),
                //RequiredRuleSurveyIds = []
            };
            surveyFormStores.Add(surveyFormStore);
        }
        return new SurveyFormMobileOfflineDto() { SurveyForms = surveyForms, SurveyFormStores = surveyFormStores };
    }
}