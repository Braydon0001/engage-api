// auto-generated
namespace Engage.Application.Services.SurveyEngageRegions.Commands;

public record SurveyEngageRegionBulkDeleteCommand(int SurveyId, List<int> EngageRegionIds) : IRequest<int?>
{
}

public class SurveyEngageRegionBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyEngageRegionBulkDeleteCommand, int?>
{
    public SurveyEngageRegionBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyEngageRegionBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleOrDefaultAsync(e => e.SurveyId == command.SurveyId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyEngageRegionTargets.IgnoreQueryFilters().Where(e => e.SurveyId == command.SurveyId);

        // Delete Ids check
        var deleteIdsCheck = await _context.EngageRegions.Where(e => command.EngageRegionIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = command.EngageRegionIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no EngageRegion with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EngageRegionIds.Contains(e.EngageRegionId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyEngageRegionBulkDeleteValidator : AbstractValidator<SurveyEngageRegionBulkDeleteCommand>
{
    public SurveyEngageRegionBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyId).GreaterThan(0);
        RuleFor(e => e.EngageRegionIds).NotNull();
        RuleForEach(e => e.EngageRegionIds).GreaterThan(0);
    }
}
