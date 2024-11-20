using Engage.Application.Services.EmployeeReports.Models;
using Engage.Application.Services.EmployeeTransactionReports.Models;

namespace Engage.Application.Services.EmployeeTransactions.Commands;

public class EmployeeTransactionLoanDeductionRecurringReportCommand : GetQuery, IRequest<EmployeeReportListVM<EmployeeTransactionsLoanDeductionRecurringDto>>
{
    public List<int> EngageRegionIds { get; set; }
    public int? EmployeeTransactionTypeId { get; set; }
    public int PayrollPeriodId { get; set; }
}

public class EmployeeTransactionLoanDeductionRecurringReportCommandHandler : BaseQueryHandler, IRequestHandler<EmployeeTransactionLoanDeductionRecurringReportCommand, EmployeeReportListVM<EmployeeTransactionsLoanDeductionRecurringDto>>
{
    public EmployeeTransactionLoanDeductionRecurringReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeReportListVM<EmployeeTransactionsLoanDeductionRecurringDto>> Handle(EmployeeTransactionLoanDeductionRecurringReportCommand command, CancellationToken cancellationToken)
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
            query = query.Where(e => e.EmployeeTransactionType.EmployeeTransactionTypeGroupId == (int)EmployeeTransactionTypeGroupId.LoanDeductionRecurring);
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
                            .ProjectTo<EmployeeTransactionsLoanDeductionRecurringDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        //We might need to put column headers in an array of strings...
        List<string> colNames = new List<string>();
        colNames.Add("Employee Number*");                         //A
        colNames.Add("Initial Amount");                           //B
        colNames.Add("Installment Amount");                       //B
        colNames.Add("Is Fringe Benefit Loan");                   //C
        colNames.Add("Base Installment On Amount Or Component?"); //C
        colNames.Add("Start Date");                               //C
        colNames.Add("End Date*");                                //D
        colNames.Add("Comment");                                  //E

        return new EmployeeReportListVM<EmployeeTransactionsLoanDeductionRecurringDto>
        {
            Count = data.Count,
            ReportName = "Employee Loan Deduction " + DateTime.Now.ToString(),
            ColumnNames = colNames,
            Data = data,
        };
    }
}