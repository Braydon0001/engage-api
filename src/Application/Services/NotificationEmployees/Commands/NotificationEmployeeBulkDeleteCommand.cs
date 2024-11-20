// auto-generated
namespace Engage.Application.Services.NotificationEmployees.Commands;

public record NotificationEmployeeBulkDeleteCommand(int NotificationId, List<int> EmployeeIds) : IRequest<int?>
{
}

public class NotificationEmployeeBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<NotificationEmployeeBulkDeleteCommand, int?>
{
    public NotificationEmployeeBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(NotificationEmployeeBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.NotificationId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.NotificationEmployees.IgnoreQueryFilters().Where(e => e.NotificationId == command.NotificationId);
  
        // Delete Ids check
        var deleteIdsCheck = await _context.Employees.Where(e => command.EmployeeIds.Contains(e.EmployeeId)).Select(e => e.EmployeeId).ToListAsync(cancellationToken);
        var notFoundIds = command.EmployeeIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Employee with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EmployeeIds.Contains(e.EmployeeId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class NotificationEmployeeBulkDeleteValidator : AbstractValidator<NotificationEmployeeBulkDeleteCommand>
{
    public NotificationEmployeeBulkDeleteValidator()
    {
        RuleFor(e => e.NotificationId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}
