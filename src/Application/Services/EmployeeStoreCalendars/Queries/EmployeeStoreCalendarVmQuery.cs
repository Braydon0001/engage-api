// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarVmQuery : IRequest<EmployeeStoreCalendarVm>
{
    public int Id { get; set; }
}

public class EmployeeStoreCalendarVmHandler : VmQueryHandler, IRequestHandler<EmployeeStoreCalendarVmQuery, EmployeeStoreCalendarVm>
{
    private readonly IUserService _userService;
    public EmployeeStoreCalendarVmHandler(IAppDbContext context, IMapper mapper, IUserService userService) : base(context, mapper)
    {
        _userService = userService;
    }

    public async Task<EmployeeStoreCalendarVm> Handle(EmployeeStoreCalendarVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendars.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Employee)
                             .Include(e => e.Store)
                             .Include(e => e.EmployeeStoreCalendarType)
                             .Include(e => e.EmployeeStoreCalendarStatus)
                             .Include(e => e.EmployeeStoreCalendarPeriod)
                             .Include(e => e.EmployeeStoreCalendarGroup)
                             .Include(e => e.SurveyInstance)
                             .Include(e => e.SurveyFormSubmissions)
                             .ThenInclude(e => e.SurveyFormSubmission)
                             .ThenInclude(e => e.SurveyForm);

        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarId == query.Id, cancellationToken);

        var mappedEntity = entity == null ? null : _mapper.Map<EmployeeStoreCalendarVm>(entity);

        if (mappedEntity != null)
        {
            mappedEntity.EmployeeEmail = _userService.UserName;
        }

        return mappedEntity;
    }
}