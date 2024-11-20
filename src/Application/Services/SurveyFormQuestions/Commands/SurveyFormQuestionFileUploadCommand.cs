namespace Engage.Application.Services.SurveyFormQuestions.Commands;

public class SurveyFormQuestionFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record SurveyFormQuestionFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SurveyFormQuestionFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(SurveyFormQuestionFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestions.SingleOrDefaultAsync(e => e.SurveyFormQuestionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SurveyFormQuestion),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class SurveyFormQuestionFileUploadValidator : FileUploadValidator<SurveyFormQuestionFileUploadCommand>
{
    public SurveyFormQuestionFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}