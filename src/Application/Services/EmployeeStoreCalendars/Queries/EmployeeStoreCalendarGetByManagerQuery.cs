namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGetByManagerQuery : IRequest<ListResult<EmployeeStoreCalendarManagerViewDto>>
{
    public int EmployeeId { get; set; }
}
public class EmployeeStoreCalendarGetByManagerHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarGetByManagerQuery, ListResult<EmployeeStoreCalendarManagerViewDto>>
{
    public EmployeeStoreCalendarGetByManagerHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeStoreCalendarManagerViewDto>> Handle(EmployeeStoreCalendarGetByManagerQuery request, CancellationToken cancellationToken)
    {
        var variables = await _context.EmployeeStoreCalendars
            .AsNoTracking()
            .OrderByDescending(e => e.CalendarDate)
            .Where(e => e.Employee.ManagerId == request.EmployeeId && e.Disabled == false)
            .Include(e => e.Employee)
            .Include(e => e.Store)
            .Include(e => e.EmployeeStoreCalendarPeriod)
            .ToListAsync(cancellationToken);

        List<EmployeeStoreCalendarManagerViewDto> mappedEntities = new List<EmployeeStoreCalendarManagerViewDto>();

        var employeeIds = variables.Select(e => e.EmployeeId).Distinct();

        foreach (var employeeId in employeeIds)
        {
            var employeeVisits = variables.Where(e => e.EmployeeId == employeeId).OrderByDescending(e => e.CalendarDate).ToList();
            mappedEntities.Add(_mapper.Map<EmployeeStoreCalendarManagerViewDto>(employeeVisits.First()));
        }

        //foreach (var item in variables)
        //{
        //    var mappedItem = _mapper.Map<EmployeeStoreCalendarManagerViewDto>(item);
        //    mappedEntities.Add(mappedItem);
        //}

        return new ListResult<EmployeeStoreCalendarManagerViewDto>
        {
            Data = mappedEntities,
            Count = mappedEntities.Count
        };
    }
}