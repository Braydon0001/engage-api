namespace Engage.Application.Services.ProjectComments.Commands;

public class ProjectCommentFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
    public int? EntityId { get; set; }
}

public record ProjectCommentFileUploadHandler(IAppDbContext Context, IFileService File, IMediator Mediator) : IRequestHandler<ProjectCommentFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(ProjectCommentFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectComments.SingleOrDefaultAsync(e => command.EntityId.HasValue ? e.ProjectCommentId == command.EntityId : e.ProjectCommentId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(ProjectComment),
            EntityFiles = entity.Files,
            MaxFiles = 5,
            OverwriteType = false
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var opStaus = await Context.SaveChangesAsync(cancellationToken);
        //if (opStaus.Status)
        //{
        //    await Mediator.Send(new ProjectCommentSendCommunicationCommand { ProjectCommentId = entity.ProjectCommentId, HasFiles = true });
        //}

        return file;
    }
}

public class ProjectCommentFileUploadValidator : FileUploadValidator<ProjectCommentFileUploadCommand>
{
    public ProjectCommentFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}