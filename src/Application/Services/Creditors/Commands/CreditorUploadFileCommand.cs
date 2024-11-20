namespace Engage.Application.Services.Creditors.Commands;

public class CreditorUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class CreditorUploadFileHandler : FileUploadHandler, IRequestHandler<CreditorUploadFileCommand, OperationStatus>
{
    public CreditorUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(CreditorUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Creditors.SingleOrDefaultAsync(e => e.CreditorId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(Creditor),
            EntityFiles = entity.Files,
            MaxFiles = 5
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class CreditorUploadFileValidator : FileUploadValidator<CreditorUploadFileCommand>
{
    public CreditorUploadFileValidator()
    {
    }
}