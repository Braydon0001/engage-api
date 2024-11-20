namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGetSurveyFormsQuery : IRequest<EmployeeStoreCalendarSurveyFormsDto>
{
    public int Id { get; set; }
}
public record EmployeeStoreCalendarGetSurveyFormsHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<EmployeeStoreCalendarGetSurveyFormsQuery, EmployeeStoreCalendarSurveyFormsDto>
{
    public async Task<EmployeeStoreCalendarSurveyFormsDto> Handle(EmployeeStoreCalendarGetSurveyFormsQuery query, CancellationToken cancellationToken)
    {
        List<SurveyForm> surveyForms = [];
        var employeeStoreCalendar = await Context.EmployeeStoreCalendars.Where(e => e.EmployeeStoreCalendarId == query.Id)
                                                                        .FirstOrDefaultAsync(cancellationToken);

        var exisitingSubmissions = await Context.EmployeeStoreCalendarSurveyFormSubmissions.Include(e => e.SurveyFormSubmission)
                                                                                           .Where(e => e.EmployeeStoreCalendarId == query.Id)
                                                                                           .ToListAsync(cancellationToken);

        if (employeeStoreCalendar != null)
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
        }
        var entity = await Context.EmployeeStoreCalendars.AsNoTracking()
                                                         .Include(e => e.Employee)
                                                         .Include(e => e.Store)
                                                         .Include(e => e.SurveyFormSubmissions)
                                                            .ThenInclude(e => e.SurveyFormSubmission)
                                                                .ThenInclude(e => e.SurveyForm)
                                                         .FirstOrDefaultAsync(e => e.EmployeeStoreCalendarId == query.Id, cancellationToken);

        return Mapper.Map<EmployeeStoreCalendarSurveyFormsDto>(entity);
    }
}
