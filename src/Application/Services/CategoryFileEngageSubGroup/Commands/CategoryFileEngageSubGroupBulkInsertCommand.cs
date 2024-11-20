// auto-generated
namespace Engage.Application.Services.CategoryFileEngageSubGroups.Commands;

public record CategoryFileEngageSubGroupBulkInsertCommand(int CategoryFileId, List<int> EngageSubGroupIds) : IRequest<List<int>>
{
}

public class CategoryFileEngageSubGroupBulkInsertHandler : BulkInsertHandler, IRequestHandler<CategoryFileEngageSubGroupBulkInsertCommand, List<int>>
{
    public CategoryFileEngageSubGroupBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(CategoryFileEngageSubGroupBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileEngageSubGroups.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EngageSubGroupId).ToListAsync(cancellationToken);
        var insertIds = command.EngageSubGroupIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.EmployeeStores.Where(e => insertIds.Contains(e.EngageSubGroupId)).Select(e => e.EngageSubGroupId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Sub Group with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new CategoryFileEngageSubGroup { CategoryFileId = command.CategoryFileId, EngageSubGroupId = id });
        _context.CategoryFileEngageSubGroups.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class CategoryFileEngageSubGroupBulkInsertValidator : AbstractValidator<CategoryFileEngageSubGroupBulkInsertCommand>
{
    public CategoryFileEngageSubGroupBulkInsertValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.EngageSubGroupIds).NotNull();
    }
}