namespace Engage.Application.Services.ProjectNotes.Commands;

public class ProjectNoteFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record ProjectNoteFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProjectNoteFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(ProjectNoteFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectNotes.SingleOrDefaultAsync(e => e.ProjectNoteId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(ProjectNote),
            EntityFiles = entity.Files,
            MaxFiles = 3
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class ProjectNoteFileUploadValidator : FileUploadValidator<ProjectNoteFileUploadCommand>
{
    public ProjectNoteFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}