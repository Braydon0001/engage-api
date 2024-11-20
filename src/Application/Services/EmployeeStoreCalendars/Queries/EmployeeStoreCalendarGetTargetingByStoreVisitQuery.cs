namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGetTargetingByStoreVisitQuery : IRequest<EmployeeStoreCalendarAdvancedTargetingVm>
{
    public int EmployeeStoreCalendarId { get; set; }
}
public record EmployeeStoreCalendarGetTargetingByStoreVisitHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<EmployeeStoreCalendarGetTargetingByStoreVisitQuery, EmployeeStoreCalendarAdvancedTargetingVm>
{
    public async Task<EmployeeStoreCalendarAdvancedTargetingVm> Handle(EmployeeStoreCalendarGetTargetingByStoreVisitQuery query, CancellationToken cancellationToken)
    {
        var employeeStoreCalendar = await Context.EmployeeStoreCalendars.FirstOrDefaultAsync(e => e.EmployeeStoreCalendarId == query.EmployeeStoreCalendarId, cancellationToken);

        if (employeeStoreCalendar == null)
        {
            throw new Exception("Store visit not found");
        }

        var surveyIds = await Context.EmployeeStoreCalendarSurveyFormSubmissions.Where(e => e.EmployeeStoreCalendarId == query.EmployeeStoreCalendarId)
            .Include(e => e.SurveyFormSubmission).Select(e => e.SurveyFormSubmission.SurveyFormId).ToListAsync(cancellationToken);

        var baseTargetedSurveys = await Mediator.Send(new EmployeeStoreCalendarEmployeeTargetingQuery { EmployeeId = employeeStoreCalendar.EmployeeId, StoreId = employeeStoreCalendar.StoreId, DateTime = employeeStoreCalendar.CalendarDate }, cancellationToken);

        baseTargetedSurveys = baseTargetedSurveys.Where(e => !surveyIds.Contains(e.SurveyFormId)).ToList();


        var employeeSubgroups = await Context.EmployeeStores.Where(e => e.EmployeeId == employeeStoreCalendar.EmployeeId)
                                                            .Select(e => e.EngageSubGroupId)
                                                            .ToListAsync(cancellationToken: cancellationToken);

        var employeeEngageRegionIds = await Context.EmployeeRegions.Where(e => e.EmployeeId == employeeStoreCalendar.EmployeeId
                                                                                && e.EngageRegion.Disabled == false)
                                                                   .Select(e => e.EngageRegionId)
                                                                   .ToListAsync(cancellationToken);

        var engageDepartmentIds = await Context.EmployeeDepartments.Where(e => e.EmployeeId == employeeStoreCalendar.EmployeeId
                                                                                && e.EngageDepartment.Disabled == false)
                                                              .Select(e => e.EngageDepartmentId)
                                                              .ToListAsync(cancellationToken);

        var jobTitleIds = await Context.EmployeeEmployeeJobTitles.Where(e => e.EmployeeId == employeeStoreCalendar.EmployeeId
                                                                            && e.EmployeeJobTitle.Disabled == false
                                                                            && e.EmployeeJobTitle.Level == 3)
                                                                 .Select(e => e.EmployeeJobTitleId)
                                                                 .ToListAsync(cancellationToken);

        var employeeDivisionIds = await Context.EmployeeEmployeeDivisions.Where(e => e.EmployeeId == employeeStoreCalendar.EmployeeId
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
        targeting.Employees = [employeeStoreCalendar.EmployeeId];
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
