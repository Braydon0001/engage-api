// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarYears.Queries;

public class EmployeeStoreCalendarYearVmQuery : IRequest<EmployeeStoreCalendarYearVm>
{
    public int Id { get; set; }
}

public class EmployeeStoreCalendarYearVmHandler : VmQueryHandler, IRequestHandler<EmployeeStoreCalendarYearVmQuery, EmployeeStoreCalendarYearVm>
{
    public EmployeeStoreCalendarYearVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarYearVm> Handle(EmployeeStoreCalendarYearVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarYears.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarYearId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeStoreCalendarYearVm>(entity);
    }
}