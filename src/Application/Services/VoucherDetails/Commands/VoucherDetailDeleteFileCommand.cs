namespace Engage.Application.Services.VoucherDetails.Commands;

public class VoucherDetailDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class VoucherDetailDeleteFileHandler : FileDeleteHandler, IRequestHandler<VoucherDetailDeleteFileCommand, OperationStatus>
{
    public VoucherDetailDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(VoucherDetailDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.VoucherDetails.SingleOrDefaultAsync(e => e.VoucherDetailId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(VoucherDetail), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class VoucherDetailDeleteFileValidator : FileDeleteValidator<VoucherDetailDeleteFileCommand>
{
    public VoucherDetailDeleteFileValidator()
    {
    }
}