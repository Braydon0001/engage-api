// auto-generated
namespace Engage.Application.Services.SurveyStores.Commands;

public record SurveyStoreBulkDeleteCommand(int SurveyId, List<int> StoreIds) : IRequest<int?>
{
}

public class SurveyStoreBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyStoreBulkDeleteCommand, int?>
{
    public SurveyStoreBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyStoreBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleOrDefaultAsync(e => e.SurveyId == command.SurveyId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyStoreTargets.IgnoreQueryFilters().Where(e => e.SurveyId == command.SurveyId);

        // Delete Ids check
        var deleteIdsCheck = await _context.Stores.Where(e => command.StoreIds.Contains(e.StoreId)).Select(e => e.StoreId).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Store with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreIds.Contains(e.StoreId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyStoreBulkDeleteValidator : AbstractValidator<SurveyStoreBulkDeleteCommand>
{
    public SurveyStoreBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}
