// auto-generated
namespace Engage.Application.Services.WebFileEmployees.Commands;

public record WebFileEmployeeBulkDeleteCommand(int WebFileId, List<int> EmployeeIds) : IRequest<int?>
{
}

public class WebFileEmployeeBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<WebFileEmployeeBulkDeleteCommand, int?>
{
    public WebFileEmployeeBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(WebFileEmployeeBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.WebFileEmployees.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);
  
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

public class WebFileEmployeeBulkDeleteValidator : AbstractValidator<WebFileEmployeeBulkDeleteCommand>
{
    public WebFileEmployeeBulkDeleteValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}
