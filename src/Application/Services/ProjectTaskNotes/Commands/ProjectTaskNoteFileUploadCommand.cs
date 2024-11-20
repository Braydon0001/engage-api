namespace Engage.Application.Services.ProjectTaskNotes.Commands;

public class ProjectTaskNoteFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record ProjectTaskNoteFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProjectTaskNoteFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(ProjectTaskNoteFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTaskNotes.SingleOrDefaultAsync(e => e.ProjectTaskNoteId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(ProjectTaskNote),
            EntityFiles = entity.Files,
            MaxFiles = 3
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class ProjectTaskNoteFileUploadValidator : FileUploadValidator<ProjectTaskNoteFileUploadCommand>
{
    public ProjectTaskNoteFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}