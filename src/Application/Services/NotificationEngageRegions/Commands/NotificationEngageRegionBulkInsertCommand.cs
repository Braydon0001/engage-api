// auto-generated
namespace Engage.Application.Services.NotificationEngageRegions.Commands;

public record NotificationEngageRegionBulkInsertCommand(int NotificationId, List<int> EngageRegionIds) : IRequest<List<int>>
{
}

public class NotificationEngageRegionBulkInsertHandler : BulkInsertHandler, IRequestHandler<NotificationEngageRegionBulkInsertCommand, List<int>>
{
    public NotificationEngageRegionBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(NotificationEngageRegionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.NotificationId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.NotificationEngageRegions.IgnoreQueryFilters().Where(e => e.NotificationId == command.NotificationId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EngageRegionId).ToListAsync(cancellationToken);
        var insertIds = command.EngageRegionIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.EngageRegions.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no EngageRegion with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new NotificationEngageRegion { NotificationId = command.NotificationId, EngageRegionId = id });
        _context.NotificationEngageRegions.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class NotificationEngageRegionBulkInsertValidator : AbstractValidator<NotificationEngageRegionBulkInsertCommand>
{
    public NotificationEngageRegionBulkInsertValidator()
    {
        RuleFor(e => e.NotificationId).GreaterThan(0);
        RuleFor(e => e.EngageRegionIds).NotNull();
        RuleForEach(e => e.EngageRegionIds).GreaterThan(0);
    }
}