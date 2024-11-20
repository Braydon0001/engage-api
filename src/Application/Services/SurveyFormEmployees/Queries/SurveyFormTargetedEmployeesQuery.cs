using Engage.Application.Services.Employees.Models;

namespace Engage.Application.Services.SurveyFormEmployees.Queries;

public class SurveyFormTargetedEmployeesQuery : IRequest<List<EmployeeListDto>>
{
    public int Id { get; set; }
}

public record SurveyFormTargetedEmployeesHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormTargetedEmployeesQuery, List<EmployeeListDto>>
{
    public async Task<List<EmployeeListDto>> Handle(SurveyFormTargetedEmployeesQuery query, CancellationToken cancellationToken)
    {
        var survey = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == query.Id, cancellationToken);
        if (survey == null)
        {
            return null;
        }

        var entities = await Context.SurveyFormTargets.AsQueryable().AsNoTracking().Where(e => e.SurveyFormId == query.Id).ToListAsync(cancellationToken);

        var employeeIds = entities.OfType<SurveyFormEmployee>().Select(e => e.EmployeeId).ToList();
        var employeeEngageRegionIds = entities.OfType<SurveyFormEmployeeEngageRegion>().Select(e => e.EmployeeEngageRegionId).ToList();
        var engageDepartmentIds = entities.OfType<SurveyFormEngageDepartment>().Select(e => e.EngageDepartmentId).ToList();
        var employeeJobTitleIds = entities.OfType<SurveyFormEmployeeJobTitle>().Select(e => e.EmployeeJobTitleId).ToList();
        var employeeDivisionIds = entities.OfType<SurveyFormEmployeeDivision>().Select(e => e.EmployeeDivisionId).ToList();

        var excludedEmployeeIds = entities.OfType<SurveyFormExcludedEmployee>().Select(e => e.ExcludedEmployeeId).ToList();

        var hasEmployeeTarget = employeeIds.Count != 0;
        var hasRegionTarget = employeeEngageRegionIds.Count != 0;
        var hasDepartmentTarget = engageDepartmentIds.Count != 0;
        var hasJobTitleTarget = employeeJobTitleIds.Count != 0;
        var hasDivisionTarget = employeeDivisionIds.Count != 0;

        var hasCriteriaTarget = hasRegionTarget || hasDepartmentTarget || hasJobTitleTarget || hasDivisionTarget;

        var queryable = Context.Employees.Include(e => e.EmployeeJobTitles)
                                            .ThenInclude(e => e.EmployeeJobTitle)
                                          .Include(e => e.EmployeeRegions)
                                            .ThenInclude(e => e.EngageRegion)
                                          .Include(e => e.EmployeeDepartments)
                                            .ThenInclude(e => e.EngageDepartment)
                                          .Include(e => e.EmployeeDivisions)
                                            .ThenInclude(e => e.EmployeeDivision)
                                          .AsQueryable()
                                          .AsNoTracking();

        var employeeList = await queryable.Where(e => (hasEmployeeTarget ? employeeIds.Contains(e.EmployeeId) : false)
                                                       || (hasCriteriaTarget
                                                          ? ((hasRegionTarget ? e.EmployeeRegions.Where(r => r.EngageRegion.Disabled == false).Any(r => employeeEngageRegionIds.Contains(r.EngageRegionId)) : true)
                                                            && (hasDepartmentTarget ? e.EmployeeDepartments.Where(d => d.EngageDepartment.Disabled == false).Any(d => engageDepartmentIds.Contains(d.EngageDepartmentId)) : true)
                                                            && (hasJobTitleTarget ? e.EmployeeJobTitles.Where(j => j.EmployeeJobTitle.Disabled == false && j.EmployeeJobTitle.Level == 3).Any(j => employeeJobTitleIds.Contains(j.EmployeeJobTitleId)) : true)
                                                            && (hasDivisionTarget ? e.EmployeeDivisions.Where(d => d.EmployeeDivision.Disabled == false).Any(d => employeeDivisionIds.Contains(d.EmployeeDivisionId)) : true))
                                                            : false))
                                            .Where(e => !excludedEmployeeIds.Contains(e.EmployeeId) && !e.Disabled && !e.Deleted)
                                            .ProjectTo<EmployeeListDto>(Mapper.ConfigurationProvider)
                                            .ToListAsync(cancellationToken);

        return employeeList;
    }
}
