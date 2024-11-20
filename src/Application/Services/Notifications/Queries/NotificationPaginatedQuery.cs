using Engage.Application.Services.Notifications.Models;

namespace Engage.Application.Services.Notifications.Queries;

public class NotificationPaginatedQuery : PaginatedQuery, IRequest<ListResult<NotificationListDto>>
{
}

public record NotificationPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<NotificationPaginatedQuery, ListResult<NotificationListDto>>
{
    public async Task<ListResult<NotificationListDto>> Handle(NotificationPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = NotificationPaginationProps.Create();

        var queryable = Context.Notifications.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.NotificationId);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<NotificationListDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}


