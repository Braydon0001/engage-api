namespace Engage.Application.Services.DCProducts.Commands;

public class DCProductDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class DCProductDeleteFileHandler : FileDeleteHandler, IRequestHandler<DCProductDeleteFileCommand, OperationStatus>
{
    public DCProductDeleteFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(DCProductDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.DCProducts.SingleOrDefaultAsync(e => e.DCProductId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(DCProduct), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class DCProductDeleteFileValidator : FileDeleteValidator<DCProductDeleteFileCommand>
{
    public DCProductDeleteFileValidator()
    {
    }
}