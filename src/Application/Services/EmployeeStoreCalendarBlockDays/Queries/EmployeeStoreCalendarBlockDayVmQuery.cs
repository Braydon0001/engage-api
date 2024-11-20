// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarBlockDays.Queries;

public class EmployeeStoreCalendarBlockDayVmQuery : IRequest<EmployeeStoreCalendarBlockDayVm>
{
    public int Id { get; set; }
}

public class EmployeeStoreCalendarBlockDayVmHandler : VmQueryHandler, IRequestHandler<EmployeeStoreCalendarBlockDayVmQuery, EmployeeStoreCalendarBlockDayVm>
{
    public EmployeeStoreCalendarBlockDayVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarBlockDayVm> Handle(EmployeeStoreCalendarBlockDayVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarBlockDays.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Employee)
                             .Include(e => e.EmployeeStoreCalendarType)
                             .Include(e => e.EmployeeStoreCalendarStatus)
                             .Include(e => e.EmployeeStoreCalendarPeriod);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarBlockDayId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeStoreCalendarBlockDayVm>(entity);
    }
}