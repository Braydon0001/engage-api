// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarStatuses.Queries;

public class EmployeeStoreCalendarStatusListQuery : IRequest<List<EmployeeStoreCalendarStatusDto>>
{

}

public class EmployeeStoreCalendarStatusListHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarStatusListQuery, List<EmployeeStoreCalendarStatusDto>>
{
    public EmployeeStoreCalendarStatusListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarStatusDto>> Handle(EmployeeStoreCalendarStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeStoreCalendarStatusId)
                              .ProjectTo<EmployeeStoreCalendarStatusDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}