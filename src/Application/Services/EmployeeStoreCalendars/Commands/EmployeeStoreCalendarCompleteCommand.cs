using Engage.Application.Services.EntityContacts.Models;

namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class EmployeeStoreCalendarCompleteCommand : IRequest<SurveyFormSubmission>
{
    public int Id { get; set; }
    public int EmployeeStoreCalendarId { get; set; }
    public string Note { get; set; }
    public List<StoreContactEmailOption> EmailAddresses { get; set; }
}
public record EmployeeStoreCalendarCompleteHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeStoreCalendarCompleteCommand, SurveyFormSubmission>
{
    public async Task<SurveyFormSubmission> Handle(EmployeeStoreCalendarCompleteCommand request, CancellationToken cancellationToken)
    {
        var surveySubmission = await Context.SurveyFormSubmissions.FirstOrDefaultAsync(e => e.SurveyFormSubmissionId == request.Id);

        if (surveySubmission == null)
        {
            return null;
        }

        surveySubmission.Note = request.Note;
        surveySubmission.IsComplete = true;
        surveySubmission.CompletedDate = DateTime.UtcNow;

        var calendar = await Context.EmployeeStoreCalendars
                                    .Include(e => e.SurveyFormSubmissions)
                                    .ThenInclude(e => e.SurveyFormSubmission)
                                    .FirstOrDefaultAsync(e => e.EmployeeStoreCalendarId == request.EmployeeStoreCalendarId, cancellationToken);

        if (calendar == null)
        {
            return null;
        }

        var surveysComplete = calendar.SurveyFormSubmissions.Select(e => e.SurveyFormSubmission.IsComplete).ToList();

        if (!surveysComplete.Contains(false))
        {
            calendar.CompletionDate = DateTime.Now;
        }

        await Context.SaveChangesAsync(cancellationToken);

        return surveySubmission;
    }
}