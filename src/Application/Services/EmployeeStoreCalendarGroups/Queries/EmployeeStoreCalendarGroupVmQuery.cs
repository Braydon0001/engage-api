// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarGroups.Queries;

public class EmployeeStoreCalendarGroupVmQuery : IRequest<EmployeeStoreCalendarGroupVm>
{
    public int Id { get; set; }
}

public class EmployeeStoreCalendarGroupVmHandler : VmQueryHandler, IRequestHandler<EmployeeStoreCalendarGroupVmQuery, EmployeeStoreCalendarGroupVm>
{
    public EmployeeStoreCalendarGroupVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarGroupVm> Handle(EmployeeStoreCalendarGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarGroups.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarGroupId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeStoreCalendarGroupVm>(entity);
    }
}