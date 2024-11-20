namespace Engage.Application.Services.SurveyForms.Commands;

public class SurveyFormFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record SurveyFormFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SurveyFormFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(SurveyFormFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SurveyForm),
            EntityFiles = entity.Files,
            MaxFiles = 5,
            OverwriteType = false
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class SurveyFormFileUploadValidator : FileUploadValidator<SurveyFormFileUploadCommand>
{
    public SurveyFormFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}