// auto-generated
namespace Engage.Application.Services.CategoryFileCategoryGroups.Commands;

public record CategoryFileCategoryGroupBulkDeleteCommand(int CategoryFileId, List<int> CategoryGroupIds) : IRequest<int?>
{
}

public class CategoryFileCategoryGroupBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<CategoryFileCategoryGroupBulkDeleteCommand, int?>
{
    public CategoryFileCategoryGroupBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(CategoryFileCategoryGroupBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileCategoryGroups.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

        // Delete Ids check
        var deleteIdsCheck = await _context.CategoryFileCategoryGroups.Where(e => command.CategoryGroupIds.Contains(e.CategoryGroupId)).Select(e => e.CategoryGroupId).ToListAsync(cancellationToken);
        var notFoundIds = command.CategoryGroupIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Group with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.CategoryGroupIds.Contains(e.CategoryGroupId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class CategoryFileCategoryGroupBulkDeleteValidator : AbstractValidator<CategoryFileCategoryGroupBulkDeleteCommand>
{
    public CategoryFileCategoryGroupBulkDeleteValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.CategoryGroupIds).NotNull();

    }
}
