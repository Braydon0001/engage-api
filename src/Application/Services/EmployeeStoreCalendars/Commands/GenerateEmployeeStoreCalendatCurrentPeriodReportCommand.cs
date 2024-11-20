namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class GenerateEmployeeStoreCalendatCurrentPeriodReportCommand : GetQuery, IRequest<EmployeeStoreCalendarReportVM<EmployeeStoreCalendarCurrentReportDto>>
{
    public int EmployeeId { get; set; }
    public DateTime CurrentDate { get; set; }
}
public class GenerateEmployeeStoreCalendatCurrentPeriodReportHandler : BaseQueryHandler, IRequestHandler<GenerateEmployeeStoreCalendatCurrentPeriodReportCommand, EmployeeStoreCalendarReportVM<EmployeeStoreCalendarCurrentReportDto>>
{
    public GenerateEmployeeStoreCalendatCurrentPeriodReportHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarReportVM<EmployeeStoreCalendarCurrentReportDto>> Handle(GenerateEmployeeStoreCalendatCurrentPeriodReportCommand request, CancellationToken cancellationToken)
    {
        var currentPeriod = await _context.EmployeeStoreCalendarPeriods
            .AsNoTracking()
            .SingleOrDefaultAsync(e =>
            request.CurrentDate >= e.StartDate
            && request.CurrentDate <= e.EndDate
            && e.Disabled == false, cancellationToken);

        if (currentPeriod == null)
        {
            throw new NotFoundException("current period not found", currentPeriod);
        }

        var data = await _context.EmployeeStoreCalendars
            .Where(e => e.EmployeeStoreCalendarPeriodId == currentPeriod.EmployeeStoreCalendarPeriodId
            && e.EmployeeId == request.EmployeeId)
            .AsNoTracking()
            .ProjectTo<EmployeeStoreCalendarCurrentReportDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        List<string> columnNames = new List<string>();
        columnNames.Add("Id");                  //A
        columnNames.Add("Employee Name");       //B
        columnNames.Add("Employee Code");       //C
        columnNames.Add("Store Name");          //D
        columnNames.Add("Store Code");          //E
        columnNames.Add("Date");                //F

        return new EmployeeStoreCalendarReportVM<EmployeeStoreCalendarCurrentReportDto>
        {
            Count = data.Count(),
            ReportName = "Employee Store Calendar Current Period Report " + DateTime.Now.ToShortDateString(),
            ColumnNames = columnNames,
            Data = data
        };
    }
}
public class GenerateEmployeeStoreCalendatCurrentPeriodReportValidator : AbstractValidator<GenerateEmployeeStoreCalendatCurrentPeriodReportCommand>
{
    public GenerateEmployeeStoreCalendatCurrentPeriodReportValidator()
    {
        RuleFor(e => e.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.CurrentDate).NotEmpty();
    }
}