namespace Engage.Application.Services.SurveyFormExcludedStores.Commands;

public class SurveyFormExcludedStoreBulkInsertCommand : IRequest<List<int>>
{
    public int SurveyFormId { get; set; }
    public List<int> StoreIds { get; set; }
}

public record SurveyFormExcludedStoreBulkInsertHandler(IAppDbContext Context) : IRequestHandler<SurveyFormExcludedStoreBulkInsertCommand, List<int>>
{
    public async Task<List<int>> Handle(SurveyFormExcludedStoreBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = Context.SurveyFormExcludedStores.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.ExcludedStoreId).ToListAsync(cancellationToken);
        var insertIds = command.StoreIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await Context.Stores.Where(e => insertIds.Contains(e.StoreId)).Select(e => e.StoreId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Stores with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormExcludedStore { SurveyFormId = command.SurveyFormId, ExcludedStoreId = id });
        Context.SurveyFormExcludedStores.AddRange(entities);

        //remove stores that are now excludded but were previously included
        var targetedStoresToBeRemoved = await Context.SurveyFormStores.IgnoreQueryFilters()
                                                                            .Where(e => e.SurveyFormId == command.SurveyFormId && command.StoreIds.Contains(e.StoreId))
                                                                            .ToListAsync(cancellationToken);

        if (targetedStoresToBeRemoved.Count > 0)
        {
            Context.SurveyFormStores.RemoveRange(targetedStoresToBeRemoved);
        }

        await Context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormExcludedStoreBulkInsertValidator : AbstractValidator<SurveyFormExcludedStoreBulkInsertCommand>
{
    public SurveyFormExcludedStoreBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}