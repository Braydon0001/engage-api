namespace Engage.Application.Services.Trainings.Commands;

public class TrainingDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class TrainingDeleteFileHandler : FileDeleteHandler, IRequestHandler<TrainingDeleteFileCommand, OperationStatus>
{
    public TrainingDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(TrainingDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Trainings.SingleOrDefaultAsync(e => e.TrainingId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(Training), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class TrainingDeleteFileValidator : FileDeleteValidator<TrainingDeleteFileCommand>
{
    public TrainingDeleteFileValidator()
    {
    }
}