namespace Engage.Application.Services.SurveyFormExcludedEmployees.Commands;

public class SurveyFormExcludedEmployeeBulkDeleteCommand : IRequest<int?>
{
    public int SurveyFormId { get; set; }
    public List<int> EmployeeIds { get; set; }
}

public record SurveyFormExcludedEmployeeBulkDeleteHandler(IAppDbContext Context) : IRequestHandler<SurveyFormExcludedEmployeeBulkDeleteCommand, int?>
{
    public async Task<int?> Handle(SurveyFormExcludedEmployeeBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = Context.SurveyFormExcludedEmployees.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await Context.Employees.Where(e => command.EmployeeIds.Contains(e.EmployeeId)).Select(e => e.EmployeeId).ToListAsync(cancellationToken);
        var notFoundIds = command.EmployeeIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Employees with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EmployeeIds.Contains(e.ExcludedEmployeeId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormExcludedEmployeeBulkDeleteValidator : AbstractValidator<SurveyFormExcludedEmployeeBulkDeleteCommand>
{
    public SurveyFormExcludedEmployeeBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}