﻿namespace Engage.Application.Services.Mobile.SurveyForms.Queries;
public record SurveyFormPosUpdateSurveyTargetedQuery(int EmployeeId, int? StoreId, DateTime Date) : IRequest<HistoryStoreSurveys>
{
}

public class SurveyFormPosUpdateSurveyTargetedHandler : IRequestHandler<SurveyFormPosUpdateSurveyTargetedQuery, HistoryStoreSurveys>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public SurveyFormPosUpdateSurveyTargetedHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HistoryStoreSurveys> Handle(SurveyFormPosUpdateSurveyTargetedQuery query, CancellationToken cancellationToken)
    {
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
                                                                              && (!e.SurveyForm.EndDate.HasValue || query.Date.Date <= e.SurveyForm.EndDate.Value.Date)
                                                                              && e.SurveyForm.IsTemplate == false)
                                                                  .Distinct()
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

        var targetedSurveyFormIds = targetedSurveyForms.Select(e => e.SurveyFormId).Distinct().ToList();

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

        foreach (var surveyFormId in employeeOnlyTargetedSurveyFormIds)
        {
            var surveyTargetings = targetedSurveyForms.Where(e => e.SurveyFormId == surveyFormId).ToList();

            var isEmployeeTargeted = false;

            //if we are targeting by employee
            if (surveyTargetings.OfType<SurveyFormEmployee>().ToList().Count > 0)
            {
                isEmployeeTargeted = surveyTargetings.OfType<SurveyFormEmployee>()
                                             .Where(e => e.EmployeeId == employee.EmployeeId)
                                             .Distinct()
                                             .Any();
            }

            var isCriteriaTargeted = true;
            var employeeCriteriaSkipped = true;

            if (surveyTargetings.OfType<SurveyFormEmployeeEngageRegion>().ToList().Count > 0)
            {
                isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeEngageRegion>()
                                                           .Where(e => employeeEngageRegionIds.Contains(e.EmployeeEngageRegionId) && e.EmployeeEngageRegion.Disabled == false)
                                                           .Distinct()
                                                           .Any();
                employeeCriteriaSkipped = false;
            }

            if (surveyTargetings.OfType<SurveyFormEngageDepartment>().ToList().Count > 0)
            {
                isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormEngageDepartment>()
                                                           .Where(e => engageDepartmentIds.Contains(e.EngageDepartmentId)
                                                                                                    && e.EngageDepartment.Disabled == false)
                                                           .Distinct()
                                                           .Any();
                employeeCriteriaSkipped = false;
            }

            if (surveyTargetings.OfType<SurveyFormEmployeeJobTitle>().ToList().Count > 0)
            {
                isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeJobTitle>()
                                                           .Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId)
                                                                                          && e.EmployeeJobTitle.Disabled == false
                                                                                          && e.EmployeeJobTitle.Level == 3)
                                                           .Distinct()
                                                           .Any();
                employeeCriteriaSkipped = false;
            }

            if (surveyTargetings.OfType<SurveyFormEmployeeDivision>().ToList().Count > 0)
            {
                isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeDivision>()
                                                           .Where(e => employeeDivisionIds.Contains(e.EmployeeDivisionId)
                                                                                              && e.EmployeeDivision.Disabled == false)
                                                           .Distinct()
                                                           .Any();
                employeeCriteriaSkipped = false;
            }

            if (employeeCriteriaSkipped)
            {
                isCriteriaTargeted = false;
            }

            if (isEmployeeTargeted || isCriteriaTargeted)
            {
                surveyFormIds.Add(surveyFormId);
            }
        }

        var targetingVm = new SurveyFormAdvancedTargetingVm();

        if (query.StoreId != null && query.StoreId > 0)
        {
            var store = await _context.Stores.Include(e => e.EngageRegion)
                                             .Include(e => e.StoreCluster)
                                             .Include(e => e.StoreFormat)
                                             .Include(e => e.StoreLSM)
                                             .Include(e => e.StoreType)
                                             .SingleOrDefaultAsync(e => e.StoreId == query.StoreId, cancellationToken);

            if (store != null)
            {

                foreach (var surveyFormId in storeOnlyTargetedSurveyFormIds)
                {
                    var surveyTargetings = targetedSurveyForms.Where(e => e.SurveyFormId == surveyFormId).ToList();

                    var isStoreTargeted = false;

                    if (surveyTargetings.OfType<SurveyFormStore>().ToList().Count > 0)
                    {
                        isStoreTargeted = surveyTargetings.OfType<SurveyFormStore>()
                                                     .Where(e => e.StoreId == store.StoreId)
                                                     .Distinct()
                                                     .Any();
                    }

                    var isCriteriaTargeted = true;
                    var storeCriteriaSkipped = true;

                    if (surveyTargetings.OfType<SurveyFormStoreEngageRegion>().ToList().Count > 0)
                    {
                        isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreEngageRegion>()
                                                                   .Where(e => e.StoreEngageRegionId == store.EngageRegionId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreCluster>().ToList().Count > 0)
                    {
                        isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreCluster>()
                                                                   .Where(e => e.StoreClusterId == store.StoreClusterId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreFormat>().ToList().Count > 0)
                    {
                        isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreFormat>()
                                                                   .Where(e => e.StoreFormatId == store.StoreFormatId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreLSM>().ToList().Count > 0)
                    {
                        isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreLSM>()
                                                                   .Where(e => e.StoreLSMId == store.StoreLSMId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreType>().ToList().Count > 0)
                    {
                        isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreType>()
                                                                   .Where(e => e.StoreTypeId == store.StoreTypeId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (storeCriteriaSkipped)
                    {
                        isCriteriaTargeted = false;
                    }

                    if (isStoreTargeted || isCriteriaTargeted)
                    {
                        surveyFormIds.Add(surveyFormId);
                    }
                }

                foreach (var surveyFormId in combinedTargetingSurveyFormIds)
                {
                    var surveyTargetings = targetedSurveyForms.Where(e => e.SurveyFormId == surveyFormId).ToList();

                    var isEmployeeTargeted = false;

                    if (surveyTargetings.OfType<SurveyFormEmployee>().ToList().Count > 0)
                    {
                        isEmployeeTargeted = surveyTargetings.OfType<SurveyFormEmployee>()
                                                     .Where(e => e.EmployeeId == employee.EmployeeId)
                                                     .Distinct()
                                                     .Any();
                    }

                    var isEmployeeCriteriaTargeted = true;
                    var employeeCriteriaSkipped = true;

                    if (surveyTargetings.OfType<SurveyFormEmployeeEngageRegion>().ToList().Count > 0)
                    {
                        isEmployeeCriteriaTargeted = isEmployeeCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeEngageRegion>()
                                                                   .Where(e => employeeEngageRegionIds.Contains(e.EmployeeEngageRegionId) && e.EmployeeEngageRegion.Disabled == false)
                                                                   .Distinct()
                                                                   .Any();
                        employeeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormEngageDepartment>().ToList().Count > 0)
                    {
                        isEmployeeCriteriaTargeted = isEmployeeCriteriaTargeted && surveyTargetings.OfType<SurveyFormEngageDepartment>()
                                                                   .Where(e => engageDepartmentIds.Contains(e.EngageDepartmentId)
                                                                                                            && e.EngageDepartment.Disabled == false)
                                                                   .Distinct()
                                                                   .Any();
                        employeeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormEmployeeJobTitle>().ToList().Count > 0)
                    {
                        isEmployeeCriteriaTargeted = isEmployeeCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeJobTitle>()
                                                                   .Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId)
                                                                                                  && e.EmployeeJobTitle.Disabled == false
                                                                                                  && e.EmployeeJobTitle.Level == 3)
                                                                   .Distinct()
                                                                   .Any();
                        employeeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormEmployeeDivision>().ToList().Count > 0)
                    {
                        isEmployeeCriteriaTargeted = isEmployeeCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeDivision>()
                                                                   .Where(e => employeeDivisionIds.Contains(e.EmployeeDivisionId)
                                                                                                      && e.EmployeeDivision.Disabled == false)
                                                                   .Distinct()
                                                                   .Any();
                        employeeCriteriaSkipped = false;
                    }

                    if (employeeCriteriaSkipped)
                    {
                        isEmployeeCriteriaTargeted = false;
                    }

                    var isStoreTargeted = false;

                    if (surveyTargetings.OfType<SurveyFormStore>().ToList().Count > 0)
                    {
                        isStoreTargeted = surveyTargetings.OfType<SurveyFormStore>()
                                                     .Where(e => e.StoreId == store.StoreId)
                                                     .Distinct()
                                                     .Any();
                    }

                    var isStoreCriteriaTargeted = true;
                    var storeCriteriaSkipped = true;

                    if (surveyTargetings.OfType<SurveyFormStoreEngageRegion>().ToList().Count > 0)
                    {
                        isStoreCriteriaTargeted = isStoreCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreEngageRegion>()
                                                                   .Where(e => e.StoreEngageRegionId == store.EngageRegionId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreCluster>().ToList().Count > 0)
                    {
                        isStoreCriteriaTargeted = isStoreCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreCluster>()
                                                                   .Where(e => e.StoreClusterId == store.StoreClusterId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreFormat>().ToList().Count > 0)
                    {
                        isStoreCriteriaTargeted = isStoreCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreFormat>()
                                                                   .Where(e => e.StoreFormatId == store.StoreFormatId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreLSM>().ToList().Count > 0)
                    {
                        isStoreCriteriaTargeted = isStoreCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreLSM>()
                                                                   .Where(e => e.StoreLSMId == store.StoreLSMId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreType>().ToList().Count > 0)
                    {
                        isStoreCriteriaTargeted = isStoreCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreType>()
                                                                   .Where(e => e.StoreTypeId == store.StoreTypeId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (storeCriteriaSkipped)
                    {
                        isStoreCriteriaTargeted = false;
                    }

                    if ((isEmployeeTargeted || isEmployeeCriteriaTargeted) && (isStoreTargeted || isStoreCriteriaTargeted))
                    {
                        surveyFormIds.Add(surveyFormId);
                    }
                }

                targetingVm.Stores = [store.StoreId];
                targetingVm.StoreEngageRegions = [store.EngageRegionId];
                targetingVm.StoreFormats = [store.StoreFormatId];
                targetingVm.StoreClusters = [store.StoreClusterId];
                targetingVm.StoreLSMs = [store.StoreLSMId];
                targetingVm.StoreTypes = [store.StoreFormatId];
            }
        }

        var storeRecurringSurveys = await _context.SurveyForms.Where(e => !e.Disabled && !e.Deleted && e.IsStoreRecurring && !e.IsDisabled).Select(e => e.SurveyFormId).ToListAsync(cancellationToken);

        var completedSurveys = await _context.SurveyFormSubmissions.Where(e => e.IsComplete
                                                                         && e.StoreId.HasValue && e.StoreId.Value == query.StoreId
                                                                         //get the surveys completed by the user
                                                                         && (e.EmployeeId == query.EmployeeId
                                                                              //or the store recurring surveys completed by others
                                                                              || storeRecurringSurveys.Contains(e.SurveyFormId))
                                                                         && e.CompletedDate.HasValue && (e.SurveyForm.IsRecurring ? e.CompletedDate.Value.Date == DateTime.Now.Date : e.CompletedDate.Value.Date <= DateTime.Now.Date))
                                                                   .Select(e => e.SurveyFormId)
                                                                   .ToListAsync(cancellationToken);

        var queryable = _context.SurveyForms.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SurveyFormProducts)
                             .Include(e => e.SurveyFormType)
                             .Include(e => e.Supplier)
                             .Include(e => e.EngageSubGroup)
                             .Include(e => e.EngageBrand)
                             .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Count != 0))
                                 .ThenInclude(e => e.SurveyFormQuestionGroupProducts)
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
                                                 && e.IsTemplate == false
                                                 // only include surveys not yet completed today
                                                 && !completedSurveys.Contains(e.SurveyFormId)
                                                 // only include surveys that have questions
                                                 && e.SurveyFormQuestionGroups.Any(e => e.SurveyFormQuestions.Count > 0)
                                                 // only get the POS Update surveys
                                                 && e.SurveyFormType.Name == "POS Update"
                                                 // only get the surveys that are set to ignore user subgroups or the ones that align with the user subgroups
                                                 && ((e.IgnoreSubgroup || !e.EngageSubgroupId.HasValue) || (!e.IgnoreSubgroup && e.EngageSubgroupId.HasValue && employeeSubgroups.Contains(e.EngageSubgroupId.Value)))
                                                 // all the surveys that have targeting applied and target the user and/or store
                                                 && ((targetedSurveyFormIds.Contains(e.SurveyFormId) && surveyFormIds.Contains(e.SurveyFormId))
                                                 // Include all surveys which are not targted
                                                 || !targetedSurveyFormIds.Contains(e.SurveyFormId)
                                                 )
                                            )
                                     .Where(e => !excludedSurveyFormIds.Contains(e.SurveyFormId))
                                     .ToListAsync(cancellationToken);

        var surveyForms = _mapper.Map<List<SurveyFormWithQuestions>>(entites);

        var hasRules = false;

        var surveysWithRules = surveyForms.Where(e => e.Rules != null).ToList();

        if (surveysWithRules.Any())
        {
            foreach (var survey in surveysWithRules)
            {
                if (survey.Rules.Any(e => e.Type == "TargetRule"))
                {
                    hasRules = true;
                    break;
                }
            }
        }

        targetingVm.HasAdvancedTargeting = hasRules;
        targetingVm.Employees = [employee.EmployeeId];
        targetingVm.EmployeeEngageRegions = employeeEngageRegionIds;
        targetingVm.EmployeeJobTitles = jobTitleIds;
        targetingVm.EmployeeEngageDepartments = engageDepartmentIds;
        targetingVm.EmployeeDivisions = employeeDivisionIds;
        targetingVm.SurveyForms = new ListResult<SurveyFormWithQuestions>(surveyForms);

        //History
        var submissions = await _context.SurveyFormSubmissions
                                                  .Include(e => e.SurveyFormAnswers)
                                                  .Include(e => e.SurveyForm)
                                                  .Where(e => e.StoreId == query.StoreId && e.SurveyForm.SurveyFormType.Name == "POS Update"
                                                              && (surveyFormIds.Contains(e.SurveyFormId)
                                                                  // Include all surveys which are not targted. This may not be necessary
                                                                  || !targetedSurveyFormIds.Contains(e.SurveyFormId)
                                                                 ))
                                                  .OrderByDescending(e => e.Created)
                                                  //.Take(10)
                                                  .ProjectTo<SurveyFormHistoryDto>(_mapper.ConfigurationProvider)
                                                  .ToListAsync(cancellationToken);

        var distinctSubmissions = submissions.OrderByDescending(e => e.Created).DistinctBy(e => e.SurveyFormId).ToList();
        //var list = new List<SurveyFormHistoryDto>(entities);
        var historySurveyIds = submissions.Select(e => e.SurveyFormId).Distinct().ToList();

        if (submissions.Count > 0)
        {
            foreach (var submission in submissions)
            {
                var answers = await _context.SurveyFormAnswers.Where(e => e.SurveyFormSubmissionId == submission.SurveyFormSubmissionId)
                                                                   .ProjectTo<SurveyFormAnswserHistoryDto>(_mapper.ConfigurationProvider)
                                                                   .ToListAsync(cancellationToken);
                submission.Answers = answers;
            }
        }

        var historySurveys = await queryable.Where(e => historySurveyIds.Contains(e.SurveyFormId)).OrderByDescending(e => e.Created).ToListAsync(cancellationToken);

        var historySurveyForms = _mapper.Map<List<SurveyFormWithQuestions>>(historySurveys);

        var SurveyFormWithHistory = new SurveyFormWithHistory { History = distinctSubmissions, SurveyForms = historySurveyForms };

        var result = new HistoryStoreSurveys { SurveyFormAdvancedTargetingVm = targetingVm, SurveyFormWithHistory = SurveyFormWithHistory };

        return result;
    }
}

public class HistoryStoreSurveys
{
    public SurveyFormAdvancedTargetingVm SurveyFormAdvancedTargetingVm { get; set; }
    public SurveyFormWithHistory SurveyFormWithHistory { get; set; }
}