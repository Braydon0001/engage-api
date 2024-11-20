// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarTypes.Queries;

public class EmployeeStoreCalendarTypeVmQuery : IRequest<EmployeeStoreCalendarTypeVm>
{
    public int Id { get; set; }
}

public class EmployeeStoreCalendarTypeVmHandler : VmQueryHandler, IRequestHandler<EmployeeStoreCalendarTypeVmQuery, EmployeeStoreCalendarTypeVm>
{
    public EmployeeStoreCalendarTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarTypeVm> Handle(EmployeeStoreCalendarTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeStoreCalendarTypeVm>(entity);
    }
}