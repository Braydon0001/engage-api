
using Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGetSurveyFormsMobileQuery : IRequest<List<EmployeeStoreCalendarSurveyFormsDto>>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
}
public record EmployeeStoreCalendarGetSurveyFormsMobileHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<EmployeeStoreCalendarGetSurveyFormsMobileQuery, List<EmployeeStoreCalendarSurveyFormsDto>>
{
    public async Task<List<EmployeeStoreCalendarSurveyFormsDto>> Handle(EmployeeStoreCalendarGetSurveyFormsMobileQuery query, CancellationToken cancellationToken)
    {
        List<SurveyForm> surveyForms = [];
        List<EmployeeStoreCalendarSurveyFormsDto> entities = [];

        var storeId = await Context.Stores.Where(e => e.StoreId == query.Id).Select(e => e.StoreId).FirstOrDefaultAsync(cancellationToken);

        var employeeStoreCalendars = await Context.EmployeeStoreCalendars.Where(e => e.StoreId == storeId && e.EmployeeId == query.EmployeeId && e.CalendarDate.Date < DateTime.Today).OrderByDescending(e => e.CalendarDate).Take(5).ToListAsync(cancellationToken);
        var employeeStoreCalendarIds = employeeStoreCalendars.Select(e => e.EmployeeStoreCalendarId).ToList();

        var exisitingSubmissions = await Context.EmployeeStoreCalendarSurveyFormSubmissions.Include(e => e.SurveyFormSubmission)
                                                                                           .Where(e => employeeStoreCalendarIds.Contains(e.EmployeeStoreCalendarId))
                                                                           .ToListAsync(cancellationToken);

        if (employeeStoreCalendars.Any())
        {
            foreach (var employeeStoreCalendar in employeeStoreCalendars)
            {
                surveyForms.AddRange(await Mediator.Send(new EmployeeStoreCalendarEmployeeTargetingQuery { EmployeeId = employeeStoreCalendar.EmployeeId, StoreId = employeeStoreCalendar.StoreId, DateTime = employeeStoreCalendar.CalendarDate }, cancellationToken));

                if (surveyForms.Count > 0)
                {
                    var tagertedSurveyFormIds = surveyForms.Select(e => e.SurveyFormId).ToList();
                    var existingSurveyFormIds = exisitingSubmissions.Select(e => e.SurveyFormSubmission.SurveyFormId).ToList();

                    var idsToAdd = tagertedSurveyFormIds.Except(existingSurveyFormIds).ToList();

                    if (idsToAdd.Count > 0)
                    {
                        List<SurveyFormSubmission> submissions = [];
                        foreach (var id in idsToAdd)
                        {
                            var submission = new SurveyFormSubmission
                            {
                                StoreId = employeeStoreCalendar.StoreId,
                                SurveyFormId = id,
                                SubmissionUuid = "",
                                StartedDate = employeeStoreCalendar.CalendarDate,
                                EmployeeId = employeeStoreCalendar.EmployeeId,
                            };

                            submissions.Add(submission);

                            Context.EmployeeStoreCalendarSurveyFormSubmissions.Add(new EmployeeStoreCalendarSurveyFormSubmission
                            {
                                EmployeeStoreCalendarId = employeeStoreCalendar.EmployeeStoreCalendarId,
                                SurveyFormSubmission = submission
                            });
                        }

                        if (submissions.Count > 0)
                        {
                            Context.SurveyFormSubmissions.AddRange(submissions);

                            await Context.SaveChangesAsync(cancellationToken);
                        }
                    }
                }
                var entity = await Context.EmployeeStoreCalendars.AsNoTracking()
                                                       .Include(e => e.Employee)
                                                       .Include(e => e.Store)
                                                       .Include(e => e.SurveyFormSubmissions)
                                                          .ThenInclude(e => e.SurveyFormSubmission)
                                                            .ThenInclude(e => e.SurveyForm)
                                                       .FirstOrDefaultAsync(e => e.EmployeeStoreCalendarId == employeeStoreCalendar.EmployeeStoreCalendarId, cancellationToken);

                entities.AddRange(Mapper.Map<EmployeeStoreCalendarSurveyFormsDto>(entity));
            }
        }





        return entities;
    }
}
