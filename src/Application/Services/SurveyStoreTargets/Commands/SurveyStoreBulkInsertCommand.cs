// auto-generated
namespace Engage.Application.Services.SurveyStores.Commands;

public record SurveyStoreBulkInsertCommand(int SurveyId, List<int> StoreIds) : IRequest<List<int>>
{
}

public class SurveyStoreBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyStoreBulkInsertCommand, List<int>>
{
    public SurveyStoreBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyStoreBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleOrDefaultAsync(e => e.SurveyId == command.SurveyId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyStoreTargets.IgnoreQueryFilters().Where(e => e.SurveyId == command.SurveyId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreId).ToListAsync(cancellationToken);
        var insertIds = command.StoreIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.Stores.Where(e => insertIds.Contains(e.StoreId)).Select(e => e.StoreId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Store with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyStoreTarget { SurveyId = command.SurveyId, StoreId = id });
        _context.SurveyStoreTargets.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyStoreBulkInsertValidator : AbstractValidator<SurveyStoreBulkInsertCommand>
{
    public SurveyStoreBulkInsertValidator()
    {
        RuleFor(e => e.SurveyId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}