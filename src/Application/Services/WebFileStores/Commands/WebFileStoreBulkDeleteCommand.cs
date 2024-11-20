// auto-generated
namespace Engage.Application.Services.WebFileStores.Commands;

public record WebFileStoreBulkDeleteCommand(int WebFileId, List<int> StoreIds) : IRequest<int?>
{
}

public class WebFileStoreBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<WebFileStoreBulkDeleteCommand, int?>
{
    public WebFileStoreBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(WebFileStoreBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.WebFileStores.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);
  
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

public class WebFileStoreBulkDeleteValidator : AbstractValidator<WebFileStoreBulkDeleteCommand>
{
    public WebFileStoreBulkDeleteValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}
