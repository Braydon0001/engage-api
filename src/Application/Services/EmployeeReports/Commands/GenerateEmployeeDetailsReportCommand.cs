using Engage.Application.Services.EmployeeReports.Models;

namespace Engage.Application.Services.EmployeeReports.Commands;

public class GenerateEmployeeDetailsReportCommand : GetQuery, IRequest<EmployeeReportListVM<EmployeeDetailsDto>>
{
    //Optional
    public List<int> EngageRegionIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GenerateEmployeeDetailsReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateEmployeeDetailsReportCommand, EmployeeReportListVM<EmployeeDetailsDto>>
{
    public GenerateEmployeeDetailsReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeReportListVM<EmployeeDetailsDto>> Handle(GenerateEmployeeDetailsReportCommand command, CancellationToken cancellationToken)
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
                            .ProjectTo<EmployeeDetailsDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        //We might need to put column headers in an array of strings...
        List<string> colNames = new List<string>();
        colNames.Add("Employee Number *");                         //A
        colNames.Add("Title *");                                   //B
        colNames.Add("First Name *");                              //C
        colNames.Add("Middle Names");                              //D
        colNames.Add("Last Name *");                               //E
        colNames.Add("Initials *");                                //F
        colNames.Add("Home Number");                               //G
        colNames.Add("Work Number");                               //H
        colNames.Add("Cell Number");                               //I
        colNames.Add("Work Extension");                            //J
        colNames.Add("Email");                                     //K
        colNames.Add("Language *");                                //L
        colNames.Add("Gender *");                                  //M
        colNames.Add("Race *");                                    //N
        colNames.Add("Nationality *");                             //O
        colNames.Add("Citizenship *");                             //P
        colNames.Add("Disabled *");                                //Q
        colNames.Add("UIF Excemption Rule *");                     //R
        colNames.Add("SDL Excemption Rule *");                     //S
        colNames.Add("Birth Date (yyyy/mm/dd) *");                 //T
        colNames.Add("Default Payslip");                           //U
        colNames.Add("Custom Field");                              //V
        colNames.Add("Preferred Name");                            //W
        colNames.Add("Marital Status");                            //X
        colNames.Add("Retired");                                   //Y
        colNames.Add("Emergency Contact Name");                    //Z
        colNames.Add("Emergency Contact Number");                  //AA
        colNames.Add("Emergency Contact Address");                 //AB

        return new EmployeeReportListVM<EmployeeDetailsDto>
        {
            Count = data.Count,
            ReportName = "Employee Bulk Upload " + DateTime.Now.ToString(),
            ColumnNames = colNames,
            Data = data,
        };
    }
}
