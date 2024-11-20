namespace Engage.Application.Services.CategoryFileCategoryGroups.Commands;

public record CategoryFileCategoryGroupBulkInsertCommand(int CategoryFileId, List<int> CategoryGroupIds) : IRequest<List<int>>
{
}

public class CategoryFileCategoryGroupBulkInserHandler : BulkInsertHandler, IRequestHandler<CategoryFileCategoryGroupBulkInsertCommand, List<int>>
{
    public CategoryFileCategoryGroupBulkInserHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(CategoryFileCategoryGroupBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileCategoryGroups.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.CategoryGroupId).ToListAsync(cancellationToken);
        var insertIds = command.CategoryGroupIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.CategoryGroups.Where(e => insertIds.Contains(e.CategoryGroupId)).Select(e => e.CategoryGroupId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Group with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new CategoryFileCategoryGroup { CategoryFileId = command.CategoryFileId, CategoryGroupId = id });
        _context.CategoryFileCategoryGroups.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }

}
public class CategoryFileCategoryGroupBulkInsertValidator : AbstractValidator<CategoryFileCategoryGroupBulkInsertCommand>
{
    public CategoryFileCategoryGroupBulkInsertValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.CategoryGroupIds).NotNull();

    }
}

