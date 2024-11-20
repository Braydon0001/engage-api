using Engage.Application.Services.SurveyInstances.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGeneratePdfReportQuery : IRequest<MemoryStream>
{
    public int Id { get; set; }
    public HttpClient HttpClient { get; set; }
}
public record EmployeeStoreCalendarGeneratePdfReportHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IPdfService PdfService, IOptions<ContactReportSettings> ContactReportSettings) : IRequestHandler<EmployeeStoreCalendarGeneratePdfReportQuery, MemoryStream>
{
    public async Task<MemoryStream> Handle(EmployeeStoreCalendarGeneratePdfReportQuery query, CancellationToken cancellationToken)
    {
        var storeVisit = await Context.EmployeeStoreCalendars
                                      .AsNoTracking()
                                      .Where(e => e.EmployeeStoreCalendarId == query.Id)
                                      .ProjectTo<EmployeeStoreCalendarGeneratePdfReportDto>(Mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("store visit not found");

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

        if (storeVisit.SurveryInstanceId == null)
            throw new Exception("No survey instance found");

        var surveyAnswers = await Mediator.Send(new SurveyInstanceAllAnswersQuery { Id = storeVisit.SurveryInstanceId.Value }, cancellationToken);

        storeVisit.Answers = surveyAnswers.Data;

        var response = await query.HttpClient.GetAsync(ContactReportSettings.Value.EngageLogo, cancellationToken);

        var imageStream = await response.Content.ReadAsStreamAsync(cancellationToken);

        return await PdfService.GenerateContactReportSummaryPdfStream(
            new PdfModel<EmployeeStoreCalendarGeneratePdfReportDto>
            {
                Data = storeVisit,
                HeaderImageURL = ContactReportSettings.Value.EngageLogo,
                HeaderStream = imageStream,
                HttpClient = query.HttpClient,
            }, cancellationToken);
    }
}
