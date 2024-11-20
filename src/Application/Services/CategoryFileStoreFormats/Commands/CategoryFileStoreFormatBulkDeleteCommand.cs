// auto-generated
namespace Engage.Application.Services.CategoryFileStoreFormats.Commands;

public record CategoryFileStoreFormatBulkDeleteCommand(int CategoryFileId, List<int> StoreFormatIds) : IRequest<int?>
{
}

public class CategoryFileStoreFormatBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<CategoryFileStoreFormatBulkDeleteCommand, int?>
{
    public CategoryFileStoreFormatBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(CategoryFileStoreFormatBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileStoreFormats.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

        // Delete Ids check
        var deleteIdsCheck = await _context.StoreFormats.Where(e => command.StoreFormatIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreFormatIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no StoreFormat with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreFormatIds.Contains(e.StoreFormatId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class CategoryFileStoreFormatBulkDeleteValidator : AbstractValidator<CategoryFileStoreFormatBulkDeleteCommand>
{
    public CategoryFileStoreFormatBulkDeleteValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.StoreFormatIds).NotNull();
        RuleForEach(e => e.StoreFormatIds).GreaterThan(0);
    }
}
