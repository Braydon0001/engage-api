namespace Engage.Application.Services.SurveyFormExcludedStores.Commands;

public class SurveyFormExcludedStoreBulkDeleteCommand : IRequest<int?>
{
    public int SurveyFormId { get; set; }
    public List<int> StoreIds { get; set; }
}

public record SurveyFormExcludedStoreBulkDeleteHandler(IAppDbContext Context) : IRequestHandler<SurveyFormExcludedStoreBulkDeleteCommand, int?>
{
    public async Task<int?> Handle(SurveyFormExcludedStoreBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = Context.SurveyFormExcludedStores.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await Context.Stores.Where(e => command.StoreIds.Contains(e.StoreId)).Select(e => e.StoreId).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Stores with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreIds.Contains(e.ExcludedStoreId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormExcludedStoreBulkDeleteValidator : AbstractValidator<SurveyFormExcludedStoreBulkDeleteCommand>
{
    public SurveyFormExcludedStoreBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}