using Engage.Application.Services.Notifications.Models;

namespace Engage.Application.Services.Mobile.Notification.Queries;

public record GetMobileNationalNotificationQuery(int EmployeeId, DateTime Date) : IRequest<ListResult<NotificationListDto>>
{
}

public class GetMobileNationalNotificationHandler : IRequestHandler<GetMobileNationalNotificationQuery, ListResult<NotificationListDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetMobileNationalNotificationHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ListResult<NotificationListDto>> Handle(GetMobileNationalNotificationQuery query, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.Include(e => e.EmployeeJobTitles).ThenInclude(e => e.EmployeeJobTitle)
                                               .Include(e => e.EmployeeRegions).ThenInclude(e => e.EngageRegion)
                                               .SingleOrDefaultAsync(e => e.EmployeeId == query.EmployeeId, cancellationToken);
        if (employee == null)
        {
            return null;
        }

        // a national notification is any notification that does not have regional targeting or has no targeting at all

        var regionTargetedNotificationIds = await _context.NotificationEngageRegions.Where(e => e.EngageRegion.Disabled == false)
                                                                            .Select(e => e.NotificationId)
                                                                            .ToListAsync(cancellationToken);

        var targetedNotificationIds = await _context.NotificationTargets.Select(e => e.NotificationId).ToListAsync(cancellationToken);

        var employeeNotificationIds = await _context.NotificationEmployees.Where(e => e.EmployeeId == query.EmployeeId &&
                                                                                      query.Date >= e.Notification.StartDate &&
                                                                                      (!e.Notification.EndDate.HasValue || query.Date <= e.Notification.EndDate) &&
                                                                                      !regionTargetedNotificationIds.Contains(e.NotificationId))
                                                                          .Select(e => e.NotificationId)
                                                                          .ToListAsync(cancellationToken);

        var jobTitleIds = employee.EmployeeJobTitles.Where(e => e.EmployeeJobTitle.Disabled == false && e.EmployeeJobTitle.Level == 3).Select(e => e.EmployeeJobTitleId).ToList();
        var JobTitleNotificationIds = await _context.NotificationEmployeeJobTitles.Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId) &&
                                                                                              e.EmployeeJobTitle.Disabled == false &&
                                                                                              e.EmployeeJobTitle.Level == 3 &&
                                                                                              !regionTargetedNotificationIds.Contains(e.NotificationId))
                                                                                  .Select(e => e.NotificationId)
                                                                                  .ToListAsync(cancellationToken);

        var nationalNotificationIds = await _context.Notifications.Where(e => e.Disabled == false &&
                                                                              (!regionTargetedNotificationIds.Contains(e.NotificationId)))
                                                                  .Select(e => e.NotificationId)
                                                                  .ToListAsync(cancellationToken);

        var notificationIds = nationalNotificationIds.Where(e => (targetedNotificationIds.Contains(e) && (employeeNotificationIds.Contains(e) || JobTitleNotificationIds.Contains(e))) ||
                                                                 !targetedNotificationIds.Contains(e))
                                                     .Distinct()
                                                     .ToList();

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