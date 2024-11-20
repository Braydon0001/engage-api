using Engage.Application.Services.EmployeeStoreCalendarBlockDays.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGetByEmployeeDateQuery : GetQuery, IRequest<ListResult<EmployeeStoreCalendarMonthDto>>
{
    public int EmployeeId { get; set; }
    public DateTime CalendarDate { get; set; }
}
public class EmployeeStoreCalendarGetByEmployeePeriodQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeStoreCalendarGetByEmployeeDateQuery, ListResult<EmployeeStoreCalendarMonthDto>>
{
    public EmployeeStoreCalendarGetByEmployeePeriodQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeStoreCalendarMonthDto>> Handle(EmployeeStoreCalendarGetByEmployeeDateQuery request, CancellationToken cancellationToken)
    {
        var pastDate = request.CalendarDate.AddDays(-28);
        var futureDate = request.CalendarDate.AddDays(35);

        var queryable = _context.EmployeeStoreCalendars.AsQueryable().Where(e => e.Disabled == false);
        var entities = await queryable
            .AsNoTracking()
            .Where(e => e.EmployeeId == request.EmployeeId
            && e.CalendarDate >= pastDate && e.CalendarDate <= futureDate)
            .Include(e => e.SurveyFormSubmissions)
            .ThenInclude(e => e.SurveyFormSubmission)
            .ProjectTo<EmployeeStoreCalendarMonthDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        //get blocked off days for employee in current period
        var blockedDaysQueryable = _context.EmployeeStoreCalendarBlockDays.AsQueryable().Where(e => e.Disabled == false);
        var blockedDays = await blockedDaysQueryable
                            .AsNoTracking()
                            .Where(e => e.EmployeeId == request.EmployeeId
                                && e.CalendarDate >= pastDate && e.CalendarDate <= futureDate)
                            .ProjectTo<EmployeeStoreCalendarBlockDayDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        foreach (var blockedDay in blockedDays)
        {
            entities.Add(new EmployeeStoreCalendarMonthDto
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
            });
        }


        return new ListResult<EmployeeStoreCalendarMonthDto>
        {
            Count = entities.Count(),
            Data = entities
        };
    }
}