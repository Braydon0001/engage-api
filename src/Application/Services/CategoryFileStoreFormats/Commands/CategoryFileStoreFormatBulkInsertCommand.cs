// auto-generated
namespace Engage.Application.Services.CategoryFileStoreFormats.Commands;

public record CategoryFileStoreFormatBulkInsertCommand(int CategoryFileId, List<int> StoreFormatIds) : IRequest<List<int>>
{
}

public class CategoryFileStoreFormatBulkInsertHandler : BulkInsertHandler, IRequestHandler<CategoryFileStoreFormatBulkInsertCommand, List<int>>
{
    public CategoryFileStoreFormatBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(CategoryFileStoreFormatBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileStoreFormats.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreFormatId).ToListAsync(cancellationToken);
        var insertIds = command.StoreFormatIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.StoreFormats.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no StoreFormat with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new CategoryFileStoreFormat { CategoryFileId = command.CategoryFileId, StoreFormatId = id });
        _context.CategoryFileStoreFormats.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class CategoryFileStoreFormatBulkInsertValidator : AbstractValidator<CategoryFileStoreFormatBulkInsertCommand>
{
    public CategoryFileStoreFormatBulkInsertValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.StoreFormatIds).NotNull();
        RuleForEach(e => e.StoreFormatIds).GreaterThan(0);
    }
}