using Engage.Application.Services.Notifications.Models;

namespace Engage.Application.Services.Notifications.Queries;

public class GetRegionalNotificationsQuery : GetQuery, IRequest<ListResult<NotificationListDto>>
{
    public DateTime TimezoneDate { get; set; }
    public int EmployeeId { get; set; }
}

public class GetRegionalNotificationsQueryHandler : BaseQueryHandler, IRequestHandler<GetRegionalNotificationsQuery, ListResult<NotificationListDto>>
{
    public GetRegionalNotificationsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<NotificationListDto>> Handle(GetRegionalNotificationsQuery query, CancellationToken cancellationToken)
    {
        // A notification is regional if it has a region.
        var regionalNotifications = _context.Notifications.Where(e => query.TimezoneDate >= e.StartDate &&
                                                                      (query.TimezoneDate <= e.EndDate || !e.EndDate.HasValue) &&
                                                                      e.NotificationRegions.Any() && !e.Disabled);

        // The notification is targeted to specific employees, including this EmployeeId.
        var employeeNotifications = await regionalNotifications.Join(_context.NotificationEmployees.Where(e => e.EmployeeId == query.EmployeeId),
                                                                     notification => notification.NotificationId,
                                                                     notificationEmployee => notificationEmployee.NotificationId,
                                                                     (notification, notificationEmployee) => notification)
                                                               .Include(e => e.NotificationRegions)
                                                               .ThenInclude(e => e.EngageRegion)
                                                               .ProjectTo<NotificationListDto>(_mapper.ConfigurationProvider)
                                                               .ToListAsync(cancellationToken);

        // The notification is targeted to no employees i.e. It is targeted to all employees.
        var nonEmployeeNotifications = await regionalNotifications.Where(e => !e.NotificationEmployees.Any())
                                                                  .Include(e => e.NotificationRegions)
                                                                  .ThenInclude(e => e.EngageRegion)
                                                                  .ProjectTo<NotificationListDto>(_mapper.ConfigurationProvider)
                                                                  .ToListAsync(cancellationToken);

        var notifications = employeeNotifications.Concat(nonEmployeeNotifications)
                                                 .OrderByDescending(e => e.StartDate)
                                                 .ToList();

        return new ListResult<NotificationListDto>
        {
            Data = notifications,
            Count = notifications.Count
        };
    }
}
