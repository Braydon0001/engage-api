namespace Engage.Application.Services.WebFiles.Commands;

public record WebFileDeleteCommand(int Id) : IRequest<OperationStatus>
{
}

public class WebFileDeleteHandler : BaseDeleteHandler, IRequestHandler<WebFileDeleteCommand, OperationStatus>
{
    public WebFileDeleteHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(WebFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        if (entity.Files != null)
        {
            foreach (var file in entity.Files)
            {
                await _fileStorage.DeleteAsync(new FileDeleteCommand
                {
                    Id = command.Id,
                }, nameof(WebFile), cancellationToken);
            }
        }

        _context.WebFiles.Remove(entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}