namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarEmployeeGetTargetingQuery : IRequest<EmployeeStoreCalendarAdvancedTargetingVm>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public DateTime DateTime { get; set; }
}
public record EmployeeStoreCalendarEmployeeGetTargetingHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<EmployeeStoreCalendarEmployeeGetTargetingQuery, EmployeeStoreCalendarAdvancedTargetingVm>
{
    public async Task<EmployeeStoreCalendarAdvancedTargetingVm> Handle(EmployeeStoreCalendarEmployeeGetTargetingQuery query, CancellationToken cancellationToken)
    {
        var baseTargetedSurveys = await Mediator.Send(new EmployeeStoreCalendarEmployeeTargetingQuery { EmployeeId = query.EmployeeId, StoreId = query.StoreId, DateTime = query.DateTime }, cancellationToken);


        var employeeSubgroups = await Context.EmployeeStores.Where(e => e.EmployeeId == query.EmployeeId)
                                                            .Select(e => e.EngageSubGroupId)
                                                            .ToListAsync(cancellationToken: cancellationToken);

        var employeeEngageRegionIds = await Context.EmployeeRegions.Where(e => e.EmployeeId == query.EmployeeId
                                                                                && e.EngageRegion.Disabled == false)
                                                                   .Select(e => e.EngageRegionId)
                                                                   .ToListAsync(cancellationToken);

        var engageDepartmentIds = await Context.EmployeeDepartments.Where(e => e.EmployeeId == query.EmployeeId
                                                                                && e.EngageDepartment.Disabled == false)
                                                              .Select(e => e.EngageDepartmentId)
                                                              .ToListAsync(cancellationToken);

        var jobTitleIds = await Context.EmployeeEmployeeJobTitles.Where(e => e.EmployeeId == query.EmployeeId
                                                                            && e.EmployeeJobTitle.Disabled == false
                                                                            && e.EmployeeJobTitle.Level == 3)
                                                                 .Select(e => e.EmployeeJobTitleId)
                                                                 .ToListAsync(cancellationToken);

        var employeeDivisionIds = await Context.EmployeeEmployeeDivisions.Where(e => e.EmployeeId == query.EmployeeId
                                                                                && e.EmployeeDivision.Disabled == false)
                                                            .Select(e => e.EmployeeDivisionId)
                                                            .ToListAsync(cancellationToken);

        EmployeeStoreCalendarAdvancedTargetingVm targeting = new();

        var hasRules = false;

        var surveysWithRules = baseTargetedSurveys.Where(e => e.Rules != null).ToList();

        if (surveysWithRules.Count != 0)
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

        targeting.HasAdvancedTargeting = hasRules;
        targeting.Employees = [query.EmployeeId];
        targeting.EmployeeEngageRegions = employeeEngageRegionIds;
        targeting.EmployeeJobTitles = jobTitleIds;
        targeting.EmployeeEngageDepartments = engageDepartmentIds;
        targeting.EmployeeDivisions = employeeDivisionIds;

        targeting.SurveyForms = new(baseTargetedSurveys);

        //foreach (var survey in baseTargetedSurveys)
        //{

        //}

        return targeting;
    }
}
