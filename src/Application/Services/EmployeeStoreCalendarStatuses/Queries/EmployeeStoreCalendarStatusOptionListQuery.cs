// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarStatuses.Queries;

public class EmployeeStoreCalendarStatusOptionListQuery : IRequest<List<EmployeeStoreCalendarStatusOption>>
{ 

}

public class EmployeeStoreCalendarStatusOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarStatusOptionListQuery, List<EmployeeStoreCalendarStatusOption>>
{
    public EmployeeStoreCalendarStatusOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarStatusOption>> Handle(EmployeeStoreCalendarStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeStoreCalendarStatusId)
                              .ProjectTo<EmployeeStoreCalendarStatusOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}