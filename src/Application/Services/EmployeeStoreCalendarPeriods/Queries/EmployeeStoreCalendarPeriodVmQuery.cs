// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarPeriods.Queries;

public class EmployeeStoreCalendarPeriodVmQuery : IRequest<EmployeeStoreCalendarPeriodVm>
{
    public int Id { get; set; }
}

public class EmployeeStoreCalendarPeriodVmHandler : VmQueryHandler, IRequestHandler<EmployeeStoreCalendarPeriodVmQuery, EmployeeStoreCalendarPeriodVm>
{
    public EmployeeStoreCalendarPeriodVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarPeriodVm> Handle(EmployeeStoreCalendarPeriodVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarPeriods.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.EmployeeStoreCalendarYear);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarPeriodId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeStoreCalendarPeriodVm>(entity);
    }
}