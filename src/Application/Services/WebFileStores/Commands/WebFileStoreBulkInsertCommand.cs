// auto-generated
namespace Engage.Application.Services.WebFileStores.Commands;

public record WebFileStoreBulkInsertCommand(int WebFileId, List<int> StoreIds) : IRequest<List<int>>
{
}

public class WebFileStoreBulkInsertHandler : BulkInsertHandler, IRequestHandler<WebFileStoreBulkInsertCommand, List<int>>
{
    public WebFileStoreBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(WebFileStoreBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.WebFileStores.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);

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
        var entities = insertIds.Select(id => new WebFileStore { WebFileId = command.WebFileId, StoreId = id });
        _context.WebFileStores.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class WebFileStoreBulkInsertValidator : AbstractValidator<WebFileStoreBulkInsertCommand>
{
    public WebFileStoreBulkInsertValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}