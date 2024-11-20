using Azure.Storage;
using Azure.Storage.Blobs;

namespace Engage.Application.Services.NPrintingBatches.Commands;

public record NPrintingBatchProcessCommand(int NPrintingBatchId) : IRequest<OperationStatus>
{
}

public class NPrintingBatchProcessHandler : IRequestHandler<NPrintingBatchProcessCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly AzureBlobStorageOptions _options;

    public NPrintingBatchProcessHandler(IAppDbContext context, IOptions<AzureBlobStorageOptions> options)
    {
        _context = context;
        _options = options.Value;
    }

    public async Task<OperationStatus> Handle(NPrintingBatchProcessCommand command, CancellationToken cancellationToken)

    {
        var batch = await _context.NPrintingBatches.Include(e => e.WebFileCategory)
                                                   .Include(e => e.NPrintings)
                                                   .SingleOrDefaultAsync(e => e.NPrintingBatchId == command.NPrintingBatchId, cancellationToken);
        if (batch == null)
        {
            return new OperationStatus
            {
                Exception = true,
                Message = $"There is no NPrintingBatch with id: {command.NPrintingBatchId} "
            };
        }

        var webFileGroup = (WebFileGroupEnum)batch.WebFileCategory.WebFileGroupId;

        foreach (var nPrinting in batch.NPrintings)
        {
            var extension = GetFileExtension(nPrinting.FileName);
            var identifier = GetEntityIdentifier(nPrinting.FileName);
            IdentifierResult identifierResult = null;

            if (webFileGroup == WebFileGroupEnum.Employee)
            {
                identifierResult = await ValidateEmployeeIdentifierAsync(identifier, cancellationToken);
            }
            else if (webFileGroup == WebFileGroupEnum.Store)
            {
                identifierResult = await ValidateStoreIdentifierAsync(identifier, cancellationToken);
            }

            if (!string.IsNullOrWhiteSpace(identifierResult.Error))
            {
                nPrinting.Error = identifierResult.Error;
            }
            else
            {
                // Upsert webfile (Each NPrinting can only have one corresponding WebFile)
                var webFile = await _context.WebFiles.SingleOrDefaultAsync(e => e.NPrintingId == nPrinting.NPrintingId, cancellationToken);
                webFile ??= new WebFile { };

                webFile.WebFileCategoryId = batch.WebFileCategoryId;
                webFile.FileTypeId = batch.FileTypeId;
                webFile.TargetStrategyId = (int)TargetStrategyEnum.One;
                webFile.Name = batch.Report;
                webFile.DisplayName = batch.DisplayName;
                webFile.NPrintingId = nPrinting.NPrintingId;

                if (webFileGroup == WebFileGroupEnum.Employee)
                {
                    webFile.EmployeeId = identifierResult.Id;
                }
                if (webFileGroup == WebFileGroupEnum.Store)
                {
                    webFile.StoreId = identifierResult.Id;
                }

                _context.WebFiles.Add(webFile);

                // Update NPrinting
                nPrinting.ProcessedDate = DateTime.UtcNow;
                nPrinting.Error = null;

                await _context.SaveChangesAsync(cancellationToken);

                // Upload the file
                var azureFileName = $"{batch.Report}.{extension}";
                var uri = new Uri($"https://{_options.AccountName}.blob.core.windows.net/webfile/{webFile.WebFileId}/{azureFileName}");
                var client = CreateBlobClient(uri);
                var filePath = Path.Combine(batch.Directory, nPrinting.FileName);
                await client.UploadAsync(filePath, overwrite: true, cancellationToken);

                // Update WebFile
                webFile.Files = new List<JsonFile> { new JsonFile(azureFileName, uri.AbsoluteUri) };
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        return new OperationStatus
        {
            Status = true,
            OperationId = command.NPrintingBatchId
        };
    }

    private static string GetFileExtension(string fileName)
    {
        return fileName[(fileName.LastIndexOf(".") + 1)..];
    }

    private static string GetEntityIdentifier(string fileName)
    {
        var delimeterA = "~ ";
        var indexA = fileName.LastIndexOf(delimeterA);

        var delimeterB = ".";
        var indexB = fileName.LastIndexOf(delimeterB);
        var id = fileName[(indexA + delimeterA.Length)..indexB];
        return id;
    }

    private async Task<IdentifierResult> ValidateStoreIdentifierAsync(string identifier, CancellationToken cancellationToken)
    {
        if (!int.TryParse(identifier, out int intId))
        {
            return new IdentifierResult("The store identifier is not an integer.", 0);
        }

        var store = await _context.Stores.IgnoreQueryFilters()
                                         .SingleOrDefaultAsync(e => e.StoreId == intId, cancellationToken);
        if (store == null)
        {
            return new IdentifierResult("There is no store with the file name id.", 0);
        }

        return new IdentifierResult(string.Empty, intId);
    }

    private async Task<IdentifierResult> ValidateEmployeeIdentifierAsync(string identifier, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.IgnoreQueryFilters()
                                                .SingleOrDefaultAsync(e => e.Code == identifier, cancellationToken);
        if (employee == null)
        {
            return new IdentifierResult("There is no employee with the file name identifier.", 0);
        }

        return new IdentifierResult(string.Empty, employee.EmployeeId);
    }

    private BlobClient CreateBlobClient(Uri uri)
    {
        return new BlobClient(uri, new StorageSharedKeyCredential(_options.AccountName, _options.AccountKey));
    }
}

public record IdentifierResult(string Error, int Id);

public class NPrintingBatchProcessValidator : AbstractValidator<NPrintingBatchProcessCommand>
{
    public NPrintingBatchProcessValidator()
    {
        RuleFor(e => e.NPrintingBatchId).GreaterThan(0).NotEmpty();
    }
}