namespace Engage.Application.Services.SurveyFormStoreLSMs.Commands;

public record SurveyFormStoreLSMBulkDeleteCommand(int SurveyFormId, List<int> StoreLSMIds) : IRequest<int?>
{
}

public class SurveyFormStoreLSMBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyFormStoreLSMBulkDeleteCommand, int?>
{
    public SurveyFormStoreLSMBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyFormStoreLSMBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStoreLSMs.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await _context.StoreLSMs.Where(e => command.StoreLSMIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreLSMIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Store LSMs with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreLSMIds.Contains(e.StoreLSMId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormStoreLSMBulkDeleteValidator : AbstractValidator<SurveyFormStoreLSMBulkDeleteCommand>
{
    public SurveyFormStoreLSMBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreLSMIds).NotNull();
        RuleForEach(e => e.StoreLSMIds).GreaterThan(0);
    }
}
