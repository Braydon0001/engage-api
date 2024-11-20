namespace Engage.Application.Services.SurveyFormStoreFormats.Commands;

public record SurveyFormStoreFormatBulkDeleteCommand(int SurveyFormId, List<int> StoreFormatIds) : IRequest<int?>
{
}

public class SurveyFormStoreFormatBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyFormStoreFormatBulkDeleteCommand, int?>
{
    public SurveyFormStoreFormatBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyFormStoreFormatBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStoreFormats.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await _context.StoreFormats.Where(e => command.StoreFormatIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreFormatIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Store Formats with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreFormatIds.Contains(e.StoreFormatId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormStoreFormatBulkDeleteValidator : AbstractValidator<SurveyFormStoreFormatBulkDeleteCommand>
{
    public SurveyFormStoreFormatBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreFormatIds).NotNull();
        RuleForEach(e => e.StoreFormatIds).GreaterThan(0);
    }
}
