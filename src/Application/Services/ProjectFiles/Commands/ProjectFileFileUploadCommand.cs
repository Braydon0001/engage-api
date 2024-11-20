namespace Engage.Application.Services.ProjectFiles.Commands;

public class ProjectFileFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
    public int? ProjectId { get; set; }
}

public class ProjectFileFileUploadHandler : FileUploadHandler, IRequestHandler<ProjectFileFileUploadCommand, JsonFile>
{
    private readonly IMediator _mediator;
    public ProjectFileFileUploadHandler(IAppDbContext context, IFileService file, IMediator mediator) : base(context, file)
    {
        _mediator = mediator;
    }

    public async Task<JsonFile> Handle(ProjectFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var Project = await _context.Projects.SingleOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken);
        var ProjectFileType = await _context.ProjectFileTypes.SingleOrDefaultAsync(e => e.Name.ToLower() == command.FileType.ToLower(), cancellationToken);
        if (Project == null)
        {
            return null;
        }

        if (ProjectFileType == null)
        {
            return null;
        }

        var entity = await _mediator.Send(new ProjectFileInsertCommand
        {
            ProjectId = Project.ProjectId,
            ProjectFileTypeId = ProjectFileType.ProjectFileTypeId,
        }, cancellationToken);

        var options = new FileUploadOptions
        {
            ContainerName = nameof(ProjectFile),
            EntityFiles = entity.Files,
            MaxFiles = 100,
            OverwriteType = false,
        };

        command.Id = entity.ProjectFileId;
        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class ProjectFileFileUploadValidator : FileUploadValidator<ProjectFileFileUploadCommand>
{
    public ProjectFileFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}