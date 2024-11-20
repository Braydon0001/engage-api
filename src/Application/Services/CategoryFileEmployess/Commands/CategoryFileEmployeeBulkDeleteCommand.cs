// auto-generated
namespace Engage.Application.Services.CategoryFileEmployees.Commands;

public record CategoryFileEmployeeBulkDeleteCommand(int CategoryFileId, List<int> EmployeeIds) : IRequest<int?>
{
}

public class CategoryFileEmployeeBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<CategoryFileEmployeeBulkDeleteCommand, int?>
{
    public CategoryFileEmployeeBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(CategoryFileEmployeeBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileEmployees.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

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

public class CategoryFileEmployeeBulkDeleteValidator : AbstractValidator<CategoryFileEmployeeBulkDeleteCommand>
{
    public CategoryFileEmployeeBulkDeleteValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}
