using Engage.Application.Services.EmployeeReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Application.Services.EmployeeReports.Commands;

public class GenerateEmployeeBankDetailsReportCommand : GetQuery, IRequest<EmployeeReportListVM<EmployeeBankDetailsDto>>
{
    //Optional
    public List<int> EngageRegionIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GenerateEmployeeBankDetailsReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateEmployeeBankDetailsReportCommand, EmployeeReportListVM<EmployeeBankDetailsDto>>
{
    public GenerateEmployeeBankDetailsReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeReportListVM<EmployeeBankDetailsDto>> Handle(GenerateEmployeeBankDetailsReportCommand command, CancellationToken cancellationToken)
    {
        var query = _context.EmployeeBankDetails.Where(e => e.Disabled == false);

        if(command.EngageRegionIds?.Any() == true)
        {
            var employeeIds = await _context.EmployeeRegions.Where(e => command.EngageRegionIds.Contains(e.EngageRegionId))
                                                         .Select(e => e.EmployeeId)
                                                         .Distinct()
                                                         .ToListAsync(cancellationToken);

            query = query.Where(e => employeeIds.Contains(e.EmployeeId));
        }

        if (command.StartDate.HasValue)
        {
            query = query.Where(e=> e.Employee.StartingDate.Date >= command.StartDate.Value.Date);
        }

        if (command.EndDate.HasValue)
        {
            query = query.Where(e => e.Employee.EndDate.Value.Date <= command.EndDate.Value.Date);
        }

        var data = await query
                            .OrderBy(c => c.Employee.FirstName)
                            .ProjectTo<EmployeeBankDetailsDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        //We might need to put column headers in an array of strings...
        List<string> colNames = new List<string>();
        colNames.Add("Employee Number *");                                      //A
        colNames.Add("Payment Method*");                                        //B
        colNames.Add("Bank Account Owner");                                     //C
        colNames.Add("Bank Account Owner Name");                                //D
        colNames.Add("Account Type");                                           //E
        colNames.Add("Bank Name*");                                             //F
        colNames.Add("Branch No*");                                             //G
        colNames.Add("Account No*");                                            //H
        colNames.Add("Beneficiary Reference");                                  //I
        colNames.Add("Comments");                                               //J
        colNames.Add("Swift Code");                                             //K
        colNames.Add("Routing Code");                                           //L
        colNames.Add("Indicator");                                              //M
        colNames.Add("Please choose which split method you would like to use"); //N
        colNames.Add("Amount");                                                 //O
        colNames.Add("Payslip Component");                                      //P
        colNames.Add("Currency");                                               //Q

        return new EmployeeReportListVM<EmployeeBankDetailsDto>
        {
            Count = data.Count,
            ReportName = "Employee Bulk Upload " + DateTime.Now.ToString(),
            ColumnNames = colNames,
            Data = data,
        };
    }
}
