namespace Engage.Application.Services.StoreFilters.Commands;

//public record StoreFilterUploadRecord(int StoreId, string Filter);
public class StoreFilterUploadRecord {
    public int StoreId { get; set; }
    public string Filter { get; set; }
}
//(int StoreId, string Filter);

public class UploadStoreFiltersCommand : IRequest<OperationStatus>
{
    public IFormFile File { get; set; }
}

public class UploadStoreFiltersCommandHandler : IRequestHandler<UploadStoreFiltersCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IOptions<ImportFileOptions> _importFileOptions;
    private readonly ICsvService _csv;
    private readonly IExcelService _excel;

    public UploadStoreFiltersCommandHandler(IAppDbContext context, IMediator mediator, IOptions<ImportFileOptions> importFileOptions, ICsvService csvService, IExcelService excel)
    {
        _context = context;
        _mediator = mediator;
        _importFileOptions = importFileOptions;
        _csv = csvService;
        _excel = excel;
    }

    public async Task<OperationStatus> Handle(UploadStoreFiltersCommand request, CancellationToken cancellationToken)
    {
        // Fixed
        var readFileOptions = new ReadFileOptions
        {
            File = request.File,
            Folder = _importFileOptions.Value.StoreFiltersFolder,
            MaxRows = _importFileOptions.Value.StoreFiltersMaxRows,
            DoFileUpload = true
        };

        // Varying 
        //var readFileResult = await _csv.ReadFile<StoreFilterUploadRecord>(readFileOptions, cancellationToken);
        var readFileResult = await _excel.ReadFile<StoreFilterUploadRecord>(readFileOptions, cancellationToken);

        // Varing
        var imports = await CreateStoreFilterImports(readFileResult, _context, cancellationToken);
        _context.StoreFilterUploads.AddRange(imports);

        // Fixed
        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = readFileResult.FileUploadId;
        operationStatus.ReturnObject = new FileUploadResult<Domain.Entities.StoreFilterUpload>(imports, readFileResult.FileName);
        return operationStatus;
    }

    private static async Task<IEnumerable<StoreFilterUpload>> CreateStoreFilterImports(ReadFileResult<StoreFilterUploadRecord> readFileResult, IAppDbContext context, CancellationToken cancellationToken)
    {
        var imports = new List<StoreFilterUpload>();
        var rowNo = 1;

        var storeIds = readFileResult.Data.Select(e => e.StoreId).ToList();

        var stores = await context.Stores.IgnoreQueryFilters()
                                         .Where(e => storeIds.Contains(e.StoreId))
                                         .ToListAsync(cancellationToken);

        foreach (var row in readFileResult.Data)
        {
            var import = new StoreFilterUpload
            {
                FileUploadId = readFileResult.FileUploadId,
                RowNo = rowNo++,
                RowType = RowType.Success,
                StoreId = row.StoreId,
                Filter = row.Filter,
            };

            var store = stores.FirstOrDefault(e => e.StoreId == row.StoreId);
            if (store != null)
            {
                import.StoreName = store.Name;
            }

            if (row.StoreId <= 0)
            {
                import.RowType = RowType.Error;
                import.RowMessage = "The StoreId column must be greater than zero";
            }
            else if (string.IsNullOrWhiteSpace(row.Filter))
            {
                import.RowType = RowType.Error;
                import.RowMessage = "The Filter column can't be empty";
            }
            else if (store == null)
            {
                import.RowType = RowType.Error;
                import.RowMessage = $"There are no stores with id: {row.StoreId}";
            }
            else if (store.Deleted == true)
            {
                import.RowType = RowType.Warning;
                import.RowMessage = "The store is deleted";
            }
            else if (store.Disabled == true)
            {
                import.RowType = RowType.Warning;
                import.RowMessage = "The store is disabled";
            }

            imports.Add(import);
        }

        return imports;
    }
}
