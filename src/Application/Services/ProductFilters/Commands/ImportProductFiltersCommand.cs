using Engage.Application.Services.FileUploads.Commands;

namespace Engage.Application.Services.ProductFilters.Commands;

public class ImportProductFiltersCommand : IRequest<OperationStatus>
{
    public int FileUploadId { get; set; }

    public bool Overwrite { get; set; }
}

public class ImportProductFiltersCommandHandler : IRequestHandler<ImportProductFiltersCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public ImportProductFiltersCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(ImportProductFiltersCommand request, CancellationToken cancellationToken)
    {
        var imports = await _context.ProductFilterUploads.Where(e => e.FileUploadId == request.FileUploadId)
                                                         .ToListAsync(cancellationToken);
        FileUploadUtils.ValidateUpload(imports);

        if (request.Overwrite == true)
        {
            var filters = imports.Select(e => e.Filter)
                                 .Distinct()
                                 .ToList();

            var productFilters = await _context.ProductFilters.Where(e => e.Filter.Contains(e.Filter))
                                                              .ToListAsync(cancellationToken);
            _context.ProductFilters.RemoveRange(productFilters);
        }

        var sql = "INSERT INTO ProductFilters (EngageVariantProductId, Barcode, Filter, FileUploadId, Disabled, Deleted, Created) " +
                  "SELECT DISTINCT EngageVariantProductId, Barcode, Filter, FileUploadId, 0, 0, NOW() FROM ProductFilterUploads WHERE RowType <> 2 AND FileUploadId = {0} " +
                  "AND (Barcode, Filter) NOT IN (SELECT Barcode, Filter FROM ProductFilters);" +
                  "SELECT * FROM ProductFilters";
        await _context.ProductFilters.FromSqlRaw(sql, request.FileUploadId)
                                     .IgnoreQueryFilters()
                                     .ToListAsync(cancellationToken);

        return await _mediator.Send(new UpdateFileUploadImportDateCommand
        {
            Id = request.FileUploadId
        }, cancellationToken);
    }
}
