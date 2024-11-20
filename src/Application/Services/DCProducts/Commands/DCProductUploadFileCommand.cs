namespace Engage.Application.Services.DCProducts.Commands;

public class DCProductUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class DCProductUploadFileHandler : FileUploadHandler, IRequestHandler<DCProductUploadFileCommand, OperationStatus>
{
    public DCProductUploadFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(DCProductUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.DCProducts.SingleOrDefaultAsync(e => e.DCProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(DCProduct),
            EntityFiles = entity.Files,
            MaxFiles = 1,
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class DCProductUploadFileValidator : FileUploadValidator<DCProductUploadFileCommand>
{
    public DCProductUploadFileValidator()
    {
    }
}