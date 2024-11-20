namespace Engage.Application.Services.Creditors.Commands;

public class CreditorDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class CreditorDeleteFileHandler : FileDeleteHandler, IRequestHandler<CreditorDeleteFileCommand, OperationStatus>
{
    public CreditorDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(CreditorDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Creditors.SingleOrDefaultAsync(e => e.CreditorId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(Creditor), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class CreditorDeleteFileValidator : FileDeleteValidator<CreditorDeleteFileCommand>
{
    public CreditorDeleteFileValidator()
    {
    }
}