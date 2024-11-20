// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarPeriods.Queries;

public class EmployeeStoreCalendarPeriodOptionListQuery : IRequest<List<EmployeeStoreCalendarPeriodOption>>
{ 

}

public class EmployeeStoreCalendarPeriodOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarPeriodOptionListQuery, List<EmployeeStoreCalendarPeriodOption>>
{
    public EmployeeStoreCalendarPeriodOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarPeriodOption>> Handle(EmployeeStoreCalendarPeriodOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarPeriods.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeStoreCalendarYearId)
                              .ThenBy(e => e.Number)
                              .ProjectTo<EmployeeStoreCalendarPeriodOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}