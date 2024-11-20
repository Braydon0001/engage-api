namespace Engage.Application.Services.EmployeeAssets.Commands;

public class EmployeeAssetDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class EmployeeDeleteFileHandler : FileDeleteHandler, IRequestHandler<EmployeeAssetDeleteFileCommand, OperationStatus>
{
    public EmployeeDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeAssetDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeAssets.SingleOrDefaultAsync(e => e.EmployeeAssetId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(EmployeeAsset), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class EmployeeDeleteFileValidator : FileDeleteValidator<EmployeeAssetDeleteFileCommand>
{
    public EmployeeDeleteFileValidator()
    {
    }
}