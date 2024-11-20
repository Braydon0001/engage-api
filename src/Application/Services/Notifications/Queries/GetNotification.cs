using Engage.Application.Services.Notifications.Models;

namespace Engage.Application.Services.Notifications.Queries;

public class GetNotificationQuery : GetByIdQuery, IRequest<NotificationDto>
{
}

public class GetNotificationQueryHandler : BaseQueryHandler, IRequestHandler<GetNotificationQuery, NotificationDto>
{

    public GetNotificationQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<NotificationDto> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.Include(x => x.NotificationRegions)
                                                 .ThenInclude(x => x.EngageRegion)
                                                 .Include(x => x.NotificationChannels)
                                                 .ThenInclude(x => x.NotificationChannel)
                                                 .SingleAsync(x => x.NotificationId == request.Id, cancellationToken);

        return _mapper.Map<Notification, NotificationDto>(entity);
    }
}
