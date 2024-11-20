namespace Engage.Application.Services.Mobile.SurveyForms.Queries;

public class SurveyFormEmployeeStoreCalendarMobileQuery : IRequest<SurveyFormMobileHistoryDto>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public DateTime DateTime { get; set; }
}

public record SurveyFormEmployeeStoreCalendarMobileHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormEmployeeStoreCalendarMobileQuery, SurveyFormMobileHistoryDto>
{
    public async Task<SurveyFormMobileHistoryDto> Handle(SurveyFormEmployeeStoreCalendarMobileQuery query, CancellationToken cancellationToken)
    {

        var employeeStoreCalendar = await Context.EmployeeStoreCalendars
            .Where(e => e.StoreId == query.StoreId && e.EmployeeId == query.EmployeeId && e.CalendarDate == query.DateTime)
            .FirstOrDefaultAsync(cancellationToken);

        if (employeeStoreCalendar == null)
        {
            return new SurveyFormMobileHistoryDto();
        }


        var employeeStoreCalendarId = employeeStoreCalendar.EmployeeStoreCalendarId;
        var exisitingFormIds = await Context.EmployeeStoreCalendarSurveyFormSubmissions.Include(e => e.SurveyFormSubmission)
                                                                                           .Where(e => e.EmployeeStoreCalendarId == employeeStoreCalendarId
                                                                                           && e.SurveyFormSubmission.Created.Date == query.DateTime.Date)
                                                                                           .Select(e => e.SurveyFormSubmission.SurveyFormId)
                                                                           .ToListAsync(cancellationToken);



        var result = await Mediator.Send(new SurveyFormEmployeeStoreCalendarTargetedMobileQuery { EmployeeId = query.EmployeeId, StoreId = query.StoreId, Date = query.DateTime, ExistingFormIds = exisitingFormIds, EmployeeStoreCalendarId = employeeStoreCalendarId }, cancellationToken);

        var surveyAdvancedTargetingVm = result.SurveyFormAdvancedTargetingVm;

        var surveyList = surveyAdvancedTargetingVm.SurveyForms.Data;

        //get all the "required" flag surveys
        var requiredSurveyIds = surveyList.Where(e => e.IsRequired).Select(e => e.Id).ToList();

        //get all the surveys that have a required rule and have to be validated
        var requiredRuleSurveyIds = surveyList.Where(e => e.Rules != null && e.Rules.Any(r => r.Type == "Required Rule")).Select(e => e.Id).ToList();

        surveyAdvancedTargetingVm.SurveyForms.Data = surveyList;
        surveyAdvancedTargetingVm.SurveyForms.Count = surveyList.Count;

        return new SurveyFormMobileHistoryDto()
        {
            SurveyFormAdvancedTargeting = surveyAdvancedTargetingVm,
            RequiredSurveyIds = requiredSurveyIds,
            RequiredRuleSurveyIds = requiredRuleSurveyIds,
            StoreId = query.StoreId,
            EmployeeId = query.EmployeeId,
            History = result.SurveyFormWithHistory
        };
    }
}
