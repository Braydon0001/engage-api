namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class GenerateEmployeeStoreCalendarPreviousPeriodReportCommand : GetQuery, IRequest<EmployeeStoreCalendarReportVM<EmployeeStoreCalendarPreviousPeriodReportDto>>
{
    public DateTime Date { get; set; }
    public int EmployeeId { get; set; }
}
public class GenerateEmployeeStoreCalendarPreviousPeriodHandler : BaseQueryHandler, IRequestHandler<GenerateEmployeeStoreCalendarPreviousPeriodReportCommand, EmployeeStoreCalendarReportVM<EmployeeStoreCalendarPreviousPeriodReportDto>>
{
    public GenerateEmployeeStoreCalendarPreviousPeriodHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    { }

    public async Task<EmployeeStoreCalendarReportVM<EmployeeStoreCalendarPreviousPeriodReportDto>> Handle(GenerateEmployeeStoreCalendarPreviousPeriodReportCommand request, CancellationToken cancellationToken)
    {
        var previousPeriod = await _context.EmployeeStoreCalendarPeriods
            .AsNoTracking()
            .SingleOrDefaultAsync(e =>
            request.Date >= e.StartDate
            && request.Date <= e.EndDate
            && e.Disabled == false, cancellationToken);
        if (previousPeriod == null)
        {
            throw new NotFoundException("period not found", previousPeriod);
        }

        var data = await _context.EmployeeStoreCalendars
            .Where(e => e.EmployeeStoreCalendarPeriodId == previousPeriod.EmployeeStoreCalendarPeriodId
                        && e.EmployeeId == request.EmployeeId)
            .AsNoTracking()
            .ProjectTo<EmployeeStoreCalendarPreviousPeriodReportDto>(_mapper.ConfigurationProvider)
            .OrderBy(e => e.IsCompleted)
            .ToListAsync(cancellationToken);

        List<string> columnNames = new List<string>();
        columnNames.Add("Id");                  //A
        columnNames.Add("Employee Name");       //B
        columnNames.Add("Employee Code");       //C
        columnNames.Add("Store Name");          //D
        columnNames.Add("Store Code");          //E
        columnNames.Add("Date");                //F
        columnNames.Add("Report Completed");    //I

        return new EmployeeStoreCalendarReportVM<EmployeeStoreCalendarPreviousPeriodReportDto>
        {
            Count = data.Count(),
            ReportName = "Employee Store Calendar Previous Period Report " + DateTime.Now.ToShortDateString(),
            ColumnNames = columnNames,
            Data = data
        };
    }
}
