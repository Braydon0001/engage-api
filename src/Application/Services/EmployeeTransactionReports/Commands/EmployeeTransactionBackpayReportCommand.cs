using Engage.Application.Services.EmployeeReports.Models;
using Engage.Application.Services.EmployeeTransactionReports.Models;

namespace Engage.Application.Services.EmployeeTransactions.Commands;

public class EmployeeTransactionBackpayReportCommand : GetQuery, IRequest<EmployeeReportListVM<EmployeeTransactionsBackpayDto>>
{
    public List<int> EngageRegionIds { get; set; }
    public int? EmployeeTransactionTypeId { get; set; }
    public int PayrollPeriodId { get; set; }
}

public class EmployeeTransactionBackpayReportCommandHandler : BaseQueryHandler, IRequestHandler<EmployeeTransactionBackpayReportCommand, EmployeeReportListVM<EmployeeTransactionsBackpayDto>>
{
    public EmployeeTransactionBackpayReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeReportListVM<EmployeeTransactionsBackpayDto>> Handle(EmployeeTransactionBackpayReportCommand command, CancellationToken cancellationToken)
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
            query = query.Where(e => e.EmployeeTransactionType.EmployeeTransactionTypeGroupId == (int)EmployeeTransactionTypeGroupId.BackPay);
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
                            .ProjectTo<EmployeeTransactionsBackpayDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        //We might need to put column headers in an array of strings...
        List<string> colNames = new List<string>();
        colNames.Add("Employee Number*");             //A
        colNames.Add("Amount");                       //B
        colNames.Add("Days");                         //C
        colNames.Add("Hours");                        //D
        colNames.Add("Start Date");                   //E
        colNames.Add("End Date*");                    //F
        colNames.Add("Comment");                      //G

        return new EmployeeReportListVM<EmployeeTransactionsBackpayDto>
        {
            Count = data.Count,
            ReportName = "Employee Backpay " + DateTime.Now.ToString(),
            ColumnNames = colNames,
            Data = data,
        };
    }
}