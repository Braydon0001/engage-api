// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class EmployeeStoreCalendarUpdateCommand : IMapTo<EmployeeStoreCalendar>, IRequest<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public DateTime CalendarDate { get; set; }
    public string Note { get; set; }
    public int Order { get; set; }
    public int EmployeeStoreCalendarPeriodId { get; set; }
    public int EmployeeStoreCalendarGroupId { get; set; }
    public int? SurveyInstanceId { get; set; }
    public bool IsNewSurvey { get; set; } = false;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarUpdateCommand, EmployeeStoreCalendar>();
    }
}

public class EmployeeStoreCalendarUpdateHandler : UpdateHandler, IRequestHandler<EmployeeStoreCalendarUpdateCommand, EmployeeStoreCalendar>
{
    private readonly ContactReportSettings _contactReportSettings;
    public EmployeeStoreCalendarUpdateHandler(IAppDbContext context, IMapper mapper, IOptions<ContactReportSettings> contactReportSettings) : base(context, mapper)
    {
        _contactReportSettings = contactReportSettings.Value;
    }

    public async Task<EmployeeStoreCalendar> Handle(EmployeeStoreCalendarUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreCalendars
                            .Include(e => e.SurveyInstance)
                            .Include(e => e.SurveyFormSubmissions)
                            .ThenInclude(e => e.SurveyFormSubmission)
                            .FirstOrDefaultAsync(e => e.EmployeeStoreCalendarId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        if (command.SurveyInstanceId == 0)
        {
            command.SurveyInstanceId = null;
        }

        var blockedDay = await _context.EmployeeStoreCalendarBlockDays
                                       .Where(e => e.EmployeeId == command.EmployeeId
                                            && e.CalendarDate.Date == command.CalendarDate.Date
                                            && e.EmployeeStoreCalendarTypeId == (int)EmployeeStoreCalendarTypeId.BlockDay)
                                       .FirstOrDefaultAsync(cancellationToken);
        if (blockedDay != null)
        {
            throw new Exception("Cannot move store visit to blocked day");
        }

        if (command.CalendarDate != entity.CalendarDate)
        {
            var calendarPeriod = await _context.EmployeeStoreCalendarPeriods.SingleOrDefaultAsync(e =>
                command.CalendarDate.Date >= e.StartDate.Date && command.CalendarDate.Date <= e.EndDate.Date, cancellationToken);

            if (calendarPeriod == null)
            {
                throw new Exception("No Calendar Period Found");
            }
            command.EmployeeStoreCalendarPeriodId = calendarPeriod.EmployeeStoreCalendarPeriodId;

            if (command.IsNewSurvey)
            {
                var submissionIds = entity.SurveyFormSubmissions.Select(e => e.SurveyFormSubmissionId).ToList();
                var employeeStoreCalendar = await _context.EmployeeStoreCalendars
                                                          .Where(e => e.EmployeeId == entity.EmployeeId
                                                            && e.StoreId == entity.StoreId
                                                            && e.CalendarDate == command.CalendarDate)
                                                          .FirstOrDefaultAsync(cancellationToken);
                if (employeeStoreCalendar != null)
                {
                    throw new Exception("Employee Store Combination already exists for this Date.");
                }

                var submissions = await _context.SurveyFormSubmissions.Where(e => submissionIds.Contains(e.SurveyFormSubmissionId))
                                                                      .ToListAsync(cancellationToken);

                //foreach ( var submission in submissions)
                //{
                //    //submission.date
                //}
            }
            else
            {
                var surveyInstance = await _context.SurveyInstances
                                        .Where(e => e.EmployeeId == entity.EmployeeId
                                                    && e.StoreId == entity.SurveyInstance.StoreId
                                                    && e.SurveyDate == command.CalendarDate
                                                    && e.SurveyId == _contactReportSettings.ContactReportSurveyId)
                                        .FirstOrDefaultAsync(cancellationToken);

                if (surveyInstance != null)
                {
                    throw new Exception("Employee Store Combination already exists for this Date.");
                }
                else
                {
                    entity.SurveyInstance.SurveyDate = command.CalendarDate;
                }
            }
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeStoreCalendarValidator : AbstractValidator<EmployeeStoreCalendarUpdateCommand>
{
    public UpdateEmployeeStoreCalendarValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CalendarDate).NotEmpty();
        RuleFor(e => e.Order);
        RuleFor(e => e.EmployeeStoreCalendarPeriodId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeStoreCalendarGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyInstanceId);
        RuleFor(e => e.Note).MaximumLength(200);
    }
}