// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarGroups.Queries;

public class EmployeeStoreCalendarGroupOptionListQuery : IRequest<List<EmployeeStoreCalendarGroupOption>>
{ 

}

public class EmployeeStoreCalendarGroupOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarGroupOptionListQuery, List<EmployeeStoreCalendarGroupOption>>
{
    public EmployeeStoreCalendarGroupOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarGroupOption>> Handle(EmployeeStoreCalendarGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeStoreCalendarGroupId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<EmployeeStoreCalendarGroupOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}