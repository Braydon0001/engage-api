using Engage.Application.Services.Notifications.Models;

namespace Engage.Application.Services.Mobile.Notification.Queries;

public record GetMobileRegionalNotificationQuery(int EmployeeId, DateTime Date) : IRequest<ListResult<NotificationListDto>>
{
}

public class GetMobileRegionalNotificationHandler : IRequestHandler<GetMobileRegionalNotificationQuery, ListResult<NotificationListDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetMobileRegionalNotificationHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ListResult<NotificationListDto>> Handle(GetMobileRegionalNotificationQuery query, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.Include(e => e.EmployeeJobTitles).ThenInclude(e => e.EmployeeJobTitle)
                                               .Include(e => e.EmployeeRegions).ThenInclude(e => e.EngageRegion)
                                               .SingleOrDefaultAsync(e => e.EmployeeId == query.EmployeeId, cancellationToken);
        if (employee == null)
        {
            return null;
        }

        // a regional notification cannot be untargeted and must have regional targeting.
        // case: if a notification is targeted to a region which is not one of the employee's regions but is still directly targeted to that employee, they should still receive that notification as a regional notification.

        // get all notifications that are targeted by region, considered "regional notifications"
        var regionTargetedNotificationIds = await _context.NotificationEngageRegions.Where(e => e.EngageRegion.Disabled == false)
                                                                               .Select(e => e.NotificationId)
                                                                               .ToListAsync(cancellationToken);

        // get all notifications targeted to this employee
        var employeeNotificationIds = await _context.NotificationEmployees.Where(e => e.EmployeeId == query.EmployeeId && query.Date >= e.Notification.StartDate && (!e.Notification.EndDate.HasValue || query.Date <= e.Notification.EndDate))
                                                                          .Select(e => e.NotificationId)
                                                                          .ToListAsync(cancellationToken);

        // get all notifications targeted to this employee's job title
        var jobTitleIds = employee.EmployeeJobTitles.Where(e => e.EmployeeJobTitle.Disabled == false && e.EmployeeJobTitle.Level == 3).Select(e => e.EmployeeJobTitleId).ToList();
        var JobTitleNotificationIds = await _context.NotificationEmployeeJobTitles.Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId) && e.EmployeeJobTitle.Disabled == false && e.EmployeeJobTitle.Level == 3)
                                                                                  .Select(e => e.NotificationId)
                                                                                  .ToListAsync(cancellationToken);

        // get all notifications targeted to this employee's region
        var regionIds = employee.EmployeeRegions.Where(e => e.EngageRegion.Disabled == false).Select(e => e.EngageRegionId).ToList();
        var regionNotificationIds = await _context.NotificationEngageRegions.Where(e => regionIds.Contains(e.EngageRegionId) && e.EngageRegion.Disabled == false)
                                                                            .Select(e => e.NotificationId)
                                                                            .ToListAsync(cancellationToken);

        var notificationIds = regionTargetedNotificationIds.Where(e => regionNotificationIds.Contains(e) || JobTitleNotificationIds.Contains(e) || employeeNotificationIds.Contains(e)).Distinct().ToList();

        var notifications = await _context.Notifications.Where(e => e.Disabled == false &&
                                                                    (e.NotificationChannels.Any(e => e.NotificationChannelId == 3) ||
                                                                     e.NotificationChannels.Any(e => e.NotificationChannelId == (int)NotificationChannelId.All)) &&
                                                                    query.Date >= e.StartDate && (!e.EndDate.HasValue || query.Date <= e.EndDate) &&
                                                                    notificationIds.Contains(e.NotificationId))
                                                        .ProjectTo<NotificationListDto>(_mapper.ConfigurationProvider)
                                                        .ToListAsync(cancellationToken);

        return new ListResult<NotificationListDto>(notifications);
    }
}