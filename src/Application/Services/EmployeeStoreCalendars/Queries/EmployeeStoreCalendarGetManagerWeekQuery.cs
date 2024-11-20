using Engage.Application.Services.EmployeeStoreCalendarBlockDays.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGetManagerWeekQuery : GetQuery, IRequest<ListResult<EmployeeStoreCalendarWeekDto>>
{
    public int EmployeeId { get; set; }
    public DateTime CalendarDate { get; set; }
    public int? JobTitleId { get; set; }
}
public class EmployeeStoreCalendarGetManagerWeekHandler : BaseQueryHandler, IRequestHandler<EmployeeStoreCalendarGetManagerWeekQuery, ListResult<EmployeeStoreCalendarWeekDto>>
{
    public EmployeeStoreCalendarGetManagerWeekHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeStoreCalendarWeekDto>> Handle(EmployeeStoreCalendarGetManagerWeekQuery request, CancellationToken cancellationToken)
    {
        var pastDate = request.CalendarDate.StartOfWeek();
        var futureDate = request.CalendarDate.EndOfWeek().AddDays(1);

        List<int> employeeIds = [];

        var queryableEntities = _context.EmployeeStoreCalendars
            .AsNoTracking()
            .OrderByDescending(e => e.CalendarDate)
            .AsQueryable();

        if (request.JobTitleId != null)
        {
            employeeIds = await _context.Employees.Where(e => e.ManagerId == request.EmployeeId
                                                            && e.EmployeeJobTitles.Any(f => f.EmployeeJobTitleId == request.JobTitleId))
                                                      .Select(e => e.EmployeeId)
                                                      .ToListAsync(cancellationToken);

            queryableEntities = queryableEntities.Where(e => employeeIds.Contains(e.EmployeeId)
                                                            && e.CalendarDate >= pastDate
                                                            && e.CalendarDate <= futureDate
                                                            && e.Disabled == false);
        }
        else
        {
            queryableEntities = queryableEntities.Where(e => e.Employee.ManagerId == request.EmployeeId
                && e.CalendarDate >= pastDate
                && e.CalendarDate <= futureDate
                && e.Disabled == false);

        }

        var entities = await queryableEntities.Include(e => e.SurveyFormSubmissions)
            .ThenInclude(e => e.SurveyFormSubmission)
            .ProjectTo<EmployeeStoreCalendarWeekDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);


        //get blocked off days for employee in current period
        var blockedDaysQueryable = _context.EmployeeStoreCalendarBlockDays.AsQueryable().AsNoTracking().Where(e => e.Disabled == false);

        if (request.JobTitleId != null)
        {
            blockedDaysQueryable = blockedDaysQueryable.Where(e => employeeIds.Contains(e.EmployeeId)
                                                        && e.CalendarDate >= pastDate && e.CalendarDate <= futureDate
                                                        && e.EmployeeStoreCalendarTypeId == (int)EmployeeStoreCalendarTypeId.Survey);
        }
        else
        {
            blockedDaysQueryable = blockedDaysQueryable.Where(e => e.Employee.ManagerId == request.EmployeeId
                                && e.CalendarDate >= pastDate && e.CalendarDate <= futureDate);
        }

        var blockedDays = await blockedDaysQueryable
                            .ProjectTo<EmployeeStoreCalendarBlockDayDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        foreach (var blockedDay in blockedDays)
        {
            entities.Add(new EmployeeStoreCalendarWeekDto
            {
                Id = blockedDay.Id,
                EmployeeId = blockedDay.EmployeeId,
                EmployeeName = blockedDay.EmployeeName,
                EmployeeCode = blockedDay.EmployeeCode,
                CalendarDate = blockedDay.CalendarDate,
                EmployeeStoreCalendarPeriodId = blockedDay.EmployeeStoreCalendarPeriodId,
                EmployeeStoreCalendarPeriodName = blockedDay.EmployeeStoreCalendarPeriodName,
                EmployeeStoreCalendarTypeId = blockedDay.EmployeeStoreCalendarTypeId,
                IsManagerCreated = blockedDay.IsManagerCreated,
                BlockDay = true,
                Note = blockedDay.Note,
                AppointmentDate = blockedDay.AppointmentDate
            });
        }

        List<EmployeeStoreCalendarWeekDto> datedValues = new();

        var appointmentDates = entities.DistinctBy(e => e.AppointmentDate).Select(e => e.AppointmentDate).ToList();

        foreach (var appointmentDate in appointmentDates)
        {
            var appointments = entities.Where(e => e.AppointmentDate == appointmentDate).OrderBy(e => e.Id).ToList();
            int index = 0;
            foreach (var appointment in appointments)
            {
                var buffer = appointment;
                buffer.StartTime = index < 10 ? $"0{index}:00" : $"{index}0:00";
                buffer.EndTime = index < 10 ? $"0{index + 1}:00" : $"{index + 1}0:00";
                datedValues.Add(buffer);
                index++;
            }

        }

        return new ListResult<EmployeeStoreCalendarWeekDto>
        {
            Count = datedValues.Count(),
            Data = datedValues
        };
    }
}