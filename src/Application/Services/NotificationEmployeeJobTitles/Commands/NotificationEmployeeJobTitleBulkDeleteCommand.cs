// auto-generated
namespace Engage.Application.Services.NotificationEmployeeJobTitles.Commands;

public record NotificationEmployeeJobTitleBulkDeleteCommand(int NotificationId, List<int> EmployeeJobTitleIds) : IRequest<int?>
{
}

public class NotificationEmployeeJobTitleBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<NotificationEmployeeJobTitleBulkDeleteCommand, int?>
{
    public NotificationEmployeeJobTitleBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(NotificationEmployeeJobTitleBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.NotificationId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.NotificationEmployeeJobTitles.IgnoreQueryFilters().Where(e => e.NotificationId == command.NotificationId);
  
        // Delete Ids check
        var deleteIdsCheck = await _context.EmployeeJobTitles.Where(e => command.EmployeeJobTitleIds.Contains(e.EmployeeJobTitleId)).Select(e => e.EmployeeJobTitleId).ToListAsync(cancellationToken);
        var notFoundIds = command.EmployeeJobTitleIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no EmployeeJobTitle with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EmployeeJobTitleIds.Contains(e.EmployeeJobTitleId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class NotificationEmployeeJobTitleBulkDeleteValidator : AbstractValidator<NotificationEmployeeJobTitleBulkDeleteCommand>
{
    public NotificationEmployeeJobTitleBulkDeleteValidator()
    {
        RuleFor(e => e.NotificationId).GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleIds).NotNull();
        RuleForEach(e => e.EmployeeJobTitleIds).GreaterThan(0);
    }
}
