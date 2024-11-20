using Engage.Application.Services.EmployeeReports.Models;
using Engage.Application.Services.EmployeeTransactionReports.Models;

namespace Engage.Application.Services.EmployeeTransactions.Commands;

public class EmployeeTransactionUnpaidReportCommand : GetQuery, IRequest<EmployeeReportListVM<EmployeeTransactionsUnpaidDto>>
{
    public List<int> EngageRegionIds { get; set; }
    public int? EmployeeTransactionTypeId { get; set; }
    public int PayrollPeriodId { get; set; }
}

public class EmployeeTransactionUnpaidReportCommandHandler : BaseQueryHandler, IRequestHandler<EmployeeTransactionUnpaidReportCommand, EmployeeReportListVM<EmployeeTransactionsUnpaidDto>>
{
    public EmployeeTransactionUnpaidReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeReportListVM<EmployeeTransactionsUnpaidDto>> Handle(EmployeeTransactionUnpaidReportCommand command, CancellationToken cancellationToken)
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
            query = query.Where(e => e.EmployeeTransactionType.EmployeeTransactionTypeGroupId == (int)EmployeeTransactionTypeGroupId.Unpaid);
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
                            .ProjectTo<EmployeeTransactionsUnpaidDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        //We might need to put column headers in an array of strings...
        List<string> colNames = new List<string>();
        colNames.Add("Employee Number*");             //A
        colNames.Add("Unpaid Days");                  //B
        colNames.Add("Unpaid Hours");                 //C
        colNames.Add("Start Date");                   //D
        colNames.Add("End Date*");                    //E
        colNames.Add("Comment");                      //F

        return new EmployeeReportListVM<EmployeeTransactionsUnpaidDto>
        {
            Count = data.Count,
            ReportName = "Employee Unpaid Time " + DateTime.Now.ToString(),
            ColumnNames = colNames,
            Data = data,
        };
    }
}