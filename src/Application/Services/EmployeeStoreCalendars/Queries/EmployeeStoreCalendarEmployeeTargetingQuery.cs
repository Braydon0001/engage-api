namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarEmployeeTargetingQuery : IRequest<List<SurveyForm>>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public DateTime DateTime { get; set; }
}
public record EmployeeStoreCalendarEmployeeTargetingHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeStoreCalendarEmployeeTargetingQuery, List<SurveyForm>>
{
    public async Task<List<SurveyForm>> Handle(EmployeeStoreCalendarEmployeeTargetingQuery query, CancellationToken cancellationToken)
    {
        var employee = await Context.Employees
                                              .Include(e => e.EmployeeDepartments).ThenInclude(e => e.EngageDepartment)
                                              .Include(e => e.EmployeeRegions).ThenInclude(e => e.EngageRegion)
                                              .Include(e => e.EmployeeJobTitles).ThenInclude(e => e.EmployeeJobTitle)
                                              .Include(e => e.EmployeeDivisions).ThenInclude(e => e.EmployeeDivision)
                                              .Include(e => e.EmployeeStores)
                                              .FirstOrDefaultAsync(e => e.EmployeeId == query.EmployeeId, cancellationToken);

        List<int> targetedSurveys = [];

        var subGroups = employee.EmployeeStores.Where(e => e.StoreId == query.StoreId).Select(e => e.EngageSubGroupId).ToList();

        var employeeDepartmentIds = employee.EmployeeDepartments.Where(e => e.EngageDepartment.Disabled == false).Select(e => e.EngageDepartmentId).ToList();

        var employeeRegionIds = employee.EmployeeRegions.Where(e => e.EngageRegion.Disabled == false).Select(e => e.EngageRegionId).ToList();

        var employeeJobTitleIds = employee.EmployeeJobTitles.Where(e => e.EmployeeJobTitle.Disabled == false).Select(e => e.EmployeeJobTitleId).ToList();

        var employeeDivisionIds = employee.EmployeeDivisions.Where(e => e.EmployeeDivision.Disabled == false).Select(e => e.EmployeeDivisionId).ToList();

        var targetedSurveyForms = await Context.SurveyFormTargets
                                   .AsNoTracking()
                                   .Where(e => e.SurveyForm.SurveyFormTypeId == (int)SurveyFormTypeId.ContactReport
                                        && e.SurveyForm.StartDate.Value.Date <= query.DateTime.Date
                                        && (e.SurveyForm.EndDate == null || e.SurveyForm.EndDate.Value.Date >= query.DateTime.Date)
                                        && e.SurveyForm.IsDisabled == false && e.SurveyForm.Disabled == false)
                                   .Distinct()
                                   .ToListAsync(cancellationToken);

        //surrveyIds of surveys that exclude this employee or store
        var excludedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormExcludedEmployee>()
                                                       .Where(e => e.ExcludedEmployeeId == query.EmployeeId)
                                                       .Select(e => e.SurveyFormId)
                                                       .Distinct()
                                                       .ToList();

        var surveyIds = targetedSurveyForms.Select(e => e.SurveyFormId).Distinct().ToList();

        var groupedTargeting = targetedSurveyForms.GroupBy(e => e.SurveyFormId).ToList();

        List<int> targetedIds = [];

        foreach (var survey in surveyIds)
        {
            var surveyTarget = targetedSurveyForms.Where(e => e.SurveyFormId == survey);

            var employeeTargets = surveyTarget.OfType<SurveyFormEmployee>().Select(e => e.EmployeeId).ToList();
            var regionTargets = surveyTarget.OfType<SurveyFormEmployeeEngageRegion>().Select(e => e.EmployeeEngageRegionId).ToList();
            var departmentTargets = surveyTarget.OfType<SurveyFormEngageDepartment>().Select(e => e.EngageDepartmentId).ToList();
            var jobTitleTargets = surveyTarget.OfType<SurveyFormEmployeeJobTitle>().Select(e => e.EmployeeJobTitleId).ToList();
            var divisionTargets = surveyTarget.OfType<SurveyFormEmployeeDivision>().Select(e => e.EmployeeDivisionId).ToList();

            var hasEmployeeTarget = employeeTargets.Count != 0;
            var hasRegionTarget = regionTargets.Count != 0;
            var hasDepartmentTarget = departmentTargets.Count != 0;
            var hasJobTitleTarget = jobTitleTargets.Count != 0;
            var hasDivisionTarget = divisionTargets.Count != 0;

            var hasCriteriaTarget = hasRegionTarget || hasDepartmentTarget || hasJobTitleTarget || hasDivisionTarget;

            var testempTarget = (hasEmployeeTarget ? employeeTargets.Contains(employee.EmployeeId) : false);

            var targeting = hasCriteriaTarget ? (hasRegionTarget ? regionTargets.Any(x => employeeRegionIds.Contains(x)) : true)
                                         && (hasDepartmentTarget ? departmentTargets.Any(x => employeeDepartmentIds.Contains(x)) : true)
                                         && (hasJobTitleTarget ? jobTitleTargets.Any(x => employeeJobTitleIds.Contains(x)) : true)
                                         && (hasDivisionTarget ? divisionTargets.Any(x => employeeDivisionIds.Contains(x)) : true)
                                    : false;

            var isTargeted = (hasEmployeeTarget ? employeeTargets.Contains(employee.EmployeeId) : false)
                                || (hasCriteriaTarget ? (hasRegionTarget ? regionTargets.Any(x => employeeRegionIds.Contains(x)) : true)
                                         && (hasDepartmentTarget ? departmentTargets.Any(x => employeeDepartmentIds.Contains(x)) : true)
                                         && (hasJobTitleTarget ? jobTitleTargets.Any(x => employeeJobTitleIds.Contains(x)) : true)
                                         && (hasDivisionTarget ? divisionTargets.Any(x => employeeDivisionIds.Contains(x)) : true)
                                    : false);

            if (isTargeted)
            {
                targetedIds.Add(survey);
            }
        }

        var employeeTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployee>().Where(e => e.EmployeeId == query.EmployeeId).Select(e => e.SurveyFormId).ToList();
        var employeeEngageRegionTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployeeEngageRegion>().Where(e => employeeRegionIds.Contains(e.EmployeeEngageRegionId)).Select(e => e.SurveyFormId).ToList();
        var engageDepartmentTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEngageDepartment>().Where(e => employeeDepartmentIds.Contains(e.EngageDepartmentId)).Select(e => e.SurveyFormId).ToList();
        var employeeJobTitleTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployeeJobTitle>().Where(e => employeeJobTitleIds.Contains(e.EmployeeJobTitleId)).Select(e => e.SurveyFormId).ToList();
        var employeeDivisionTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployeeDivision>().Where(e => employeeDivisionIds.Contains(e.EmployeeDivisionId)).Select(e => e.SurveyFormId).ToList();

        //employee targeted surveys
        var employeeTargetedSurveys = employeeTargetedSurveyFormIds.Union(employeeEngageRegionTargetedSurveyFormIds)
                                                                          .Union(employeeEngageRegionTargetedSurveyFormIds)
                                                                          .Union(engageDepartmentTargetedSurveyFormIds)
                                                                          .Union(employeeJobTitleTargetedSurveyFormIds)
                                                                          .Union(employeeDivisionTargetedSurveyFormIds)
                                                                          .Distinct()
                                                                          .ToList();

        var surveys = await Context.SurveyForms.AsNoTracking()
                                               .Where(e => targetedIds.Contains(e.SurveyFormId) && !excludedSurveyFormIds.Contains(e.SurveyFormId))
                                               .ToListAsync(cancellationToken);

        return surveys;
    }
}
