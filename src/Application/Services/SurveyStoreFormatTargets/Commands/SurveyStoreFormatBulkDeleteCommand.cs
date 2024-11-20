// auto-generated
namespace Engage.Application.Services.SurveyStoreFormats.Commands;

public record SurveyStoreFormatBulkDeleteCommand(int SurveyId, List<int> StoreFormatIds) : IRequest<int?>
{
}

public class SurveyStoreFormatBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyStoreFormatBulkDeleteCommand, int?>
{
    public SurveyStoreFormatBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyStoreFormatBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleOrDefaultAsync(e => e.SurveyId == command.SurveyId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyStoreFormatTargets.IgnoreQueryFilters().Where(e => e.SurveyId == command.SurveyId);

        // Delete Ids check
        var deleteIdsCheck = await _context.StoreFormats.Where(e => command.StoreFormatIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreFormatIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no StoreFormat with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreFormatIds.Contains(e.StoreFormatId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyStoreFormatBulkDeleteValidator : AbstractValidator<SurveyStoreFormatBulkDeleteCommand>
{
    public SurveyStoreFormatBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyId).GreaterThan(0);
        RuleFor(e => e.StoreFormatIds).NotNull();
        RuleForEach(e => e.StoreFormatIds).GreaterThan(0);
    }
}
