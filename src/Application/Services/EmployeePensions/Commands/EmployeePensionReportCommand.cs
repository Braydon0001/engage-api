using Engage.Application.Services.ClaimReports.Models;
using Engage.Application.Services.EmployeePensions.Queries;

namespace Engage.Application.Services.EmployeePensions.Commands;

public record EmployeePensionReportCommand(List<int> EngageRegionIds, DateTime? StartDate, DateTime? EndDate) : IRequest<ReportListVM<EmployeePensionReportDto>>;

public record GenerateEmployeePensionReportCommandHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeePensionReportCommand, ReportListVM<EmployeePensionReportDto>>
{

    public async Task<ReportListVM<EmployeePensionReportDto>> Handle(EmployeePensionReportCommand command, CancellationToken cancellationToken)
    {
        var pensionQuery = Context.EmployeePensions.AsQueryable();
        var employeesQuery = Context.Employees.AsQueryable();

        if (command.EngageRegionIds != null && command.EngageRegionIds.Any())
        {
            var employeeIds = await Context.EmployeeRegions
                                                .Where(e => command.EngageRegionIds.Contains(e.EngageRegionId))
                                                .Select(x => x.EmployeeId)
                                                .ToListAsync(cancellationToken);

            employeeIds = employeeIds.Distinct().ToList();

            pensionQuery = pensionQuery.Where(x => employeeIds.Contains(x.EmployeeId));
            employeesQuery = employeesQuery.Where(x => employeeIds.Contains(x.EmployeeId));
        }

        if (command.StartDate.HasValue)
        {
            pensionQuery = pensionQuery.Where(p => p.Created.Date >= command.StartDate.Value.Date);
        }

        if (command.EndDate.HasValue)
        {
            pensionQuery = pensionQuery.Where(p => p.Created.Date <= command.EndDate.Value.Date);
        }

        var employeeIdsWithPension = await Context.EmployeePensions.Select(x => x.EmployeeId)
                                                                   .ToListAsync(cancellationToken);

        if (employeeIdsWithPension.Any())
        {
            employeesQuery = employeesQuery.Where(e => !employeeIdsWithPension.Contains(e.EmployeeId));
        }

        List<EmployeePensionReportDto> data = new();

        var pensionList = await pensionQuery.ProjectTo<EmployeePensionReportDto>(Mapper.ConfigurationProvider)
                                            .OrderBy(e => e.EmployeeName)
                                            .ToListAsync(cancellationToken);

        var employeeList = await employeesQuery.ProjectTo<EmployeePensionReportDto>(Mapper.ConfigurationProvider)
                                               .OrderBy(e => e.EmployeeName)
                                               .ToListAsync(cancellationToken);

        if (pensionList.Any())
        {
            data.AddRange(pensionList);
        }

        if (employeeList.Any())
        {
            data.AddRange(employeeList);
        }

        string reportName = "Employee Pension Report - " + DateTime.Now.ToString();

        return new ReportListVM<EmployeePensionReportDto>
        {
            Count = data.Count,
            ReportName = reportName,
            Data = data,
        };
    }
}