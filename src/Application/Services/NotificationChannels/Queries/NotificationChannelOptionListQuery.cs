namespace Engage.Application.Services.NotificationChannels.Queries;

public class NotificationChannelOptionListQuery : IRequest<List<NotificationChannelOption>>
{

}

public class NotificationChannelOptionListHandler : ListQueryHandler, IRequestHandler<NotificationChannelOptionListQuery, List<NotificationChannelOption>>
{
    public NotificationChannelOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<NotificationChannelOption>> Handle(NotificationChannelOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.NotificationChannels.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Order)
                              .ProjectTo<NotificationChannelOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}