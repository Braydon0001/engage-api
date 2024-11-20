using Engage.Application.Services.EmployeeReports.Models;
using Engage.Application.Services.EmployeeTransactionReports.Commands;
using Engage.Application.Services.EmployeeTransactionReports.Models;

namespace Engage.Application.Services.EmployeeTransactions.Commands;

public class EmployeeTransactionOvertimeReportCommand : EmployeeTransactionReportCommand, IRequest<EmployeeReportListVM<EmployeeTransactionsOvertimeDto>>
{
}

public class EmployeeTransactionOvertimeReportHandler : BaseQueryHandler, IRequestHandler<EmployeeTransactionOvertimeReportCommand, EmployeeReportListVM<EmployeeTransactionsOvertimeDto>>
{
    public EmployeeTransactionOvertimeReportHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeReportListVM<EmployeeTransactionsOvertimeDto>> Handle(EmployeeTransactionOvertimeReportCommand command, CancellationToken cancellationToken)
    {
        var query = _context.EmployeeTransactions.Where(e => e.Disabled == false && e.PayrollPeriodId == command.PayrollPeriodId)
                                                 .AsQueryable();

        if (command.EmployeeTransactionTypeId.HasValue)
        {
            query = query.Where(e => e.EmployeeTransactionTypeId == command.EmployeeTransactionTypeId.Value);
        }
        else
        {
            query = query.Where(e => e.EmployeeTransactionType.EmployeeTransactionTypeGroupId == (int)EmployeeTransactionTypeGroupId.Overtime);
        }

        if (command.EngageRegionIds?.Any() == true)
        {
            var employeeIds = await _context.EmployeeRegions.Where(e => command.EngageRegionIds.Contains(e.EngageRegionId))
                                                            .Select(e => e.EmployeeId)
                                                            .Distinct()
                                                            .ToListAsync(cancellationToken);

            query = query.Where(e => employeeIds.Contains(e.EmployeeId));
        }

        var data = await query.OrderBy(c => c.Employee.FirstName)
                              .ProjectTo<EmployeeTransactionsOvertimeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        //We might need to put column headers in an array of strings...
        string[] columns = { "Employee Number*", "Hours", "Start Date", "End Date*", "Comment" };
        List<string> colNames = new List<string>();
        colNames.AddRange(columns);

        return new EmployeeReportListVM<EmployeeTransactionsOvertimeDto>
        {
            Count = data.Count,
            ReportName = "Employee Overtime " + DateTime.Now.ToString(),
            ColumnNames = colNames,
            Data = data,
        };
    }
}