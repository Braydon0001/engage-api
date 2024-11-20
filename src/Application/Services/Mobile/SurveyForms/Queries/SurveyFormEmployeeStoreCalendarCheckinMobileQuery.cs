namespace Engage.Application.Services.Mobile.SurveyForms.Queries;

public class SurveyFormEmployeeStoreCalendarCheckinMobileQuery : IRequest<SurveyFormMobileHistoryDto>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public DateTime DateTime { get; set; }
}

public record SurveyFormEmployeeStoreCalendarCheckinMobileHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormEmployeeStoreCalendarCheckinMobileQuery, SurveyFormMobileHistoryDto>
{
    public async Task<SurveyFormMobileHistoryDto> Handle(SurveyFormEmployeeStoreCalendarCheckinMobileQuery query, CancellationToken cancellationToken)
    {
        var employeeStoreCalendar = await Context.EmployeeStoreCalendars
           .Where(e => e.StoreId == query.StoreId && e.EmployeeId == query.EmployeeId && e.CalendarDate == query.DateTime)
           .FirstOrDefaultAsync(cancellationToken);

        var blockDay = Context.EmployeeStoreCalendarBlockDays.Where(e => e.EmployeeId == query.EmployeeId && e.CalendarDate == query.DateTime).AsQueryable().AsNoTracking();


        if (employeeStoreCalendar.IsNotNull() || blockDay.IsNotNull())
        {
            return new SurveyFormMobileHistoryDto();
        }

        var result = await Mediator.Send(new SurveyFormEmployeeStoreCalendarTargetedMobileQuery { EmployeeId = query.EmployeeId, StoreId = query.StoreId, Date = query.DateTime, EmployeeStoreCalendarId = 0, ExistingFormIds = [] }, cancellationToken);

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
