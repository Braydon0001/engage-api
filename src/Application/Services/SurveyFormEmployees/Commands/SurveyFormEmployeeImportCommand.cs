
namespace Engage.Application.Services.SurveyFormEmployees.Commands;

public class SurveyFormEmployeeImportCommand : IRequest<List<int>>
{
    public int SurveyFormId { get; set; }
    public List<SurveyFormEmployeeImport> Employees { get; set; }
}

public record SurveyFormEmployeeImportHandler(IMediator Mediator, IAppDbContext Context, ICsvService CsvService) : IRequestHandler<SurveyFormEmployeeImportCommand, List<int>>
{
    public async Task<List<int>> Handle(SurveyFormEmployeeImportCommand command, CancellationToken cancellationToken)
    {
        var importedEmployees = command.Employees.Select(e => e.EmployeeId).ToList();

        var employeeIds = await Context.Employees.Where(e => importedEmployees.Contains(e.EmployeeId))
                                           .Select(e => e.EmployeeId)
                                           .Distinct()
                                           .ToListAsync(cancellationToken);

        return await Mediator.Send(new SurveyFormEmployeeBulkInsertCommand(command.SurveyFormId, employeeIds), cancellationToken);

    }
}
