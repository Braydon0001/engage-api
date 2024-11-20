// auto-generated
namespace Engage.Application.Services.NotificationStores.Commands;

public record NotificationStoreBulkInsertCommand(int NotificationId, List<int> StoreIds) : IRequest<List<int>>
{
}

public class NotificationStoreBulkInsertHandler : BulkInsertHandler, IRequestHandler<NotificationStoreBulkInsertCommand, List<int>>
{
    public NotificationStoreBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(NotificationStoreBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.NotificationId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.NotificationStores.IgnoreQueryFilters().Where(e => e.NotificationId == command.NotificationId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreId).ToListAsync(cancellationToken);
        var insertIds = command.StoreIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.Stores.Where(e => insertIds.Contains(e.StoreId)).Select(e => e.StoreId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Store with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new NotificationStore { NotificationId = command.NotificationId, StoreId = id });
        _context.NotificationStores.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class NotificationStoreBulkInsertValidator : AbstractValidator<NotificationStoreBulkInsertCommand>
{
    public NotificationStoreBulkInsertValidator()
    {
        RuleFor(e => e.NotificationId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}