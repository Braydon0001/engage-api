using Engage.Application.Services.NotificationEngageRegions.Commands;

namespace Engage.Application.Services.Notifications.Commands;

public class NotificationBirthdayBulkInsertCommand : IRequest<OperationStatus>
{
}

public class NotificationBirthdayBulkInsertHandler : BaseCreateCommandHandler, IRequestHandler<NotificationBirthdayBulkInsertCommand, OperationStatus>
{
    public NotificationBirthdayBulkInsertHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(NotificationBirthdayBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var startDate = DateTime.Now.Date;
        var endDate = DateTime.Now.Date.AddDays(6);

        var employeesWithBirthdays = await _context.Employees.Include(x => x.EmployeeRegions)
                                                                .ThenInclude(x => x.EngageRegion)
                                                            .Where(x => (x.DateOfBirth.Date.AddYears(startDate.Year - x.DateOfBirth.Year)).Date >= startDate
                                                                        && (x.DateOfBirth.Date.AddYears(endDate.Year - x.DateOfBirth.Year)).Date <= endDate
                                                                        && x.Disabled == false
                                                                        )
                                                            .ToListAsync(cancellationToken);

        if (employeesWithBirthdays.Any())
        {
            var regionIds = employeesWithBirthdays.SelectMany(x => x.EmployeeRegions.Select(y => y.EngageRegionId))
                .Distinct()
                .ToList();

            foreach (var regionId in regionIds)
            {
                //Insert a notification with the list of employee names with birthday by region
                var regionEmployees = employeesWithBirthdays.Where(x => x.EmployeeRegions.Select(y => y.EngageRegionId).Contains(regionId))
                                                            .ToList();

                var regionName = regionEmployees.FirstOrDefault()?.EmployeeRegions.Where(x => x.EngageRegionId == regionId).FirstOrDefault()?.EngageRegion.Name;

                var notificationByRegion = new NotificationInsertCommand
                {
                    NotificationTypeId = 2,
                    StartDate = startDate,
                    EndDate = endDate,
                    Title = "Birthdays In " + regionName + " This Week ",
                    Message = "The following employees have birthdays this week: " + string.Join(", ", regionEmployees.Select(x => x.FirstName + " " + x.LastName + " - " + x.DateOfBirth.ToMonthDayString())),
                    NotificationChannelIds = new List<int> { 4 },
                    EngageRegionIds = new List<int> { regionId },
                };

                var regionNotification = await _mediator.Send(notificationByRegion, cancellationToken);

                if (regionNotification.Status)
                {
                    //Target the notification to the region
                    var targetNofication = new NotificationEngageRegionBulkInsertCommand
                    (
                        (int)regionNotification.OperationId,
                        new List<int> { regionId }
                    );

                    await _mediator.Send(targetNofication, cancellationToken);
                }

                foreach (var employee in regionEmployees)
                {
                    //Insert a notification for each employee with a birthday
                    var yearDifference = DateTime.Now.Year - employee.DateOfBirth.Year;
                    var employeeBirthdayNotification = new NotificationInsertCommand
                    {
                        NotificationTypeId = 2,
                        StartDate = employee.DateOfBirth.Date.AddYears(yearDifference).Date,
                        EndDate = endDate.Date,//employee.DateOfBirth.Date.AddYears(yearDifference).Date,
                        Title = "Happy Birthday",
                        Message = "Happy Birthday: " + employee.FirstName + " " + employee.LastName + " - " + employee.DateOfBirth.ToMonthDayString(),
                        NotificationChannelIds = new List<int> { 4 },
                        EngageRegionIds = new List<int> { regionId },
                    };

                    var employeeBirthdayStatus = await _mediator.Send(employeeBirthdayNotification, cancellationToken);
                    if (employeeBirthdayStatus.Status)
                    {
                        //target the notification to the region
                        var targetNofication = new NotificationEngageRegionBulkInsertCommand
                        (
                            (int)employeeBirthdayStatus.OperationId,
                            new List<int> { regionId }
                        );

                        await _mediator.Send(targetNofication, cancellationToken);
                    }
                }
            }
        }

        return new OperationStatus { Status = true };
    }
}
