namespace Engage.Application.Services.TrainingFiles.Commands;

public class TrainingFileFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
    public int? TrainingId { get; set; }
}

public class TrainingFileFileUploadHandler : FileUploadHandler, IRequestHandler<TrainingFileFileUploadCommand, JsonFile>
{
    private readonly IMediator _mediator;
    public TrainingFileFileUploadHandler(IAppDbContext context, IFileService file, IMediator mediator) : base(context, file)
    {
        _mediator = mediator;
    }

    public async Task<JsonFile> Handle(TrainingFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var training = await _context.Trainings.SingleOrDefaultAsync(e => e.TrainingId == command.TrainingId, cancellationToken);
        var trainingFileType = await _context.TrainingFileTypes.IgnoreQueryFilters().SingleOrDefaultAsync(e => e.Name.ToLower() == command.FileType.ToLower(), cancellationToken);
        if (training == null)
        {
            return null;
        }

        if (trainingFileType == null)
        {
            return null;
        }

        var entity = await _mediator.Send(new TrainingFileInsertCommand
        {
            TrainingId = training.TrainingId,
            TrainingFileTypeId = trainingFileType.TrainingFileTypeId,
        }, cancellationToken);

        var options = new FileUploadOptions
        {
            ContainerName = nameof(TrainingFile),
            EntityFiles = entity.Files,
            MaxFiles = 100,
            OverwriteType = false,
        };

        command.Id = entity.TrainingFileId;
        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class TrainingFileFileUploadValidator : FileUploadValidator<TrainingFileFileUploadCommand>
{
    public TrainingFileFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}