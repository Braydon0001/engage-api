using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarPaginatedQuery : PaginatedQuery, IRequest<ListResult<EmployeeStoreCalendarDto>>
{
}

public record EmployeeStoreCalendarPaginatedHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<EmployeeStoreCalendarPaginatedQuery, ListResult<EmployeeStoreCalendarDto>>
{
    public async Task<ListResult<EmployeeStoreCalendarDto>> Handle(EmployeeStoreCalendarPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = EmployeeStoreCalendarPaginationProps.Create();

        var engageRegionIds = await Mediator.Send(new UserRegionsQuery(), cancellationToken);

        var queryable = Context.EmployeeStoreCalendars.AsQueryable().AsNoTracking();

        if (!engageRegionIds.Contains(7))
        {
            queryable = queryable
                                .Join(Context.EmployeeRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                      employee => employee.EmployeeId,
                                      region => region.EmployeeId,
                                      (employee, region) => employee).Distinct();
        }

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.EmployeeStoreCalendarId);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<EmployeeStoreCalendarDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}