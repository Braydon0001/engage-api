// auto-generated
namespace Engage.Application.Services.SurveyStoreFormats.Commands;

public record SurveyStoreFormatBulkInsertCommand(int SurveyId, List<int> StoreFormatIds) : IRequest<List<int>>
{
}

public class SurveyStoreFormatBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyStoreFormatBulkInsertCommand, List<int>>
{
    public SurveyStoreFormatBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyStoreFormatBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleOrDefaultAsync(e => e.SurveyId == command.SurveyId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyStoreFormatTargets.IgnoreQueryFilters().Where(e => e.SurveyId == command.SurveyId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreFormatId).ToListAsync(cancellationToken);
        var insertIds = command.StoreFormatIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.StoreFormats.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no StoreFormat with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyStoreFormatTarget { SurveyId = command.SurveyId, StoreFormatId = id });
        _context.SurveyStoreFormatTargets.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyStoreFormatBulkInsertValidator : AbstractValidator<SurveyStoreFormatBulkInsertCommand>
{
    public SurveyStoreFormatBulkInsertValidator()
    {
        RuleFor(e => e.SurveyId).GreaterThan(0);
        RuleFor(e => e.StoreFormatIds).NotNull();
        RuleForEach(e => e.StoreFormatIds).GreaterThan(0);
    }
}