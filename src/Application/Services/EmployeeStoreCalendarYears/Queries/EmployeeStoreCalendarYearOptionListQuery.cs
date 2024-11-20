// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarYears.Queries;

public class EmployeeStoreCalendarYearOptionListQuery : IRequest<List<EmployeeStoreCalendarYearOption>>
{ 

}

public class EmployeeStoreCalendarYearOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarYearOptionListQuery, List<EmployeeStoreCalendarYearOption>>
{
    public EmployeeStoreCalendarYearOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarYearOption>> Handle(EmployeeStoreCalendarYearOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarYears.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeStoreCalendarYearId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<EmployeeStoreCalendarYearOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}