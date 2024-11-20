// auto-generated
namespace Engage.Application.Services.SurveyEmployees.Commands;

public record SurveyEmployeeBulkDeleteCommand(int SurveyId, List<int> EmployeeIds) : IRequest<int?>
{
}

public class SurveyEmployeeBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyEmployeeBulkDeleteCommand, int?>
{
    public SurveyEmployeeBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyEmployeeBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleOrDefaultAsync(e => e.SurveyId == command.SurveyId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyEmployeeTargets.IgnoreQueryFilters().Where(e => e.SurveyId == command.SurveyId);

        // Delete Ids check
        var deleteIdsCheck = await _context.Employees.Where(e => command.EmployeeIds.Contains(e.EmployeeId)).Select(e => e.EmployeeId).ToListAsync(cancellationToken);
        var notFoundIds = command.EmployeeIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Employee with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EmployeeIds.Contains(e.EmployeeId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyEmployeeBulkDeleteValidator : AbstractValidator<SurveyEmployeeBulkDeleteCommand>
{
    public SurveyEmployeeBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}
