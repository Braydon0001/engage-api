// auto-generated
namespace Engage.Application.Services.NotificationEmployeeJobTitles.Commands;

public record NotificationEmployeeJobTitleBulkInsertCommand(int NotificationId, List<int> EmployeeJobTitleIds) : IRequest<List<int>>
{
}

public class NotificationEmployeeJobTitleBulkInsertHandler : BulkInsertHandler, IRequestHandler<NotificationEmployeeJobTitleBulkInsertCommand, List<int>>
{
    public NotificationEmployeeJobTitleBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(NotificationEmployeeJobTitleBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.NotificationId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.NotificationEmployeeJobTitles.IgnoreQueryFilters().Where(e => e.NotificationId == command.NotificationId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EmployeeJobTitleId).ToListAsync(cancellationToken);
        var insertIds = command.EmployeeJobTitleIds.Except(currentIds).ToList();
        
        // Insert Ids check
        var insertIdsCheck = await _context.EmployeeJobTitles.Where(e => insertIds.Contains(e.EmployeeJobTitleId)).Select(e => e.EmployeeJobTitleId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no EmployeeJobTitle with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new NotificationEmployeeJobTitle { NotificationId = command.NotificationId, EmployeeJobTitleId = id });
        _context.NotificationEmployeeJobTitles.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class NotificationEmployeeJobTitleBulkInsertValidator : AbstractValidator<NotificationEmployeeJobTitleBulkInsertCommand>
{
    public NotificationEmployeeJobTitleBulkInsertValidator()
    {
        RuleFor(e => e.NotificationId).GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleIds).NotNull();
        RuleForEach(e => e.EmployeeJobTitleIds).GreaterThan(0);
    }
}