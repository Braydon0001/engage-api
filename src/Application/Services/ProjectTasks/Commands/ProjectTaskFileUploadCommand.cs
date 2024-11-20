namespace Engage.Application.Services.ProjectTasks.Commands;

public class ProjectTaskFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record ProjectTaskFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProjectTaskFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(ProjectTaskFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTasks.SingleOrDefaultAsync(e => e.ProjectTaskId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(ProjectTask),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class ProjectTaskFileUploadValidator : FileUploadValidator<ProjectTaskFileUploadCommand>
{
    public ProjectTaskFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}