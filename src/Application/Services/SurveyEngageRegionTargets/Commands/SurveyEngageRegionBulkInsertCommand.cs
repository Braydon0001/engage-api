// auto-generated
namespace Engage.Application.Services.SurveyEngageRegions.Commands;

public record SurveyEngageRegionBulkInsertCommand(int SurveyId, List<int> EngageRegionIds) : IRequest<List<int>>
{
}

public class SurveyEngageRegionBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyEngageRegionBulkInsertCommand, List<int>>
{
    public SurveyEngageRegionBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyEngageRegionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleOrDefaultAsync(e => e.SurveyId == command.SurveyId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyEngageRegionTargets.IgnoreQueryFilters().Where(e => e.SurveyId == command.SurveyId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EngageRegionId).ToListAsync(cancellationToken);
        var insertIds = command.EngageRegionIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.EngageRegions.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no EngageRegion with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyEngageRegionTarget { SurveyId = command.SurveyId, EngageRegionId = id });
        _context.SurveyEngageRegionTargets.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyEngageRegionBulkInsertValidator : AbstractValidator<SurveyEngageRegionBulkInsertCommand>
{
    public SurveyEngageRegionBulkInsertValidator()
    {
        RuleFor(e => e.SurveyId).GreaterThan(0);
        RuleFor(e => e.EngageRegionIds).NotNull();
        RuleForEach(e => e.EngageRegionIds).GreaterThan(0);
    }
}