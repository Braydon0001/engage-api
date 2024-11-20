// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class EmployeeStoreCalendarUpdateSurveysCommand : IMapTo<EmployeeStoreCalendar>, IRequest<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public List<int> SurveyFormIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarUpdateSurveysCommand, EmployeeStoreCalendar>();
    }
}

public class EmployeeStoreCalendarUpdateSurveysHandler : InsertHandler, IRequestHandler<EmployeeStoreCalendarUpdateSurveysCommand, EmployeeStoreCalendar>
{
    private readonly ContactReportSettings _contactReportSettings;
    private readonly FeatureSwitchOptions _featureSwitchOptions;
    private readonly IMediator _mediator;
    private readonly IUserService _user;
    public EmployeeStoreCalendarUpdateSurveysHandler(IAppDbContext context, IMapper mapper, IOptions<FeatureSwitchOptions> featureSwitchOptions, IOptions<ContactReportSettings> contactReportSettings, IMediator mediator, IUserService user) : base(context, mapper)
    {
        _contactReportSettings = contactReportSettings.Value;
        _featureSwitchOptions = featureSwitchOptions.Value;
        _mediator = mediator;
        _user = user;
    }

    public async Task<EmployeeStoreCalendar> Handle(EmployeeStoreCalendarUpdateSurveysCommand command, CancellationToken cancellationToken)
    {
        var surveys = await _context.SurveyForms.Where(e => command.SurveyFormIds.Contains(e.SurveyFormId)).ToListAsync(cancellationToken);

        var employeeStoreCalendar = await _context.EmployeeStoreCalendars.FirstOrDefaultAsync(e => e.EmployeeStoreCalendarId == command.Id, cancellationToken);

        var storeSurveys = await _context.EmployeeStoreCalendarSurveyFormSubmissions.Where(e => e.EmployeeStoreCalendarId == command.Id)
                                                                                    .Include(e => e.SurveyFormSubmission)
                                                                                    .Select(e => e.SurveyFormSubmission.SurveyFormId)
                                                                                    .ToListAsync(cancellationToken);

        List<SurveyFormSubmission> submissions = [];
        foreach (var survey in surveys.Where(e => !storeSurveys.Contains(e.SurveyFormId)))
        {
            submissions.Add(new SurveyFormSubmission
            {
                StoreId = employeeStoreCalendar.StoreId,
                SurveyFormId = survey.SurveyFormId,
                SubmissionUuid = "",
                StartedDate = employeeStoreCalendar.CalendarDate,
                EmployeeId = employeeStoreCalendar.EmployeeId,
            });
        }

        _context.SurveyFormSubmissions.AddRange(submissions);

        await _context.SaveChangesAsync(cancellationToken);

        foreach (var submission in submissions)
        {
            _context.EmployeeStoreCalendarSurveyFormSubmissions.Add(new EmployeeStoreCalendarSurveyFormSubmission
            {
                EmployeeStoreCalendarId = employeeStoreCalendar.EmployeeStoreCalendarId,
                SurveyFormSubmissionId = submission.SurveyFormSubmissionId
            });
        }

        await _context.SaveChangesAsync(cancellationToken);

        return employeeStoreCalendar;
    }
}

public class EmployeeStoreCalendarUpdateSurveysValidator : AbstractValidator<EmployeeStoreCalendarUpdateSurveysCommand>
{
    public EmployeeStoreCalendarUpdateSurveysValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormIds).NotEmpty();
        RuleForEach(e => e.SurveyFormIds).NotEmpty().GreaterThan(0);
    }
}