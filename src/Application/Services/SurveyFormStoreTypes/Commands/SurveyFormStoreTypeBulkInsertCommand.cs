namespace Engage.Application.Services.SurveyFormStoreTypes.Commands;

public record SurveyFormStoreTypeBulkInsertCommand(int SurveyFormId, List<int> StoreTypeIds) : IRequest<List<int>>
{
}

public class SurveyFormStoreTypeBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyFormStoreTypeBulkInsertCommand, List<int>>
{
    public SurveyFormStoreTypeBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyFormStoreTypeBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStoreTypes.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreTypeId).ToListAsync(cancellationToken);
        var insertIds = command.StoreTypeIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.StoreTypes.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Store Types with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormStoreType { SurveyFormId = command.SurveyFormId, StoreTypeId = id });
        _context.SurveyFormStoreTypes.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormStoreTypeBulkInsertValidator : AbstractValidator<SurveyFormStoreTypeBulkInsertCommand>
{
    public SurveyFormStoreTypeBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreTypeIds).NotNull();
        RuleForEach(e => e.StoreTypeIds).GreaterThan(0);
    }
}