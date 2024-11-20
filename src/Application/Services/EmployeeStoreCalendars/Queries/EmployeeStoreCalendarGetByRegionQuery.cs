using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGetByRegionQuery : IRequest<ListResult<EmployeeStoreCalendarManagerViewDto>>
{
    public int EmployeeId { get; set; }
}
public class EmployeeStoreCalendarGetByRegionHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarGetByRegionQuery, ListResult<EmployeeStoreCalendarManagerViewDto>>
{
    private readonly IMediator _mediator;
    public EmployeeStoreCalendarGetByRegionHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<EmployeeStoreCalendarManagerViewDto>> Handle(EmployeeStoreCalendarGetByRegionQuery request, CancellationToken cancellationToken)
    {

        var employeeRegions = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        var currentDate = DateTime.UtcNow.Date;

        var currentPeriod = await _context.EmployeeStoreCalendarPeriods.AsNoTracking()
                                                                       .Where(e => e.StartDate.Date <= currentDate.Date
                                                                            && e.EndDate.Date >= currentDate.Date)
                                                                       .FirstOrDefaultAsync(cancellationToken)
                                                                            ?? throw new Exception("No period found.");

        List<EmployeeStoreCalendarManagerViewDto> employeeList = new List<EmployeeStoreCalendarManagerViewDto>();
        List<int> employeeIds = new List<int>
        {
            request.EmployeeId
        };

        foreach (var regionId in employeeRegions)
        {
            var results = await _context.EmployeeStoreCalendars.AsNoTracking()
                                                   .Include(e => e.Employee)
                                                   .ThenInclude(e => e.EmployeeRegions)
                                                   .Where(e =>
                                                   e.Employee.EmployeeRegions.FirstOrDefault(r => r.EngageRegionId == regionId) != null
                                                   && !employeeIds.Contains(e.EmployeeId)
                                                   && e.EmployeeStoreCalendarPeriodId >= currentPeriod.EmployeeStoreCalendarPeriodId
                                                   && e.Disabled == false)
                                                   .ProjectTo<EmployeeStoreCalendarManagerViewDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

            employeeList.AddRange(results.OrderBy(e => e.EmployeeId).ThenBy(e => e.VisitDate).DistinctBy(e => e.EmployeeId));
            employeeIds.AddRange(results.Select(e => e.EmployeeId).ToList());
        }

        return new ListResult<EmployeeStoreCalendarManagerViewDto>(employeeList.DistinctBy(e => e.EmployeeId).ToList());

    }
}