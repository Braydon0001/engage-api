// auto-generated
namespace Engage.Application.Services.CategoryFileStores.Commands;

public record CategoryFileStoreBulkDeleteCommand(int CategoryFileId, List<int> StoreIds) : IRequest<int?>
{
}

public class CategoryFileStoreBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<CategoryFileStoreBulkDeleteCommand, int?>
{
    public CategoryFileStoreBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(CategoryFileStoreBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileStores.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

        // Delete Ids check
        var deleteIdsCheck = await _context.Stores.Where(e => command.StoreIds.Contains(e.StoreId)).Select(e => e.StoreId).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Store with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreIds.Contains(e.StoreId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class CategoryFileStoreBulkDeleteValidator : AbstractValidator<CategoryFileStoreBulkDeleteCommand>
{
    public CategoryFileStoreBulkDeleteValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}
