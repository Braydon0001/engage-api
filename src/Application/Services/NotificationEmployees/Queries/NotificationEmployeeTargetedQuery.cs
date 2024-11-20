using Engage.Application.Services.Notifications.Models;

namespace Engage.Application.Services.NotificationEmployees.Queries;

public record NotificationEmployeeTargetedQuery(int EmployeeId, int ChannelId, DateTime Date) : IRequest<ListResult<NotificationDto>>
{
}

public class NotificationEmployeeTargetedHandler : IRequestHandler<NotificationEmployeeTargetedQuery, ListResult<NotificationDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public NotificationEmployeeTargetedHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ListResult<NotificationDto>> Handle(NotificationEmployeeTargetedQuery query, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.Include(e => e.EmployeeJobTitles).ThenInclude(e => e.EmployeeJobTitle)
                                               .Include(e => e.EmployeeRegions).ThenInclude(e => e.EngageRegion)
                                               .SingleOrDefaultAsync(e => e.EmployeeId == query.EmployeeId, cancellationToken);
        if (employee == null)
        {
            return null;
        }

        var targetedNotifications = await _context.NotificationTargets.Select(e => e.NotificationId).ToListAsync(cancellationToken);

        var employeeNotificationIds = await _context.NotificationEmployees.Where(e => e.EmployeeId == query.EmployeeId && query.Date.Date >= e.Notification.StartDate.Date && (!e.Notification.EndDate.HasValue || query.Date.Date <= e.Notification.EndDate.Value.Date))
                                                                          .Select(e => e.NotificationId)
                                                                          .ToListAsync(cancellationToken);

        var jobTitleIds = employee.EmployeeJobTitles.Where(e => e.EmployeeJobTitle.Disabled == false && e.EmployeeJobTitle.Level == 3).Select(e => e.EmployeeJobTitleId).ToList();
        var JobTitleNotificationIds = await _context.NotificationEmployeeJobTitles.Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId) && e.EmployeeJobTitle.Disabled == false && e.EmployeeJobTitle.Level == 3)
                                                                                  .Select(e => e.NotificationId)
                                                                                  .ToListAsync(cancellationToken);

        var regionIds = employee.EmployeeRegions.Where(e => e.EngageRegion.Disabled == false).Select(e => e.EngageRegionId).ToList();
        var regionNotificationIds = await _context.NotificationEngageRegions.Where(e => regionIds.Contains(e.EngageRegionId) && e.EngageRegion.Disabled == false)
                                                                            .Select(e => e.NotificationId)
                                                                            .ToListAsync(cancellationToken);

        var notificationIds = employeeNotificationIds.Union(JobTitleNotificationIds).Union(regionNotificationIds).Distinct().ToList();

        var notifications = await _context.Notifications.Where(e => e.Disabled == false &&
                                                                    (e.NotificationChannels.Any(e => e.NotificationChannelId == query.ChannelId) ||
                                                                     e.NotificationChannels.Any(e => e.NotificationChannelId == (int)NotificationChannelId.All)) &&
                                                                    query.Date.Date >= e.StartDate.Date && (!e.EndDate.HasValue || query.Date.Date <= e.EndDate.Value.Date) &&
                                                                    ((targetedNotifications.Contains(e.NotificationId) && notificationIds.Contains(e.NotificationId)) || !targetedNotifications.Contains(e.NotificationId)))
                                                        .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
                                                        .ToListAsync(cancellationToken);

        return new ListResult<NotificationDto>(notifications);
    }
}