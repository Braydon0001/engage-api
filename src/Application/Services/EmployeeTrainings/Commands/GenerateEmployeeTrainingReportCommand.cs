using Engage.Application.Services.ClaimReports.Models;
using Engage.Application.Services.EmployeeTrainings.Models;

namespace Engage.Application.Services.EmployeeTrainings.Commands;

public class GenerateEmployeeTrainingReportCommand : GetQuery, IRequest<ReportListVM<EmployeeTrainingReportDto>>
{
    public List<int> EngageRegionIds { get; set; }
    public List<int> TrainingIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GenerateEmployeeTrainingReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateEmployeeTrainingReportCommand, ReportListVM<EmployeeTrainingReportDto>>
{
    public GenerateEmployeeTrainingReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ReportListVM<EmployeeTrainingReportDto>> Handle(GenerateEmployeeTrainingReportCommand command, CancellationToken cancellationToken)
    {
        //var query = _context.EmployeeTrainings.Where(t => command.EngageRegionIds.Contains(t.Training.EngageRegionId.Value));
        var query = _context.EmployeeTrainings.AsQueryable();

        if (command.EngageRegionIds != null && command.EngageRegionIds.Any())
        {
            query = query.Where(t => command.EngageRegionIds.Contains(t.Training.EngageRegionId.Value));
        }

        if (command.StartDate.HasValue)
        {
            query = query.Where(t => t.Training.StartDate.Date >= command.StartDate.Value.Date);
        }

        if (command.EndDate.HasValue)
        {
            query = query.Where(t => t.Training.EndDate.Date <= command.EndDate.Value.Date);
        }

        if (command.TrainingIds != null)
        {
            if (command.TrainingIds.Count > 0)
            {
                query = query.Where(c => command.TrainingIds.Contains(c.TrainingId));
            }
        }

        var data = await query.ProjectTo<EmployeeTrainingReportDto>(_mapper.ConfigurationProvider)
                              .ToListAsync();

        string reportName = "Employee Training Report - " + DateTime.Now.ToString();

        return new ReportListVM<EmployeeTrainingReportDto>
        {
            Count = data.Count,
            ReportName = reportName,
            Data = data,
        };
    }
}
