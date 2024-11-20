// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarGroups.Queries;

public class EmployeeStoreCalendarGroupListQuery : IRequest<List<EmployeeStoreCalendarGroupDto>>
{

}

public class EmployeeStoreCalendarGroupListHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarGroupListQuery, List<EmployeeStoreCalendarGroupDto>>
{
    public EmployeeStoreCalendarGroupListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarGroupDto>> Handle(EmployeeStoreCalendarGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeStoreCalendarGroupId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<EmployeeStoreCalendarGroupDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}