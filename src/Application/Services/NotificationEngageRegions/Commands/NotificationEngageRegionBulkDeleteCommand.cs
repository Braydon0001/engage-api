// auto-generated
namespace Engage.Application.Services.NotificationEngageRegions.Commands;

public record NotificationEngageRegionBulkDeleteCommand(int NotificationId, List<int> EngageRegionIds) : IRequest<int?>
{
}

public class NotificationEngageRegionBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<NotificationEngageRegionBulkDeleteCommand, int?>
{
    public NotificationEngageRegionBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(NotificationEngageRegionBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.NotificationId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.NotificationEngageRegions.IgnoreQueryFilters().Where(e => e.NotificationId == command.NotificationId);

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

public class NotificationEngageRegionBulkDeleteValidator : AbstractValidator<NotificationEngageRegionBulkDeleteCommand>
{
    public NotificationEngageRegionBulkDeleteValidator()
    {
        RuleFor(e => e.NotificationId).GreaterThan(0);
        RuleFor(e => e.EngageRegionIds).NotNull();
        RuleForEach(e => e.EngageRegionIds).GreaterThan(0);
    }
}
