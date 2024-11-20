namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGetEmployeesByPeriodQuery : IRequest<List<EmployeeStoreCalendarGetEmployeesByPeriodDto>>
{
    public DateTime Date { get; set; }
}

public class EmployeeStoreCalendarCurrentPeriodEmployeesHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarGetEmployeesByPeriodQuery, List<EmployeeStoreCalendarGetEmployeesByPeriodDto>>
{
    public EmployeeStoreCalendarCurrentPeriodEmployeesHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarGetEmployeesByPeriodDto>> Handle(EmployeeStoreCalendarGetEmployeesByPeriodQuery request, CancellationToken cancellationToken)
    {
        var period = await _context.EmployeeStoreCalendarPeriods
            .AsNoTracking()
            .Where(e => request.Date >= e.StartDate
                && request.Date <= e.EndDate
                && e.Disabled == false)
            .FirstOrDefaultAsync(cancellationToken);

        if (period == null)
        {
            throw new NotFoundException("No period found", period);
        }

        var employees = await _context.EmployeeStoreCalendars
            .AsNoTracking()
            .Where(e => e.EmployeeStoreCalendarPeriodId == period.EmployeeStoreCalendarPeriodId)
            .ProjectTo<EmployeeStoreCalendarGetEmployeesByPeriodDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        employees = employees.DistinctBy(e => e.Id).ToList();

        return employees;
    }
}