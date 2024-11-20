// auto-generated
namespace Engage.Application.Services.NotificationStores.Commands;

public record NotificationStoreBulkDeleteCommand(int NotificationId, List<int> StoreIds) : IRequest<int?>
{
}

public class NotificationStoreBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<NotificationStoreBulkDeleteCommand, int?>
{
    public NotificationStoreBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(NotificationStoreBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.NotificationId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.NotificationStores.IgnoreQueryFilters().Where(e => e.NotificationId == command.NotificationId);

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

public class NotificationStoreBulkDeleteValidator : AbstractValidator<NotificationStoreBulkDeleteCommand>
{
    public NotificationStoreBulkDeleteValidator()
    {
        RuleFor(e => e.NotificationId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}
