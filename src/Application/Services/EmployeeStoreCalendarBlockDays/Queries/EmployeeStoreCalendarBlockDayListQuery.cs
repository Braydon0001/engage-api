// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarBlockDays.Queries;

public class EmployeeStoreCalendarBlockDayListQuery : IRequest<List<EmployeeStoreCalendarBlockDayDto>>
{

}

public class EmployeeStoreCalendarBlockDayListHandler : ListQueryHandler, IRequestHandler<EmployeeStoreCalendarBlockDayListQuery, List<EmployeeStoreCalendarBlockDayDto>>
{
    public EmployeeStoreCalendarBlockDayListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeStoreCalendarBlockDayDto>> Handle(EmployeeStoreCalendarBlockDayListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendarBlockDays.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CalendarDate)
                              .ProjectTo<EmployeeStoreCalendarBlockDayDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}