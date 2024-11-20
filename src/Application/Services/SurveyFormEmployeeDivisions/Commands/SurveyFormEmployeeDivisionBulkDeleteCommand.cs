namespace Engage.Application.Services.SurveyFormEmployeeDivisions.Commands;

public record SurveyFormEmployeeDivisionBulkDeleteCommand(int SurveyFormId, List<int> EmployeeDivisionIds) : IRequest<int?>
{
}

public class SurveyFormEmployeeDivisionBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyFormEmployeeDivisionBulkDeleteCommand, int?>
{
    public SurveyFormEmployeeDivisionBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyFormEmployeeDivisionBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormEmployeeDivisions.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await _context.EmployeeDivisions.Where(e => command.EmployeeDivisionIds.Contains(e.EmployeeDivisionId)).Select(e => e.EmployeeDivisionId).ToListAsync(cancellationToken);
        var notFoundIds = command.EmployeeDivisionIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Divisions with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EmployeeDivisionIds.Contains(e.EmployeeDivisionId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormEmployeeDivisionBulkDeleteValidator : AbstractValidator<SurveyFormEmployeeDivisionBulkDeleteCommand>
{
    public SurveyFormEmployeeDivisionBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EmployeeDivisionIds).NotNull();
        RuleForEach(e => e.EmployeeDivisionIds).GreaterThan(0);
    }
}
