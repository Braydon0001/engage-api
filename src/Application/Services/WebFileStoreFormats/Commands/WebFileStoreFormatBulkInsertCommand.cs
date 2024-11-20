// auto-generated
namespace Engage.Application.Services.WebFileStoreFormats.Commands;

public record WebFileStoreFormatBulkInsertCommand(int WebFileId, List<int> StoreFormatIds) : IRequest<List<int>>
{
}

public class WebFileStoreFormatBulkInsertHandler : BulkInsertHandler, IRequestHandler<WebFileStoreFormatBulkInsertCommand, List<int>>
{
    public WebFileStoreFormatBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(WebFileStoreFormatBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.WebFileStoreFormats.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);

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
        var entities = insertIds.Select(id => new WebFileStoreFormat { WebFileId = command.WebFileId, StoreFormatId = id });
        _context.WebFileStoreFormats.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class WebFileStoreFormatBulkInsertValidator : AbstractValidator<WebFileStoreFormatBulkInsertCommand>
{
    public WebFileStoreFormatBulkInsertValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.StoreFormatIds).NotNull();
        RuleForEach(e => e.StoreFormatIds).GreaterThan(0);
    }
}