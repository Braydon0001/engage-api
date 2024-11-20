// auto-generated
namespace Engage.Application.Services.CategoryFileEmployees.Commands;

public record CategoryFileEmployeeBulkInsertCommand(int CategoryFileId, List<int> EmployeeIds) : IRequest<List<int>>
{
}

public class CategoryFileEmployeeBulkInsertHandler : BulkInsertHandler, IRequestHandler<CategoryFileEmployeeBulkInsertCommand, List<int>>
{
    public CategoryFileEmployeeBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(CategoryFileEmployeeBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileEmployees.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

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
        var entities = insertIds.Select(id => new CategoryFileEmployee { CategoryFileId = command.CategoryFileId, EmployeeId = id });
        _context.CategoryFileEmployees.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class CategoryFileEmployeeBulkInsertValidator : AbstractValidator<CategoryFileEmployeeBulkInsertCommand>
{
    public CategoryFileEmployeeBulkInsertValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}