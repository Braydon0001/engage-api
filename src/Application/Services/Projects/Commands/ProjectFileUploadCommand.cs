namespace Engage.Application.Services.Projects.Commands;

public class ProjectFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record ProjectFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProjectFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(ProjectFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Projects.SingleOrDefaultAsync(e => e.ProjectId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(Project),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class ProjectFileUploadValidator : FileUploadValidator<ProjectFileUploadCommand>
{
    public ProjectFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}