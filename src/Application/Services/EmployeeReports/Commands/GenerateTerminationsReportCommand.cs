using Engage.Application.Services.EmployeeReports.Models;

namespace Engage.Application.Services.EmployeeReports.Commands;

public class GenerateTerminationsReportCommand : GetQuery, IRequest<EmployeeReportListVM<EmployeeTerminationsDto>>
{
    public List<int> EngageRegionIds { get; set; }
    public int PayrollPeriodId { get; set; }
}

public class GenerateTerminationsReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateTerminationsReportCommand, EmployeeReportListVM<EmployeeTerminationsDto>>
{
    public GenerateTerminationsReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeReportListVM<EmployeeTerminationsDto>> Handle(GenerateTerminationsReportCommand command, CancellationToken cancellationToken)
    {
        var query = _context.Employees
                            .Where(e => e.Disabled == true
                                && e.EndDate != null
                                && e.EmployeeTerminationReasonId != null
                                && e.PayrollPeriodId.Value == command.PayrollPeriodId
                             );

        if (command.EngageRegionIds?.Any() == true)
        {
            var employeeIds = await _context.EmployeeRegions.Where(e => command.EngageRegionIds.Contains(e.EngageRegionId))
                                                         .Select(e => e.EmployeeId)
                                                         .Distinct()
                                                         .ToListAsync(cancellationToken);

            query = query.Where(e => employeeIds.Contains(e.EmployeeId));
        }

        var data = await query
                            .OrderBy(c => c.FirstName)
                            .ProjectTo<EmployeeTerminationsDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        //We might need to put column headers in an array of strings...
        List<string> colNames = new List<string>();
        colNames.Add("Employee Number*");                         //A
        colNames.Add("Employment Action");                        //B
        colNames.Add("Group join date*");                         //C
        colNames.Add("Employment date*");                         //D
        colNames.Add("Termination date");                         //E
        colNames.Add("Termination Reasion");                      //F
        colNames.Add("Termination Run");                          //G
        colNames.Add("Encash Leave*");                            //H

        return new EmployeeReportListVM<EmployeeTerminationsDto>
        {
            Count = data.Count,
            ReportName = "Employee Terminations " + DateTime.Now.ToString(),
            ColumnNames = colNames,
            Data = data,
        };
    }
}
