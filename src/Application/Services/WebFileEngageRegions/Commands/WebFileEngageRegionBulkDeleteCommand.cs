// auto-generated
namespace Engage.Application.Services.WebFileEngageRegions.Commands;

public record WebFileEngageRegionBulkDeleteCommand(int WebFileId, List<int> EngageRegionIds) : IRequest<int?>
{
}

public class WebFileEngageRegionBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<WebFileEngageRegionBulkDeleteCommand, int?>
{
    public WebFileEngageRegionBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(WebFileEngageRegionBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.WebFileEngageRegions.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);

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

public class WebFileEngageRegionBulkDeleteValidator : AbstractValidator<WebFileEngageRegionBulkDeleteCommand>
{
    public WebFileEngageRegionBulkDeleteValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.EngageRegionIds).NotNull();
        RuleForEach(e => e.EngageRegionIds).GreaterThan(0);
    }
}
