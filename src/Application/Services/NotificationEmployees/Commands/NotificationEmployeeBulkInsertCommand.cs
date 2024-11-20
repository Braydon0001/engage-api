// auto-generated
namespace Engage.Application.Services.NotificationEmployees.Commands;

public record NotificationEmployeeBulkInsertCommand(int NotificationId, List<int> EmployeeIds) : IRequest<List<int>>
{
}

public class NotificationEmployeeBulkInsertHandler : BulkInsertHandler, IRequestHandler<NotificationEmployeeBulkInsertCommand, List<int>>
{
    public NotificationEmployeeBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(NotificationEmployeeBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.NotificationId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.NotificationEmployees.IgnoreQueryFilters().Where(e => e.NotificationId == command.NotificationId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EmployeeId).ToListAsync(cancellationToken);
        var insertIds = command.EmployeeIds.Except(currentIds).ToList();
        
        // Insert Ids check
        var insertIdsCheck = await _context.Employees.Where(e => insertIds.Contains(e.EmployeeId)).Select(e => e.EmployeeId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Employee with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new NotificationEmployee { NotificationId = command.NotificationId, EmployeeId = id });
        _context.NotificationEmployees.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class NotificationEmployeeBulkInsertValidator : AbstractValidator<NotificationEmployeeBulkInsertCommand>
{
    public NotificationEmployeeBulkInsertValidator()
    {
        RuleFor(e => e.NotificationId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}