using Engage.Application.Services.Notifications.Models;

namespace Engage.Application.Services.Notifications.Queries;

public class NotificationVmQuery : GetByIdQuery, IRequest<NotificationVm>
{
}

public class NotificationVmQueryHandler : BaseQueryHandler, IRequestHandler<NotificationVmQuery, NotificationVm>
{
    public NotificationVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<NotificationVm> Handle(NotificationVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.Include(x => x.NotificationType)
                                                 .Include(x => x.NotificationCategory)
                                                 .Include(x => x.NotificationRegions)
                                                 .ThenInclude(x => x.EngageRegion)
                                                 .Include(x => x.NotificationChannels)
                                                 .ThenInclude(x => x.NotificationChannel)
                                                 .SingleOrDefaultAsync(x => x.NotificationId == request.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<Notification, NotificationVm>(entity);
    }
}
