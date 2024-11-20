// auto-generated
namespace Engage.Application.Services.CategoryFileStores.Commands;

public record CategoryFileStoreBulkInsertCommand(int CategoryFileId, List<int> StoreIds) : IRequest<List<int>>
{
}

public class CategoryFileStoreBulkInsertHandler : BulkInsertHandler, IRequestHandler<CategoryFileStoreBulkInsertCommand, List<int>>
{
    public CategoryFileStoreBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(CategoryFileStoreBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileStores.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreId).ToListAsync(cancellationToken);
        var insertIds = command.StoreIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.Stores.Where(e => insertIds.Contains(e.StoreId)).Select(e => e.StoreId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Store with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new CategoryFileStore { CategoryFileId = command.CategoryFileId, StoreId = id });
        _context.CategoryFileStores.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class CategoryFileStoreBulkInsertValidator : AbstractValidator<CategoryFileStoreBulkInsertCommand>
{
    public CategoryFileStoreBulkInsertValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}