// auto-generated
namespace Engage.Application.Services.NotificationStoreFormats.Commands;

public record NotificationStoreFormatBulkInsertCommand(int NotificationId, List<int> StoreFormatIds) : IRequest<List<int>>
{
}

public class NotificationStoreFormatBulkInsertHandler : BulkInsertHandler, IRequestHandler<NotificationStoreFormatBulkInsertCommand, List<int>>
{
    public NotificationStoreFormatBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(NotificationStoreFormatBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.NotificationId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.NotificationStoreFormats.IgnoreQueryFilters().Where(e => e.NotificationId == command.NotificationId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreFormatId).ToListAsync(cancellationToken);
        var insertIds = command.StoreFormatIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.StoreFormats.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no StoreFormat with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new NotificationStoreFormat { NotificationId = command.NotificationId, StoreFormatId = id });
        _context.NotificationStoreFormats.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class NotificationStoreFormatBulkInsertValidator : AbstractValidator<NotificationStoreFormatBulkInsertCommand>
{
    public NotificationStoreFormatBulkInsertValidator()
    {
        RuleFor(e => e.NotificationId).GreaterThan(0);
        RuleFor(e => e.StoreFormatIds).NotNull();
        RuleForEach(e => e.StoreFormatIds).GreaterThan(0);
    }
}