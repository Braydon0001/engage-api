namespace Engage.Application.Services.ProductFilters.Commands;

//public record ProductFilterUploadRecord(string Barcode, string Filter);
public class ProductFilterUploadRecord
{
    public string Barcode { get; set; }
    public string Filter { get; set; }
}

public class UploadProductFiltersCommand : IRequest<OperationStatus>
{
    public IFormFile File { get; set; }
}

public class UploadProductFiltersCommandHandler : IRequestHandler<UploadProductFiltersCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IOptions<ImportFileOptions> _importFileOptions;
    private readonly ICsvService _csv;
    private readonly IExcelService _excel;

    public UploadProductFiltersCommandHandler(IAppDbContext context, IMediator mediator, IOptions<ImportFileOptions> importFileOptions, ICsvService csvService, IExcelService excel)
    {
        _context = context;
        _mediator = mediator;
        _importFileOptions = importFileOptions;
        _csv = csvService;
        _excel = excel;
    }

    public async Task<OperationStatus> Handle(UploadProductFiltersCommand request, CancellationToken cancellationToken)
    {
        var readFileOptions = new ReadFileOptions
        {
            File = request.File,
            Folder = _importFileOptions.Value.ProductFiltersFolder,
            MaxRows = _importFileOptions.Value.ProductFiltersMaxRows,
            DoFileUpload = true
        };

        //var readFileResult = await _csv.ReadFile<ProductFilterUploadRecord>(readFileOptions, cancellationToken);
        var readFileResult = await _excel.ReadFile<ProductFilterUploadRecord>(readFileOptions, cancellationToken);

        var imports = await CreateProductFilterImports(readFileResult, _context, cancellationToken);
        _context.ProductFilterUploads.AddRange(imports);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = readFileResult.FileUploadId;
        operationStatus.ReturnObject = new FileUploadResult<Domain.Entities.ProductFilterUpload>(imports, readFileResult.FileName);
        return operationStatus;
    }

    private static async Task<IEnumerable<ProductFilterUpload>> CreateProductFilterImports(ReadFileResult<ProductFilterUploadRecord> readFileResult, IAppDbContext context, CancellationToken cancellationToken)
    {
        var imports = new List<Domain.Entities.ProductFilterUpload>();
        var rowNo = 1;

        var barCodes = readFileResult.Data.Where(e => !string.IsNullOrWhiteSpace(e.Barcode))
                                          .Select(e => e.Barcode).ToList();

        var products = await context.EngageVariantProducts.IgnoreQueryFilters()
                                                          .Where(e => barCodes.Contains(e.UnitBarcode))
                                                          .ToListAsync(cancellationToken);

        foreach (var row in readFileResult.Data)
        {
            var import = new ProductFilterUpload
            {
                FileUploadId = readFileResult.FileUploadId,
                RowNo = rowNo++,
                RowType = RowType.Success,
                Barcode = row.Barcode,
                Filter = row.Filter,
            };

            var product = products.FirstOrDefault(e => e.UnitBarcode == row.Barcode);
            if (product != null)
            {
                import.EngageVariantProductId = product.EngageVariantProductId;
                import.EngageVariantProductName = product.Name;
            }

            if (string.IsNullOrWhiteSpace(row.Barcode))
            {
                import.RowType = RowType.Error;
                import.RowMessage = "The Barcode column can't be empty";
            }
            else if (string.IsNullOrWhiteSpace(row.Filter))
            {
                import.RowType = RowType.Error;
                import.RowMessage = "The Filter column can't be empty";
            }
            else if (product == null)
            {
                import.RowType = RowType.Warning;
                import.RowMessage = $"There are no variant products with barcode: {row.Barcode}";
            }
            else if (product.Deleted == true)
            {
                import.RowType = RowType.Warning;
                import.RowMessage = "The variant product is deleted";
            }
            else if (product.Disabled == true)
            {
                import.RowType = RowType.Warning;
                import.RowMessage = "The variant product is disabled";
            }

            imports.Add(import);
        }

        return imports;
    }
}
