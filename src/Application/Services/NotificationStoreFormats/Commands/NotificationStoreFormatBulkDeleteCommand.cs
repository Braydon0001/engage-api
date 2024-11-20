// auto-generated
namespace Engage.Application.Services.NotificationStoreFormats.Commands;

public record NotificationStoreFormatBulkDeleteCommand(int NotificationId, List<int> StoreFormatIds) : IRequest<int?>
{
}

public class NotificationStoreFormatBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<NotificationStoreFormatBulkDeleteCommand, int?>
{
    public NotificationStoreFormatBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(NotificationStoreFormatBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.NotificationId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.NotificationStoreFormats.IgnoreQueryFilters().Where(e => e.NotificationId == command.NotificationId);

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

public class NotificationStoreFormatBulkDeleteValidator : AbstractValidator<NotificationStoreFormatBulkDeleteCommand>
{
    public NotificationStoreFormatBulkDeleteValidator()
    {
        RuleFor(e => e.NotificationId).GreaterThan(0);
        RuleFor(e => e.StoreFormatIds).NotNull();
        RuleForEach(e => e.StoreFormatIds).GreaterThan(0);
    }
}
