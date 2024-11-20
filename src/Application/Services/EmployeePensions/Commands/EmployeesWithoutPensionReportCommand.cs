using Engage.Application.Services.ClaimReports.Models;
using Engage.Application.Services.EmployeePensions.Queries;

namespace Engage.Application.Services.EmployeePensions.Commands;

public class EmployeesWithoutPensionReportCommand : GetQuery, IRequest<ReportListVM<EmployeesWithoutPensionReportDto>>
{
    public List<int> EngageRegionIds { get; set; }
}

public class GenerateEmployeesWithoutPensionReportCommandHandler : BaseQueryHandler, IRequestHandler<EmployeesWithoutPensionReportCommand, ReportListVM<EmployeesWithoutPensionReportDto>>
{
    public GenerateEmployeesWithoutPensionReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ReportListVM<EmployeesWithoutPensionReportDto>> Handle(EmployeesWithoutPensionReportCommand command, CancellationToken cancellationToken)
    {
        var employeeIdsWithPension = await _context.EmployeePensions
                                                        .Select(x => x.EmployeeId)
                                                        .ToListAsync(cancellationToken);
        var query = _context.Employees.Where(e => e.Disabled == false).AsQueryable();

        if (command.EngageRegionIds != null && command.EngageRegionIds.Any())
        {
            var employeeIds = await _context.EmployeeRegions
                                                .Where(e => command.EngageRegionIds.Contains(e.EngageRegionId))
                                                .Select(x => x.EmployeeId)
                                                .ToListAsync(cancellationToken);

            employeeIds = employeeIds.Distinct().ToList();

            query = query.Where(x => employeeIds.Contains(x.EmployeeId));
        }

        if (employeeIdsWithPension.Any())
        {
            query = query.Where(e => !employeeIdsWithPension.Contains(e.EmployeeId));
        }

        var data = await query.ProjectTo<EmployeesWithoutPensionReportDto>(_mapper.ConfigurationProvider)
                              .OrderBy(e => e.EmployeeName)
                              .ToListAsync();

        string reportName = "Employee Pension Report - " + DateTime.Now.ToString();

        return new ReportListVM<EmployeesWithoutPensionReportDto>
        {
            Count = data.Count,
            ReportName = reportName,
            Data = data,
        };
    }
}