namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarEmployeeReportQuery : IRequest<EmployeeStoreCalendarEmployeeReportData>
{
    public List<int> EmployeeIds { get; set; }
    public int EmployeeStoreCalendarYearId { get; set; }
    public int CalendarMonth { get; set; }
}
public class EmployeeStoreCalendarEmployeeReportHandler : BaseQueryHandler, IRequestHandler<EmployeeStoreCalendarEmployeeReportQuery, EmployeeStoreCalendarEmployeeReportData>
{
    private readonly IUserService _user;
    public EmployeeStoreCalendarEmployeeReportHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<EmployeeStoreCalendarEmployeeReportData> Handle(EmployeeStoreCalendarEmployeeReportQuery query, CancellationToken cancellationToken)
    {
        if (query.EmployeeIds.IsNullOrEmpty())
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == _user.UserName, cancellationToken) ?? throw new Exception("No user found");
            var manager = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == currentUser.UserId, cancellationToken) ?? throw new Exception("No employee found");

            query.EmployeeIds = await _context.Employees
                                .AsNoTracking()
                                .Where(e => e.ManagerId == manager.EmployeeId)
                                .Select(e => e.EmployeeId)
                                .ToListAsync(cancellationToken);
        }

        var employees = await _context.Employees.AsNoTracking().Where(e => query.EmployeeIds.Contains(e.EmployeeId)).ToListAsync(cancellationToken);

        var year = await _context.EmployeeStoreCalendarYears
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(e => e.EmployeeStoreCalendarYearId == query.EmployeeStoreCalendarYearId, cancellationToken: cancellationToken)
                                    ?? throw new Exception("Year not found");

        var firstDayOfMonth = new DateTime(year.StartDate.Value.Year, query.CalendarMonth, 1);

        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

        var entities = await _context.EmployeeStoreCalendars
                                     .AsNoTracking()
                                     .OrderByDescending(e => e.CalendarDate)
                                     .Where(e => query.EmployeeIds.Contains(e.EmployeeId)
                                        && e.Disabled == false
                                        && e.CalendarDate.Date >= firstDayOfMonth.Date
                                        && e.CalendarDate.Date <= lastDayOfMonth.Date)
                                     .ProjectTo<EmployeeStoreCalendarEmployeeReportDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);

        employees = employees.Where(e => !entities.Select(e => e.EmployeeCode).ToList().Contains(e.Code)).ToList();

        var groupedEntities = entities.GroupBy(e => e.EmployeeName).ToDictionary(e => e.Key, x => x.ToList());

        foreach (var employee in employees)
        {
            groupedEntities.Add($"{employee.FirstName} {employee.LastName}", []);
        }

        List<string> headings =
        [
            "Id",
            "Employee Name",
            "Employee Code",
            "Store Name",
            "Calendar Date",
            "Status"
        ];

        return new(headings, groupedEntities, $"Store Visits - {firstDayOfMonth.Year}-{firstDayOfMonth:MMM}");
    }
}
public class EmployeeStoreCalendarEmployeeReportValidator : AbstractValidator<EmployeeStoreCalendarEmployeeReportQuery>
{
    public EmployeeStoreCalendarEmployeeReportValidator()
    {
        RuleFor(e => e.EmployeeIds).NotEmpty();
        RuleFor(e => e.EmployeeStoreCalendarYearId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CalendarMonth).NotEmpty().GreaterThan(0).LessThan(13);
    }
}