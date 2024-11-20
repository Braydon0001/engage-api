using Engage.Application.Services.EmployeeReports.Models;
using Engage.Application.Services.EmployeeTransactionReports.Models;

namespace Engage.Application.Services.EmployeeTransactions.Commands;

public class EmployeeTransactionCellPhoneAllowanceRecurringReportCommand : GetQuery, IRequest<EmployeeReportListVM<EmployeeTransactionsCellPhoneAllowanceRecurringDto>>
{
    public List<int> EngageRegionIds { get; set; }
    public int? EmployeeTransactionTypeId { get; set; }
    public int PayrollPeriodId { get; set; }
}

public class EmployeeTransactionCellPhoneAllowanceRecurringReportCommandHandler : BaseQueryHandler, IRequestHandler<EmployeeTransactionCellPhoneAllowanceRecurringReportCommand, EmployeeReportListVM<EmployeeTransactionsCellPhoneAllowanceRecurringDto>>
{
    public EmployeeTransactionCellPhoneAllowanceRecurringReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeReportListVM<EmployeeTransactionsCellPhoneAllowanceRecurringDto>> Handle(EmployeeTransactionCellPhoneAllowanceRecurringReportCommand command, CancellationToken cancellationToken)
    {
        var query = _context.EmployeeTransactions
                            .Where(e => e.Disabled == false
                                && e.PayrollPeriodId == command.PayrollPeriodId
                             );

        if (command.EmployeeTransactionTypeId.HasValue)
        {
            query = query.Where(e => e.EmployeeTransactionTypeId == command.EmployeeTransactionTypeId.Value);
        }
        else
        {
            query = query.Where(e => e.EmployeeTransactionType.EmployeeTransactionTypeGroupId == (int)EmployeeTransactionTypeGroupId.CellPhoneRecurring);
        }

        if (command.EngageRegionIds?.Any() == true)
        {
            var employeeIds = await _context.EmployeeRegions.Where(e => command.EngageRegionIds.Contains(e.EngageRegionId))
                                                         .Select(e => e.EmployeeId)
                                                         .Distinct()
                                                         .ToListAsync(cancellationToken);

            query = query.Where(e => employeeIds.Contains(e.EmployeeId));
        }

        var data = await query
                            .OrderBy(c => c.Employee.FirstName)
                            .ProjectTo<EmployeeTransactionsCellPhoneAllowanceRecurringDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        //We might need to put column headers in an array of strings...
        List<string> colNames = new List<string>();
        colNames.Add("Employee Number*");             //A
        colNames.Add("Amount");                       //B
        colNames.Add("Start Date");                   //C
        colNames.Add("End Date*");                    //D
        colNames.Add("Comment");                      //E

        return new EmployeeReportListVM<EmployeeTransactionsCellPhoneAllowanceRecurringDto>
        {
            Count = data.Count,
            ReportName = "Employee Cell Phone Recurring " + DateTime.Now.ToString(),
            ColumnNames = colNames,
            Data = data,
        };
    }
}