namespace Engage.Application.Services.VoucherDetails.Commands;

public class VoucherDetailUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class VoucherDetailUploadFileHandler : FileUploadHandler, IRequestHandler<VoucherDetailUploadFileCommand, OperationStatus>
{
    public VoucherDetailUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(VoucherDetailUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.VoucherDetails.SingleOrDefaultAsync(e => e.VoucherDetailId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(VoucherDetail),
            EntityFiles = entity.Files,
            MaxFiles = 3
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class VoucherDetailUploadFileValidator : FileUploadValidator<VoucherDetailUploadFileCommand>
{
    public VoucherDetailUploadFileValidator()
    {
    }
}