// auto-generated
namespace Engage.Application.Services.WebFileStoreFormats.Commands;

public record WebFileStoreFormatBulkDeleteCommand(int WebFileId, List<int> StoreFormatIds) : IRequest<int?>
{
}

public class WebFileStoreFormatBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<WebFileStoreFormatBulkDeleteCommand, int?>
{
    public WebFileStoreFormatBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(WebFileStoreFormatBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.WebFileStoreFormats.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);

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

public class WebFileStoreFormatBulkDeleteValidator : AbstractValidator<WebFileStoreFormatBulkDeleteCommand>
{
    public WebFileStoreFormatBulkDeleteValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.StoreFormatIds).NotNull();
        RuleForEach(e => e.StoreFormatIds).GreaterThan(0);
    }
}
