using Engage.Application.Services.EmployeeReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Application.Services.EmployeeReports.Commands;

public class GenerateEmployeeAddressesReportCommand : GetQuery, IRequest<EmployeeReportListVM<EmployeeAddressDetailsDto>>
{
    //Optional
    public List<int> EngageRegionIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GenerateEmployeeAddressesReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateEmployeeAddressesReportCommand, EmployeeReportListVM<EmployeeAddressDetailsDto>>
{
    public GenerateEmployeeAddressesReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeReportListVM<EmployeeAddressDetailsDto>> Handle(GenerateEmployeeAddressesReportCommand command, CancellationToken cancellationToken)
    {
        var query = _context.EmployeeAddresses.Where(e => e.Disabled == false);

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
                            .ProjectTo<EmployeeAddressDetailsDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        //We might need to put column headers in an array of strings...
        List<string> colNames = new List<string>();
        colNames.Add("Employee Number *");                                      //A
        colNames.Add("Address type*");                                          //B
        colNames.Add("Is the postal address the same as physical address?");    //C
        colNames.Add("Unit Number");                                            //D
        colNames.Add("Complex Name");                                           //E
        colNames.Add("Street Number");                                          //F
        colNames.Add("Street Name / Postal address number*");                   //G
        colNames.Add("Suburb or District / Postal agency");                     //H
        colNames.Add("City or Town / Postal city*");                            //I
        colNames.Add("Code*");                                                  //J
        colNames.Add("Country*");                                               //K
        colNames.Add("Province*");                                              //L
        colNames.Add("Special services");                                       //M
        colNames.Add("Is the postal address a care of address?");               //N
        colNames.Add("Care of intermediary");                                   //O

        return new EmployeeReportListVM<EmployeeAddressDetailsDto>
        {
            Count = data.Count,
            ReportName = "Employee Bulk Upload " + DateTime.Now.ToString(),
            ColumnNames = colNames,
            Data = data,
        };
    }
}
