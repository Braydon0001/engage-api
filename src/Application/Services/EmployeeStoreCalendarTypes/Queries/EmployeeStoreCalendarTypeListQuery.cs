// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarTypes.Queries;

public class EmployeeStoreCalendarTypeListQuery : IRequest<List<EmployeeStoreCalendarTypeDto>>
{

}

public class EmployeeStoreCalendarTypeListHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarTypeListQuery, List<EmployeeStoreCalendarTypeDto>>
{
    public EmployeeStoreCalendarTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarTypeDto>> Handle(EmployeeStoreCalendarTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeStoreCalendarTypeId)
                              .ProjectTo<EmployeeStoreCalendarTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}