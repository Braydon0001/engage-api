namespace Engage.Application.Services.Trainings.Commands;

public class TrainingUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class TrainingUploadFileHandler : FileUploadHandler, IRequestHandler<TrainingUploadFileCommand, OperationStatus>
{
    public TrainingUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(TrainingUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Trainings.SingleOrDefaultAsync(e => e.TrainingId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(Training),
            EntityFiles = entity.Files,
            MaxFiles = 100,
            OverwriteType = false,
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        var files = entity.Files.AddFile(file);

        entity.Files = files;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class TrainingUploadFileValidator : FileUploadValidator<TrainingUploadFileCommand>
{
    public TrainingUploadFileValidator()
    {
    }
}