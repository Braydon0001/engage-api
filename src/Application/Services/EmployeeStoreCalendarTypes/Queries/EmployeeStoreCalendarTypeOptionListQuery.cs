// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarTypes.Queries;

public class EmployeeStoreCalendarTypeOptionListQuery : IRequest<List<EmployeeStoreCalendarTypeOption>>
{ 

}

public class EmployeeStoreCalendarTypeOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarTypeOptionListQuery, List<EmployeeStoreCalendarTypeOption>>
{
    public EmployeeStoreCalendarTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarTypeOption>> Handle(EmployeeStoreCalendarTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeStoreCalendarTypeId)
                              .ProjectTo<EmployeeStoreCalendarTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}