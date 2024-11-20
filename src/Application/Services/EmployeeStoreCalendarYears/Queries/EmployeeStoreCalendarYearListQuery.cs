// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarYears.Queries;

public class EmployeeStoreCalendarYearListQuery : IRequest<List<EmployeeStoreCalendarYearDto>>
{

}

public class EmployeeStoreCalendarYearListHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarYearListQuery, List<EmployeeStoreCalendarYearDto>>
{
    public EmployeeStoreCalendarYearListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarYearDto>> Handle(EmployeeStoreCalendarYearListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarYears.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeStoreCalendarYearId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<EmployeeStoreCalendarYearDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}