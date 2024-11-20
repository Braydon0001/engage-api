namespace Engage.Application.Services.SurveyFormStoreEngageRegions.Commands;

public record SurveyFormStoreEngageRegionBulkInsertCommand(int SurveyFormId, List<int> StoreEngageRegionIds) : IRequest<List<int>>
{
}

public class SurveyFormStoreEngageRegionBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyFormStoreEngageRegionBulkInsertCommand, List<int>>
{
    public SurveyFormStoreEngageRegionBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyFormStoreEngageRegionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStoreEngageRegions.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreEngageRegionId).ToListAsync(cancellationToken);
        var insertIds = command.StoreEngageRegionIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.EngageRegions.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Engage Regions with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormStoreEngageRegion { SurveyFormId = command.SurveyFormId, StoreEngageRegionId = id });
        _context.SurveyFormStoreEngageRegions.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormStoreEngageRegionBulkInsertValidator : AbstractValidator<SurveyFormStoreEngageRegionBulkInsertCommand>
{
    public SurveyFormStoreEngageRegionBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreEngageRegionIds).NotNull();
        RuleForEach(e => e.StoreEngageRegionIds).GreaterThan(0);
    }
}