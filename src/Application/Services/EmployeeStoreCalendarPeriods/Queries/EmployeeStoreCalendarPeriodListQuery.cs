// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarPeriods.Queries;

public class EmployeeStoreCalendarPeriodListQuery : IRequest<List<EmployeeStoreCalendarPeriodDto>>
{
    public int? EmployeeStoreCalendarYearId { get; set; }
}

public class EmployeeStoreCalendarPeriodListHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarPeriodListQuery, List<EmployeeStoreCalendarPeriodDto>>
{
    public EmployeeStoreCalendarPeriodListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarPeriodDto>> Handle(EmployeeStoreCalendarPeriodListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarPeriods.AsQueryable().AsNoTracking();

        if (query.EmployeeStoreCalendarYearId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeStoreCalendarYearId == query.EmployeeStoreCalendarYearId);
        }

        return await queryable.OrderBy(e => e.EmployeeStoreCalendarYearId)
                              .ThenBy(e => e.Number)
                              .ProjectTo<EmployeeStoreCalendarPeriodDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}