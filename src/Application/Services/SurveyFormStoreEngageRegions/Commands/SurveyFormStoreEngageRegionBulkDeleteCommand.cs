namespace Engage.Application.Services.SurveyFormStoreEngageRegions.Commands;

public record SurveyFormStoreEngageRegionBulkDeleteCommand(int SurveyFormId, List<int> StoreEngageRegionIds) : IRequest<int?>
{
}

public class SurveyFormStoreEngageRegionBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyFormStoreEngageRegionBulkDeleteCommand, int?>
{
    public SurveyFormStoreEngageRegionBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyFormStoreEngageRegionBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStoreEngageRegions.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await _context.EngageRegions.Where(e => command.StoreEngageRegionIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreEngageRegionIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Engage Regions with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreEngageRegionIds.Contains(e.StoreEngageRegionId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormStoreEngageRegionBulkDeleteValidator : AbstractValidator<SurveyFormStoreEngageRegionBulkDeleteCommand>
{
    public SurveyFormStoreEngageRegionBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreEngageRegionIds).NotNull();
        RuleForEach(e => e.StoreEngageRegionIds).GreaterThan(0);
    }
}
