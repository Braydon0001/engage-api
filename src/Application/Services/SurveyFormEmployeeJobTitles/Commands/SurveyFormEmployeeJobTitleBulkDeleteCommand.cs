namespace Engage.Application.Services.SurveyFormEmployeeJobTitles.Commands;

public record SurveyFormEmployeeJobTitleBulkDeleteCommand(int SurveyFormId, List<int> EmployeeJobTitleIds) : IRequest<int?>
{
}

public class SurveyFormEmployeeJobTitleBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyFormEmployeeJobTitleBulkDeleteCommand, int?>
{
    public SurveyFormEmployeeJobTitleBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyFormEmployeeJobTitleBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormEmployeeJobTitles.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await _context.EmployeeJobTitles.Where(e => command.EmployeeJobTitleIds.Contains(e.EmployeeJobTitleId)).Select(e => e.EmployeeJobTitleId).ToListAsync(cancellationToken);
        var notFoundIds = command.EmployeeJobTitleIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Job Titles with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EmployeeJobTitleIds.Contains(e.EmployeeJobTitleId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormEmployeeJobTitleBulkDeleteValidator : AbstractValidator<SurveyFormEmployeeJobTitleBulkDeleteCommand>
{
    public SurveyFormEmployeeJobTitleBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleIds).NotNull();
        RuleForEach(e => e.EmployeeJobTitleIds).GreaterThan(0);
    }
}
