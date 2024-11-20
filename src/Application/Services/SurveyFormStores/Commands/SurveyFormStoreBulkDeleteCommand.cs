namespace Engage.Application.Services.SurveyFormStores.Commands;

public record SurveyFormStoreBulkDeleteCommand(int SurveyFormId, List<int> StoreIds) : IRequest<int?>
{
}

public class SurveyFormStoreBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyFormStoreBulkDeleteCommand, int?>
{
    public SurveyFormStoreBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyFormStoreBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStores.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await _context.Stores.Where(e => command.StoreIds.Contains(e.StoreId)).Select(e => e.StoreId).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Stores with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreIds.Contains(e.StoreId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormStoreBulkDeleteValidator : AbstractValidator<SurveyFormStoreBulkDeleteCommand>
{
    public SurveyFormStoreBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}
