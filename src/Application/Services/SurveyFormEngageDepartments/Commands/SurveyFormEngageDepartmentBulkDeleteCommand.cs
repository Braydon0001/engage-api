namespace Engage.Application.Services.SurveyFormEngageDepartments.Commands;

public record SurveyFormEngageDepartmentBulkDeleteCommand(int SurveyFormId, List<int> EngageDepartmentIds) : IRequest<int?>
{
}

public class SurveyFormEngageDepartmentBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyFormEngageDepartmentBulkDeleteCommand, int?>
{
    public SurveyFormEngageDepartmentBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyFormEngageDepartmentBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormEngageDepartments.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await _context.EngageDepartments.Where(e => command.EngageDepartmentIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = command.EngageDepartmentIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Engage Departments with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EngageDepartmentIds.Contains(e.EngageDepartmentId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormEngageDepartmentBulkDeleteValidator : AbstractValidator<SurveyFormEngageDepartmentBulkDeleteCommand>
{
    public SurveyFormEngageDepartmentBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EngageDepartmentIds).NotNull();
        RuleForEach(e => e.EngageDepartmentIds).GreaterThan(0);
    }
}
