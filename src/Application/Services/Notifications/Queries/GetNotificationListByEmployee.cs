using Engage.Application.Services.Notifications.Models;

namespace Engage.Application.Services.Notifications.Queries;

public class GetNotificationListByEmployeeQuery : GetQuery, IRequest<ListResult<NotificationListDto>>
{
    public int EmployeeId { get; set; }
    public DateTime? TimezoneDate { get; set; }
}

public class GetNotificationListByEmployeeQueryHandler : BaseQueryHandler, IRequestHandler<GetNotificationListByEmployeeQuery, ListResult<NotificationListDto>>
{
    public GetNotificationListByEmployeeQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<NotificationListDto>> Handle(GetNotificationListByEmployeeQuery request, CancellationToken cancellationToken)
    {
        DateTime? timezoneDate = request.TimezoneDate ?? null;

        var notificationIds = await _context.NotificationRegions
            .Join(_context.EmployeeRegions.Where(e => e.EmployeeId == request.EmployeeId),
                  notificationRegion => notificationRegion.EngageRegionId,
                  employeeRegion => employeeRegion.EngageRegionId,
                  (notificationRegion, employeeRegion) => notificationRegion.NotificationId)
            .Distinct()
            .ToListAsync(cancellationToken);

        if (notificationIds.Count == 0)
        {
            return new ListResult<NotificationListDto>
            {
                Data = new List<NotificationListDto>(),
                Count = 0
            };
        }

        var entities = await _context.Notifications
            .Where(e => notificationIds.Contains(e.NotificationId))
            .Where(e => timezoneDate == null || timezoneDate.Value >= e.StartDate && timezoneDate.Value <= e.EndDate)
            .Include(e => e.NotificationRegions)
                .ThenInclude(e => e.EngageRegion)
            .OrderByDescending(d => d.StartDate)
            .ProjectTo<NotificationListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ListResult<NotificationListDto>
        {
            Data = entities,
            Count = entities.Count
        };
    }
}
