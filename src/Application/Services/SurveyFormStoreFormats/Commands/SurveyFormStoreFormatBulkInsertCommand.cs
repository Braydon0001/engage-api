namespace Engage.Application.Services.SurveyFormStoreFormats.Commands;

public record SurveyFormStoreFormatBulkInsertCommand(int SurveyFormId, List<int> StoreFormatIds) : IRequest<List<int>>
{
}

public class SurveyFormStoreFormatBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyFormStoreFormatBulkInsertCommand, List<int>>
{
    public SurveyFormStoreFormatBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyFormStoreFormatBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStoreFormats.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreFormatId).ToListAsync(cancellationToken);
        var insertIds = command.StoreFormatIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.StoreFormats.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Store Formats with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormStoreFormat { SurveyFormId = command.SurveyFormId, StoreFormatId = id });
        _context.SurveyFormStoreFormats.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormStoreFormatBulkInsertValidator : AbstractValidator<SurveyFormStoreFormatBulkInsertCommand>
{
    public SurveyFormStoreFormatBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreFormatIds).NotNull();
        RuleForEach(e => e.StoreFormatIds).GreaterThan(0);
    }
}