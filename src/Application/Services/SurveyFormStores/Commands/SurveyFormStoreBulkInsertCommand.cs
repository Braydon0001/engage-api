namespace Engage.Application.Services.SurveyFormStores.Commands;

public record SurveyFormStoreBulkInsertCommand(int SurveyFormId, List<int> StoreIds) : IRequest<List<int>>
{
}

public class SurveyFormStoreBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyFormStoreBulkInsertCommand, List<int>>
{
    public SurveyFormStoreBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyFormStoreBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStores.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreId).ToListAsync(cancellationToken);
        var insertIds = command.StoreIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.Stores.Where(e => insertIds.Contains(e.StoreId)).Select(e => e.StoreId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Stores with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormStore { SurveyFormId = command.SurveyFormId, StoreId = id });
        _context.SurveyFormStores.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormStoreBulkInsertValidator : AbstractValidator<SurveyFormStoreBulkInsertCommand>
{
    public SurveyFormStoreBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}