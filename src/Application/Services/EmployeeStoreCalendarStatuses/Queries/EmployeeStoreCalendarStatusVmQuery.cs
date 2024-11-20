// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarStatuses.Queries;

public class EmployeeStoreCalendarStatusVmQuery : IRequest<EmployeeStoreCalendarStatusVm>
{
    public int Id { get; set; }
}

public class EmployeeStoreCalendarStatusVmHandler : VmQueryHandler, IRequestHandler<EmployeeStoreCalendarStatusVmQuery, EmployeeStoreCalendarStatusVm>
{
    public EmployeeStoreCalendarStatusVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarStatusVm> Handle(EmployeeStoreCalendarStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarStatusId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeStoreCalendarStatusVm>(entity);
    }
}