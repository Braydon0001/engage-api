using Engage.Application.Services.SurveyFormSubmissions.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGenerateSurveyFormPdfReportQuery : IRequest<MemoryStream>
{
    public int Id { get; set; }
    //public int SurveyFormId { get; set; }
    public HttpClient HttpClient { get; set; }
    public List<int> SubmissionIds { get; set; } = [];
}

public record EmployeeStoreCalendarGenerateSurveyFormPdfReportHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IPdfService PdfService, IOptions<ContactReportSettings> ContactReportSettings) : IRequestHandler<EmployeeStoreCalendarGenerateSurveyFormPdfReportQuery, MemoryStream>
{
    public async Task<MemoryStream> Handle(EmployeeStoreCalendarGenerateSurveyFormPdfReportQuery query, CancellationToken cancellationToken)
    {
        var employeeStoreCal = await Context.EmployeeStoreCalendars
                                      .AsNoTracking()
                                      .Include(e => e.Employee)
                                      .Include(e => e.Store)
                                      .Include(e => e.SurveyFormSubmissions)
                                      .ThenInclude(e => e.SurveyFormSubmission)
                                      .Where(e => e.EmployeeStoreCalendarId == query.Id)
        .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("store visit not found");

        var surveyIds = employeeStoreCal.SurveyFormSubmissions.Where(e => e.SurveyFormSubmission.IsComplete == true).Select(e => e.SurveyFormSubmissionId).ToList();

        if (query.SubmissionIds.Any())
        {
            surveyIds = surveyIds.Where(e => query.SubmissionIds.Contains(e)).ToList();
        }

        var storeVisit = Mapper.Map<EmployeeStoreCalendarGenerateSurveyFormPdfReportDto>(employeeStoreCal);

        var employeejobtitle = await Context.Employees
                                                   .AsNoTracking()
                                                   .Where(e => e.EmployeeId == storeVisit.EmployeeId)
                                                   .Include(e => e.EmployeeJobTitles)
                                                   .Select(e => e.EmployeeJobTitles)
                                                   .FirstOrDefaultAsync(cancellationToken);
        var jobTitleIds = employeejobtitle.Select(e => e.EmployeeJobTitleId).ToList();

        storeVisit.JobTitles = await Context.EmployeeJobTitles
                                                        .AsNoTracking()
                                                        .Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId))
                                                        .Select(e => e.Name)
                                                        .ToListAsync(cancellationToken);

        storeVisit.SurveySubmission = [];
        foreach (var submissionId in surveyIds)
        {
            var submisson = await Mediator.Send(new SurveyFormSubmissionSummaryQuery { Id = submissionId }, cancellationToken);
            storeVisit.SurveySubmission.Add(submisson);
        }

        var response = await query.HttpClient.GetAsync(ContactReportSettings.Value.EngageLogo, cancellationToken);

        var imageStream = await response.Content.ReadAsStreamAsync(cancellationToken);

        return await PdfService.GenerateSurveyFormContactReportSummaryPdfStream(
            new PdfModel<EmployeeStoreCalendarGenerateSurveyFormPdfReportDto>
            {
                Data = storeVisit,
                HeaderImageURL = ContactReportSettings.Value.EngageLogo,
                HeaderStream = imageStream,
                HttpClient = query.HttpClient
            }, cancellationToken);
    }
}
