namespace Engage.Application.Services.SurveyFormStoreClusters.Commands;

public record SurveyFormStoreClusterBulkInsertCommand(int SurveyFormId, List<int> StoreClusterIds) : IRequest<List<int>>
{
}

public class SurveyFormStoreClusterBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyFormStoreClusterBulkInsertCommand, List<int>>
{
    public SurveyFormStoreClusterBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyFormStoreClusterBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStoreClusters.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreClusterId).ToListAsync(cancellationToken);
        var insertIds = command.StoreClusterIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.StoreClusters.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Store Clusters with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormStoreCluster { SurveyFormId = command.SurveyFormId, StoreClusterId = id });
        _context.SurveyFormStoreClusters.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormStoreClusterBulkInsertValidator : AbstractValidator<SurveyFormStoreClusterBulkInsertCommand>
{
    public SurveyFormStoreClusterBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreClusterIds).NotNull();
        RuleForEach(e => e.StoreClusterIds).GreaterThan(0);
    }
}