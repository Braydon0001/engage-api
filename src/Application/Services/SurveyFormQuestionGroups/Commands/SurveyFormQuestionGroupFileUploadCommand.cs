namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record SurveyFormQuestionGroupFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SurveyFormQuestionGroupFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(SurveyFormQuestionGroupFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestionGroups.SingleOrDefaultAsync(e => e.SurveyFormQuestionGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SurveyFormQuestionGroup),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class SurveyFormQuestionGroupFileUploadValidator : FileUploadValidator<SurveyFormQuestionGroupFileUploadCommand>
{
    public SurveyFormQuestionGroupFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}