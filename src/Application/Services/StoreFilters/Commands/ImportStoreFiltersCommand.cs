using Engage.Application.Services.FileUploads.Commands;

namespace Engage.Application.Services.StoreFilters.Commands;

public class ImportStoreFiltersCommand : IRequest<OperationStatus>
{
    public int FileUploadId { get; set; }
    public bool Overwrite { get; set; }
}

public class ImportStoreFiltersCommandHandler : IRequestHandler<ImportStoreFiltersCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public ImportStoreFiltersCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(ImportStoreFiltersCommand request, CancellationToken cancellationToken)
    {
        var imports = await _context.StoreFilterUploads.Where(e => e.FileUploadId == request.FileUploadId)
                                                       .ToListAsync(cancellationToken);
        FileUploadUtils.ValidateUpload(imports);

        if (request.Overwrite == true)
        {
            var filters = imports.Select(e => e.Filter)
                                 .Distinct()
                                 .ToList();

            var storeFilters = await _context.StoreFilters.Where(e => e.Filter.Contains(e.Filter))
                                                          .ToListAsync(cancellationToken);
            _context.StoreFilters.RemoveRange(storeFilters);
        }

        var sql = "INSERT INTO StoreFilters (StoreId, Filter, FileUploadId, Disabled, Deleted, Created) " +
                  "SELECT DISTINCT StoreId, Filter, FileUploadId, 0, 0, NOW() FROM StoreFilterUploads WHERE RowType = 1 AND FileUploadId = {0} " +
                  "AND (StoreId, Filter) NOT IN (SELECT StoreId, Filter FROM StoreFilters);" +
                  "SELECT * FROM StoreFilters";
        await _context.StoreFilters.FromSqlRaw(sql, request.FileUploadId)
                                   .IgnoreQueryFilters()
                                   .ToListAsync(cancellationToken);

        return await _mediator.Send(new UpdateFileUploadImportDateCommand
        {
            Id = request.FileUploadId
        }, cancellationToken);
    }
}
