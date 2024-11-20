namespace Engage.Application.Services.NPrintingBatches.Commands;

public record NPrintingBatchStageCommand(int WebFileCategoryId, int FileTypeId, string Report, string DisplayName) : IRequest<OperationStatus>
{
}

public class NPrintingBatchStageHandler : IRequestHandler<NPrintingBatchStageCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly AzureBlobStorageOptions _options;

    public NPrintingBatchStageHandler(IAppDbContext context, IOptions<AzureBlobStorageOptions> options)
    {
        _context = context;
        _options = options.Value;
    }

    public async Task<OperationStatus> Handle(NPrintingBatchStageCommand command, CancellationToken cancellationToken)
    {
        var directory = $"{_options.NPrintingBaseDirectory}{command.Report}";
        if (!Directory.Exists(directory))
        {
            return new OperationStatus
            {
                Exception = true,
                Message = $"There is no {command.Report} directory"
            };
        }

        // Add NPrintingBatch  
        var nPrintingBatch = new NPrintingBatch
        {
            Directory = directory,
            WebFileCategoryId = command.WebFileCategoryId,
            FileTypeId = command.FileTypeId,
            Report = command.Report,
            DisplayName = command.DisplayName
        };
        _context.NPrintingBatches.Add(nPrintingBatch);

        var batchOperationStatus = await _context.SaveChangesAsync(cancellationToken);
        if (batchOperationStatus.Exception)
        {
            return batchOperationStatus;
        }

        // Add NPrinting's
        var nPrintings = new List<NPrinting>();
        foreach (var filePath in Directory.GetFiles(directory))
        {
            var fileInfo = new FileInfo(filePath);
            nPrintings.Add(new NPrinting
            {
                NPrintingBatchId = nPrintingBatch.NPrintingBatchId,
                FileName = fileInfo.Name,
            });
        }
        _context.NPrintings.AddRange(nPrintings);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        if (operationStatus.Exception)
        {
            return operationStatus;
        }

        return new OperationStatus(nPrintingBatch.NPrintingBatchId);

    }
}

public class NPrintingBatchStageValidator : AbstractValidator<NPrintingBatchStageCommand>
{
    public NPrintingBatchStageValidator()
    {
        RuleFor(e => e.WebFileCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.FileTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Report).NotEmpty().MaximumLength(100);
        RuleFor(e => e.DisplayName).NotEmpty().MaximumLength(100);
    }
}