using Engage.Application.Services.Mobile.SurveyForms.Queries;

namespace Engage.Application.Services.SurveyFormTargets.Queries;
public record SurveyFormTargetedParamsVmQuery(int EmployeeId, int? StoreId, DateTime Date) : IRequest<SurveyFormAdvancedTargetingVm>
{
}

public class SurveyFormTargetedParamsVmHandler : IRequestHandler<SurveyFormTargetedParamsVmQuery, SurveyFormAdvancedTargetingVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public SurveyFormTargetedParamsVmHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SurveyFormAdvancedTargetingVm> Handle(SurveyFormTargetedParamsVmQuery query, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.Include(e => e.EmployeeJobTitles).ThenInclude(e => e.EmployeeJobTitle)
                                               .Include(e => e.EmployeeRegions).ThenInclude(e => e.EngageRegion)
                                               .Include(e => e.EmployeeDepartments).ThenInclude(e => e.EngageDepartment)
                                               .Include(e => e.EmployeeStores)
                                               .SingleOrDefaultAsync(e => e.EmployeeId == query.EmployeeId, cancellationToken);

        if (employee == null)
        {
            return null;
        }

        var employeeSubgroups = employee.EmployeeStores.Select(e => e.EngageSubGroupId).ToList();

        var targetedSurveyForms = await _context.SurveyFormTargets.Select(e => e.SurveyFormId).Distinct().ToListAsync(cancellationToken);

        var employeeSurveyFormIds = await _context.SurveyFormEmployees.Where(e => e.EmployeeId == query.EmployeeId
                                                                            && query.Date.Date >= e.SurveyForm.StartDate.Value.Date
                                                                            && (!e.SurveyForm.EndDate.HasValue
                                                                            || query.Date.Date <= e.SurveyForm.EndDate.Value.Date))
                                                                          .Select(e => e.SurveyFormId)
                                                                          .ToListAsync(cancellationToken);

        var employeeEngageRegionIds = employee.EmployeeRegions.Where(e => e.EngageRegion.Disabled == false)
                                                              .Select(e => e.EngageRegionId)
                                                              .ToList();

        var employeeEngageRegionSurveyFormIds = await _context.SurveyFormEmployeeEngageRegions.Where(e => employeeEngageRegionIds.Contains(e.EmployeeEngageRegionId)
                                                                                                && e.EmployeeEngageRegion.Disabled == false)
                                                                                      .Select(e => e.SurveyFormId)
                                                                                      .ToListAsync(cancellationToken);

        var engageDepartmentIds = employee.EmployeeDepartments.Where(e => e.EngageDepartment.Disabled == false)
                                                              .Select(e => e.EngageDepartmentId)
                                                              .ToList();

        var engageDepartmentSurveyFormIds = await _context.SurveyFormEngageDepartments.Where(e => engageDepartmentIds.Contains(e.EngageDepartmentId)
                                                                                                && e.EngageDepartment.Disabled == false)
                                                                                      .Select(e => e.SurveyFormId)
                                                                                      .ToListAsync(cancellationToken);

        var jobTitleIds = employee.EmployeeJobTitles.Where(e => e.EmployeeJobTitle.Disabled == false
                                                        && e.EmployeeJobTitle.Level == 3)
                                                    .Select(e => e.EmployeeJobTitleId)
                                                    .ToList();

        var jobTitleSurveyFormIds = await _context.SurveyFormEmployeeJobTitles.Where(e => jobTitleIds
                                                                                  .Contains(e.EmployeeJobTitleId)
                                                                                      && e.EmployeeJobTitle.Disabled == false
                                                                                      && e.EmployeeJobTitle.Level == 3)
                                                                                  .Select(e => e.SurveyFormId)
                                                                                  .ToListAsync(cancellationToken);

        var surveyFormIds = employeeSurveyFormIds.Union(employeeEngageRegionSurveyFormIds)
                                                 .Union(engageDepartmentSurveyFormIds)
                                                 .Union(jobTitleSurveyFormIds)
                                                 .Distinct()
                                                 .ToList();

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
                var storeSurveyFormIds = await _context.SurveyFormStores.Where(e => e.StoreId == query.StoreId
                                                                                    && query.Date.Date >= e.SurveyForm.StartDate.Value.Date
                                                                                    && (!e.SurveyForm.EndDate.HasValue
                                                                                        || query.Date.Date <= e.SurveyForm.EndDate.Value.Date
                                                                                       )
                                                                                    )
                                                                                  .Select(e => e.SurveyFormId)
                                                                                  .ToListAsync(cancellationToken);

                var storeEngageRegionSurveyFormIds = await _context.SurveyFormStoreEngageRegions
                                                                        .Where(e => e.StoreEngageRegionId == store.EngageRegionId)
                                                                        .Select(e => e.SurveyFormId)
                                                                        .ToListAsync(cancellationToken);

                var clusterSurveyFormIds = await _context.SurveyFormStoreClusters.Where(e => store.StoreClusterId == e.StoreClusterId)
                                                                                   .Select(e => e.SurveyFormId)
                                                                                   .ToListAsync(cancellationToken);

                var formatSurveyFormIds = await _context.SurveyFormStoreFormats.Where(e => store.StoreFormatId == e.StoreFormatId)
                                                                                   .Select(e => e.SurveyFormId)
                                                                                   .ToListAsync(cancellationToken);

                var lsmSurveyFormIds = await _context.SurveyFormStoreLSMs.Where(e => store.StoreLSMId == e.StoreLSMId)
                                                                                   .Select(e => e.SurveyFormId)
                                                                                   .ToListAsync(cancellationToken);

                var typeSurveyFormIds = await _context.SurveyFormStoreTypes.Where(e => store.StoreTypeId == e.StoreTypeId)
                                                                                   .Select(e => e.SurveyFormId)
                                                                                   .ToListAsync(cancellationToken);

                targetingVm.Stores = [store.StoreId];
                targetingVm.StoreEngageRegions = [store.EngageRegionId];
                targetingVm.StoreFormats = [store.StoreFormatId];
                targetingVm.StoreClusters = [store.StoreClusterId];
                targetingVm.StoreLSMs = [store.StoreLSMId];
                targetingVm.StoreTypes = [store.StoreFormatId];

                surveyFormIds = surveyFormIds.Union(storeSurveyFormIds)
                                                             .Union(storeEngageRegionSurveyFormIds)
                                                             .Union(clusterSurveyFormIds)
                                                             .Union(formatSurveyFormIds)
                                                             .Union(lsmSurveyFormIds)
                                                             .Union(typeSurveyFormIds)
                                                             .Distinct()
                                                             .ToList();
            }
        }

        var queryable = _context.SurveyForms.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SurveyFormProducts)
                             .Include(e => e.SurveyFormType)
                             .Include(e => e.Supplier)
                             .Include(e => e.EngageSubGroup)
                             .Include(e => e.EngageBrand)
                             .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Any()))
                                 .ThenInclude(e => e.SurveyFormQuestionGroupProducts)
                             .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Any()))
                                 .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                     .ThenInclude(e => e.SurveyFormQuestionProducts)
                                         .ThenInclude(e => e.EngageVariantProduct)
                             .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Any()))
                                 .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                     .ThenInclude(e => e.SurveyFormQuestionOptions)
                                         .ThenInclude(e => e.SurveyFormOption)
                             .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Any()))
                                 .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                     .ThenInclude(e => e.SurveyFormQuestionReasons)
                                         .ThenInclude(e => e.SurveyFormReason)
                             .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Any()))
                                 .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                     .ThenInclude(e => e.SurveyFormQuestionType);

        var entites = await queryable.Where(e => e.Disabled == false && e.IsDisabled == false
                                     && query.Date.Date >= e.StartDate.Value.Date
                                     && (!e.EndDate.HasValue || query.Date.Date <= e.EndDate.Value.Date)
                                     && e.SurveyFormQuestionGroups.Any(e => e.SurveyFormQuestions.Count > 0)
                                     && (e.IgnoreSubgroup || (!e.IgnoreSubgroup && e.EngageSubgroupId.HasValue && employeeSubgroups.Contains(e.EngageSubgroupId.Value)))
                                     && ((targetedSurveyForms.Contains(e.SurveyFormId)
                                     && surveyFormIds.Contains(e.SurveyFormId))
                                     //Include all files which are not targted
                                     || !targetedSurveyForms.Contains(e.SurveyFormId)
                                     )).ToListAsync(cancellationToken);

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
        targetingVm.SurveyForms = new ListResult<SurveyFormWithQuestions>(surveyForms);

        return targetingVm;
    }
}