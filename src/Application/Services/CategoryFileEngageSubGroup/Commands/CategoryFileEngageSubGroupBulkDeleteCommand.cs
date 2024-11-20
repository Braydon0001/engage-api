// auto-generated
namespace Engage.Application.Services.CategoryFileEngageSubGroups.Commands;

public record CategoryFileEngageSubGroupBulkDeleteCommand(int CategoryFileId, List<int> EngageSubGroupIds) : IRequest<int?>
{
}

public class CategoryFileEngageSubGroupBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<CategoryFileEngageSubGroupBulkDeleteCommand, int?>
{
    public CategoryFileEngageSubGroupBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(CategoryFileEngageSubGroupBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileEngageSubGroups.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

        // Delete Ids check
        var deleteIdsCheck = await _context.CategoryFileEngageSubGroups.Where(e => command.EngageSubGroupIds.Contains(e.EngageSubGroupId)).Select(e => e.EngageSubGroupId).ToListAsync(cancellationToken);
        var notFoundIds = command.EngageSubGroupIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Sub groups with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EngageSubGroupIds.Contains(e.EngageSubGroupId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class CategoryFileEngageSubGroupBulkDeleteValidator : AbstractValidator<CategoryFileEngageSubGroupBulkDeleteCommand>
{
    public CategoryFileEngageSubGroupBulkDeleteValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.EngageSubGroupIds).NotNull();
    }
}
