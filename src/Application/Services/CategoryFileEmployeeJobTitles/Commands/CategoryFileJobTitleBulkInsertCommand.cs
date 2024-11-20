// auto-generated
namespace Engage.Application.Services.CategoryFileEmployeeJobTitles.Commands;

public record CategoryFileEmployeeJobTitleBulkInsertCommand(int CategoryFileId, List<int> EmployeeJobTitleIds) : IRequest<List<int>>
{
}

public class CategoryFileEmployeeJobTitleBulkInsertHandler : BulkInsertHandler, IRequestHandler<CategoryFileEmployeeJobTitleBulkInsertCommand, List<int>>
{
    public CategoryFileEmployeeJobTitleBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(CategoryFileEmployeeJobTitleBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileEmployeeJobTitles.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

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
        var entities = insertIds.Select(id => new CategoryFileEmployeeJobTitle { CategoryFileId = command.CategoryFileId, EmployeeJobTitleId = id });
        _context.CategoryFileEmployeeJobTitles.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class CategoryFileEmployeeJobTitleBulkInsertValidator : AbstractValidator<CategoryFileEmployeeJobTitleBulkInsertCommand>
{
    public CategoryFileEmployeeJobTitleBulkInsertValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleIds).NotNull();
        RuleForEach(e => e.EmployeeJobTitleIds).GreaterThan(0);
    }
}