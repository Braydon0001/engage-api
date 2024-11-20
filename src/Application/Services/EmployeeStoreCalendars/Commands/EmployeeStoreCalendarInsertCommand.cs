// auto-generated
using Engage.Application.Services.EmployeeStoreCalendars.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class EmployeeStoreCalendarInsertCommand : IMapTo<EmployeeStoreCalendar>, IRequest<EmployeeStoreCalendar>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public DateTime CalendarDate { get; set; }
    public int Order { get; set; }
    public int EmployeeStoreCalendarGroupId { get; set; }
    public int? SurveyInstanceId { get; set; }
    public bool IsManagerCreated { get; set; }
    public int? EmployeeStoreCalendarTypeId { get; set; }
    public int? EmployeeStoreCalendarStatusId { get; set; } = 1;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarInsertCommand, EmployeeStoreCalendar>();
    }
}

public class EmployeeStoreCalendarInsertHandler : InsertHandler, IRequestHandler<EmployeeStoreCalendarInsertCommand, EmployeeStoreCalendar>
{
    private readonly ContactReportSettings _contactReportSettings;
    private readonly FeatureSwitchOptions _featureSwitchOptions;
    private readonly IMediator _mediator;
    private readonly IUserService _user;
    public EmployeeStoreCalendarInsertHandler(IAppDbContext context, IMapper mapper, IOptions<FeatureSwitchOptions> featureSwitchOptions, IOptions<ContactReportSettings> contactReportSettings, IMediator mediator, IUserService user) : base(context, mapper)
    {
        _contactReportSettings = contactReportSettings.Value;
        _featureSwitchOptions = featureSwitchOptions.Value;
        _mediator = mediator;
        _user = user;
    }

    public async Task<EmployeeStoreCalendar> Handle(EmployeeStoreCalendarInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeStoreCalendarInsertCommand, EmployeeStoreCalendar>(command);
        List<SurveyForm> surveyForms = [];

        var employeeStoreCalendar = await _context.EmployeeStoreCalendars.Where(e => e.EmployeeStoreCalendarId == entity.EmployeeStoreCalendarId)
                                                                        .FirstOrDefaultAsync(cancellationToken);

        if (_featureSwitchOptions.NewContactReport)
        {
            if (employeeStoreCalendar == null)
            {
                surveyForms.AddRange(await _mediator.Send(new EmployeeStoreCalendarEmployeeTargetingQuery { EmployeeId = command.EmployeeId, StoreId = command.StoreId, DateTime = command.CalendarDate }, cancellationToken));

            }
            //if (surveyForms.Count == 0)
            //{
            //    throw new Exception("No contact reports found");
            //}
        }

        var blockedDay = await _context.EmployeeStoreCalendarBlockDays
                                       .Where(e => e.CalendarDate.Date == entity.CalendarDate.Date
                                            && e.EmployeeId == entity.EmployeeId
                                            && e.Disabled == false
                                            && e.EmployeeStoreCalendarTypeId == (int)EmployeeStoreCalendarTypeId.BlockDay)
                                       .FirstOrDefaultAsync(cancellationToken);

        if (blockedDay != null)
        {
            throw new Exception("Cannot add store visit on blocked day");
        }

        //check if there is a previous calendar visit on this day
        var pastEntity = await _context.EmployeeStoreCalendars
                                    .Where(e =>
                                            e.EmployeeId == entity.EmployeeId
                                            && e.StoreId == entity.StoreId
                                    && e.CalendarDate == entity.CalendarDate
                                    )
                                    .FirstOrDefaultAsync(cancellationToken);
        //reactivate past entity
        if (pastEntity != null)
        {
            pastEntity.Disabled = false;
            await _context.SaveChangesAsync(cancellationToken);

            //return pastEntity;
        }

        var calendarPeriod = await _context.EmployeeStoreCalendarPeriods.SingleOrDefaultAsync(e =>
        command.CalendarDate.Date >= e.StartDate.Date && command.CalendarDate.Date <= e.EndDate.Date, cancellationToken);

        if (calendarPeriod == null)
        {
            throw new Exception("No Calendar Period Found");
        }
        entity.EmployeeStoreCalendarPeriodId = calendarPeriod.EmployeeStoreCalendarPeriodId;

        //Using old surveys
        if (!_featureSwitchOptions.NewContactReport)
        {
            var existingSurveyInstance = await _context.SurveyInstances
                                            .Where(s => s.EmployeeId == entity.EmployeeId
                                                        && s.StoreId == entity.StoreId
                                                        && s.SurveyDate == entity.CalendarDate.Date
                                                        && s.SurveyId == _contactReportSettings.ContactReportSurveyId)
                                            .FirstOrDefaultAsync(cancellationToken);

            if (existingSurveyInstance != null)
            {
                throw new Exception("Employee Store Combination already exists for this Date.");
            }

            var surveyInstance = new SurveyInstance
            {
                EmployeeId = entity.EmployeeId,
                StoreId = entity.StoreId,
                SurveyId = _contactReportSettings.ContactReportSurveyId,
                SurveyDate = entity.CalendarDate
            };

            //default
            entity.EmployeeStoreCalendarTypeId = 1;

            entity.SurveyInstance = surveyInstance;

            _context.EmployeeStoreCalendars.Add(entity);
        }
        else
        {
            if (pastEntity == null)
            {
                _context.EmployeeStoreCalendars.Add(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);

            var exisitingSubmissions = await _context.EmployeeStoreCalendarSurveyFormSubmissions.Include(e => e.SurveyFormSubmission)
                                                                                           .Where(e => e.EmployeeStoreCalendarId == entity.EmployeeStoreCalendarId)
                                                                                           .ToListAsync(cancellationToken);

            var tagertedSurveyFormIds = surveyForms.Select(e => e.SurveyFormId).ToList();
            var existingSurveyFormIds = exisitingSubmissions.Select(e => e.SurveyFormSubmission.SurveyFormId).ToList();

            var idsToAdd = tagertedSurveyFormIds.Except(existingSurveyFormIds).ToList();

            if (idsToAdd.Count > 0)
            {
                List<SurveyFormSubmission> submissions = [];
                foreach (var survey in surveyForms)
                {
                    submissions.Add(new SurveyFormSubmission
                    {
                        StoreId = command.StoreId,
                        SurveyFormId = survey.SurveyFormId,
                        SubmissionUuid = "",
                        StartedDate = command.CalendarDate,
                        EmployeeId = command.EmployeeId,
                    });
                }

                _context.SurveyFormSubmissions.AddRange(submissions);

                await _context.SaveChangesAsync(cancellationToken);
                foreach (var submission in submissions)
                {
                    if (pastEntity == null)
                    {
                        _context.EmployeeStoreCalendarSurveyFormSubmissions.Add(new EmployeeStoreCalendarSurveyFormSubmission
                        {
                            EmployeeStoreCalendarId = entity.EmployeeStoreCalendarId,
                            SurveyFormSubmissionId = submission.SurveyFormSubmissionId
                        });
                    }
                    else
                    {
                        //TODO?: Remove previous entries into this table for this store visit
                        _context.EmployeeStoreCalendarSurveyFormSubmissions.Add(new EmployeeStoreCalendarSurveyFormSubmission
                        {
                            EmployeeStoreCalendarId = pastEntity.EmployeeStoreCalendarId,
                            SurveyFormSubmissionId = submission.SurveyFormSubmissionId
                        });
                    }
                }
            }



        }

        await _context.SaveChangesAsync(cancellationToken);

        if (command.IsManagerCreated)
        {
            var managerName = await _context.Users.Where(e => e.Email == _user.UserName)
                .Select(e => e.FullName)
                .FirstOrDefaultAsync(cancellationToken);

            if (managerName != null)
            {
                await _mediator.Send(new EmployeeStoreCalendarManagerEmailCommand
                {
                    EmployeeStoreCalendarId = entity.EmployeeStoreCalendarId,
                    ManagerName = managerName
                }, cancellationToken);
            }
        }

        return entity;
    }
}

public class EmployeeStoreCalendarInsertValidator : AbstractValidator<EmployeeStoreCalendarInsertCommand>
{
    public EmployeeStoreCalendarInsertValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CalendarDate).NotEmpty();
        RuleFor(e => e.Order);
        RuleFor(e => e.EmployeeStoreCalendarGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyInstanceId);
    }
}