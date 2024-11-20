using Engage.Application.Services.EmployeeReports.Models;

namespace Engage.Application.Services.EmployeeReports.Commands;

public class GenerateEmployeeEmploymentStatusReportCommand : GetQuery, IRequest<EmployeeReportListVM<EmployeeEmploymentStatusDto>>
{
    //Optional
    public List<int> EngageRegionIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GenerateEmployeeEmploymentStatusReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateEmployeeEmploymentStatusReportCommand, EmployeeReportListVM<EmployeeEmploymentStatusDto>>
{
    public GenerateEmployeeEmploymentStatusReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeReportListVM<EmployeeEmploymentStatusDto>> Handle(GenerateEmployeeEmploymentStatusReportCommand command, CancellationToken cancellationToken)
    {
        var query = _context.Employees.Where(e => e.Disabled == false);

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
            query = query.Where(e=> e.StartingDate.Date >= command.StartDate.Value.Date);
        }

        if (command.EndDate.HasValue)
        {
            query = query.Where(e => e.EndDate.Value.Date <= command.EndDate.Value.Date);
        }

        var data = await query
                            .OrderBy(c => c.FirstName)
                            .ProjectTo<EmployeeEmploymentStatusDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        //We might need to put column headers in an array of strings...
        List<string> colNames = new List<string>();
        colNames.Add("Employee Number*");                          //A
        colNames.Add("Employment Action");                         //B
        colNames.Add("Group join date*");                          //C
        colNames.Add("Employment date*");                          //D
        colNames.Add("Not re-employable");                         //E
        colNames.Add("NatureOfPerson*");                           //F
        colNames.Add("IdentityType");                              //G
        colNames.Add("number");                                    //H
        colNames.Add("Permit issue date");                         //I
        colNames.Add("Permit expiry date");                        //J
        colNames.Add("PassportCountry");                           //K
        colNames.Add("Passport number");                           //L
        colNames.Add("Passport issue date");                       //M
        colNames.Add("Passport expiry date");                      //N
        colNames.Add("TaxStatus*");                                //O
        colNames.Add("Tax directive number");                      //P
        colNames.Add("Has An IRP30 Certificate Been Issued?");     //Q
        colNames.Add("Percentage / Amount");                       //R
        colNames.Add("Percent");                                   //S
        colNames.Add("Amount");                                    //T
        colNames.Add("Tax reference number*");                     //U
        colNames.Add("Reference number");                          //V
        colNames.Add("Termination date");                          //W
        colNames.Add("Termination Reason");                        //X
        colNames.Add("Termination Run");                           //Y
        colNames.Add("Encash Leave");                              //Z

        return new EmployeeReportListVM<EmployeeEmploymentStatusDto>
        {
            Count = data.Count,
            ReportName = "Employee Bulk Upload " + DateTime.Now.ToString(),
            ColumnNames = colNames,
            Data = data,
        };
    }
}
