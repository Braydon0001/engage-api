namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarSurveyQuery : IRequest<EmployeeStoreCalendarSurveyVm>
{
    public int Id { get; set; }
}
public class EmployeeStoreCalendarSurveyHandler : VmQueryHandler, IRequestHandler<EmployeeStoreCalendarSurveyQuery, EmployeeStoreCalendarSurveyVm>
{
    public EmployeeStoreCalendarSurveyHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarSurveyVm> Handle(EmployeeStoreCalendarSurveyQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendars.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Employee)
                             .Include(e => e.Store)
                             .Include(e => e.EmployeeStoreCalendarPeriod)
                             .Include(e => e.EmployeeStoreCalendarGroup)
                             .Include(e => e.SurveyInstance);

        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeStoreCalendarSurveyVm>(entity);
    }
}
