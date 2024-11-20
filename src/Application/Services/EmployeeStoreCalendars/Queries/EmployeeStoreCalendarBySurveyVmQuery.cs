namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarBySurveyVmQuery : IRequest<EmployeeStoreCalendarBySurveyInstanceVm>
{
    public int Id { get; set; }
}
public class EmployeeStoreCalendarBySurveyVmHandler : VmQueryHandler, IRequestHandler<EmployeeStoreCalendarBySurveyVmQuery, EmployeeStoreCalendarBySurveyInstanceVm>
{
    private readonly ContactReportSettings _contactReportSettings;
    public EmployeeStoreCalendarBySurveyVmHandler(IAppDbContext context, IMapper mapper, IOptions<ContactReportSettings> contactReportSettings) : base(context, mapper)
    {
        _contactReportSettings = contactReportSettings.Value;
    }

    public async Task<EmployeeStoreCalendarBySurveyInstanceVm> Handle(EmployeeStoreCalendarBySurveyVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendars.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Employee)
                             .Include(e => e.Store)
                             .Include(e => e.EmployeeStoreCalendarPeriod)
                             .Include(e => e.EmployeeStoreCalendarGroup)
                             .Include(e => e.SurveyInstance);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SurveyInstanceId == query.Id, cancellationToken);

        if (entity == null)
        {
            throw new Exception("No entity Found");
        }

        var mappedEntity = _mapper.Map<EmployeeStoreCalendarBySurveyInstanceVm>(entity);
        mappedEntity.JobTitles = new List<string>();
        mappedEntity.SurveyInstanceCompletionDate = entity.CompletionDate ?? DateTime.Now;

        //get CC emails from survey Instance
        var questions = await _context.SurveyAnswers
                                     .AsNoTracking()
                                     .Where(e => e.SurveyQuestionId == _contactReportSettings.QuestionTenId && e.SurveyInstanceId == query.Id
                                               || e.SurveyQuestionId == _contactReportSettings.QuestionElevenId && e.SurveyInstanceId == query.Id
                                               || e.SurveyQuestionId == _contactReportSettings.QuestionTwelveId && e.SurveyInstanceId == query.Id)
                                     .ToListAsync(cancellationToken);


        List<string> ccEmails = new();

        var employeejobTitles = await _context.Employees
                                                   .AsNoTracking()
                                                   .Where(e => e.EmployeeId == mappedEntity.EmployeeId.Id)
                                                   .Include(e => e.EmployeeJobTitles)
                                                   .Select(e => e.EmployeeJobTitles)
                                                   .FirstOrDefaultAsync(cancellationToken);

        var jobTitleIds = employeejobTitles.Select(e => e.EmployeeJobTitleId).ToList();

        mappedEntity.JobTitles = await _context.EmployeeJobTitles
                                                        .AsNoTracking()
                                                        .Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId))
                                                        .Select(e => e.Name)
                                                        .ToListAsync(cancellationToken);

        if (questions.Any())
        {
            var storeCallCycles = await _context.EmployeeStores
                                                    .AsNoTracking()
                                                    .Include(e => e.EngageSubGroup)
                                                    .Where(e => e.StoreId == mappedEntity.StoreId.Id
                                                                && jobTitleIds.Contains(e.EngageSubGroup.EngageDepartmentId))
                                                    .ToListAsync(cancellationToken);

            var employeeCalls = storeCallCycles.DistinctBy(e => e.EmployeeId).Select(e => e.EmployeeId).ToList();

            if (questions.Find(e => e.SurveyQuestionId == _contactReportSettings.QuestionTenId) != null)
            {
                var freshRep = await _context.EmployeeEmployeeJobTitles
                                                    .AsNoTracking()
                                                    .Where(e => employeeCalls.Contains(e.EmployeeId)
                                                        && e.EmployeeJobTitleId == _contactReportSettings.FreshRepJobTitleId)
                                                    .Include(e => e.Employee)
                                                    .Select(e => e.Employee.EmailAddress1)
                                                    .ToListAsync(cancellationToken);

                if (freshRep != null)
                {
                    ccEmails.AddRange(freshRep);
                }
            }
            if (questions.Find(e => e.SurveyQuestionId == _contactReportSettings.QuestionElevenId) != null)
            {

                var teleSales = await _context.EmployeeEmployeeJobTitles
                                                    .AsNoTracking()
                                                    .Where(e => employeeCalls.Contains(e.EmployeeId)
                                                        && e.EmployeeJobTitleId == _contactReportSettings.FreshTeleSalesJobTitleId)
                                                    .Include(e => e.Employee)
                                                    .Select(e => e.Employee.EmailAddress1)
                                                    .ToListAsync(cancellationToken);

                if (teleSales != null)
                {
                    ccEmails.AddRange(teleSales);
                }
            }
            if (questions.Find(e => e.SurveyQuestionId == _contactReportSettings.QuestionTwelveId) != null)
            {
                var manager = await _context.Employees
                                                 .AsNoTracking()
                                                 .Where(e => e.EmployeeId == mappedEntity.EmployeeId.Id
                                                                && e.Disabled == false)
                                                 .Include(e => e.Manager)
                                                 .Select(e => e.Manager.EmailAddress1)
                                                 .FirstOrDefaultAsync(cancellationToken);

                if (manager != null)
                {
                    ccEmails.Add(manager);
                }
            }
        }

        mappedEntity.CCEmails = ccEmails;
        mappedEntity.EngageLogo = _contactReportSettings.EngageLogo;

        return mappedEntity;
    }
}