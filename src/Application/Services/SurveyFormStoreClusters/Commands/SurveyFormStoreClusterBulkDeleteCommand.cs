namespace Engage.Application.Services.SurveyFormStoreClusters.Commands;

public record SurveyFormStoreClusterBulkDeleteCommand(int SurveyFormId, List<int> StoreClusterIds) : IRequest<int?>
{
}

public class SurveyFormStoreClusterBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyFormStoreClusterBulkDeleteCommand, int?>
{
    public SurveyFormStoreClusterBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyFormStoreClusterBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStoreClusters.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await _context.StoreClusters.Where(e => command.StoreClusterIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreClusterIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Store Clusters with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreClusterIds.Contains(e.StoreClusterId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormStoreClusterBulkDeleteValidator : AbstractValidator<SurveyFormStoreClusterBulkDeleteCommand>
{
    public SurveyFormStoreClusterBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreClusterIds).NotNull();
        RuleForEach(e => e.StoreClusterIds).GreaterThan(0);
    }
}
