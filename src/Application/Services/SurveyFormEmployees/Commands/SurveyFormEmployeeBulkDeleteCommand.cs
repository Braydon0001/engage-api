namespace Engage.Application.Services.SurveyFormEmployees.Commands;

public record SurveyFormEmployeeBulkDeleteCommand(int SurveyFormId, List<int> EmployeeIds) : IRequest<int?>
{
}

public class SurveyFormEmployeeBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyFormEmployeeBulkDeleteCommand, int?>
{
    public SurveyFormEmployeeBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyFormEmployeeBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormEmployees.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await _context.Employees.Where(e => command.EmployeeIds.Contains(e.EmployeeId)).Select(e => e.EmployeeId).ToListAsync(cancellationToken);
        var notFoundIds = command.EmployeeIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Employees with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EmployeeIds.Contains(e.EmployeeId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormEmployeeBulkDeleteValidator : AbstractValidator<SurveyFormEmployeeBulkDeleteCommand>
{
    public SurveyFormEmployeeBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}
