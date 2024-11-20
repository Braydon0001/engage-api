namespace Engage.Application.Services.SurveyFormAnswers.Commands;

public class SurveyFormAnswerFileUploadCommand : IRequest<Result>
{
    public string Id { get; set; }
    public IFormFile File { get; set; }
    public string FileType { get; set; }
}

public record SurveyFormAnswerFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SurveyFormAnswerFileUploadCommand, Result>
{
    public async Task<Result> Handle(SurveyFormAnswerFileUploadCommand command, CancellationToken cancellationToken)
    {
        SurveyFormAnswer entity = null;
        var entityId = Int32.TryParse(command.Id, out int n) ? n : (int?)null;
        // if the id is not a number, we might have a guid
        if (entityId == null)
        {
            //try parse the id as a guid to make sure
            var isGuid = Guid.TryParse(command.Id, out Guid g);
            if (isGuid)
            {
                entity = await Context.SurveyFormAnswers.SingleOrDefaultAsync(e => e.AnswerUuid == command.Id, cancellationToken);
            }
        }
        else
        {
            entity = await Context.SurveyFormAnswers.SingleOrDefaultAsync(e => e.SurveyFormAnswerId == entityId, cancellationToken);
        }

        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SurveyFormAnswer),
            EntityFiles = entity.Files,
            MaxFiles = 5,
            OverwriteType = false
        };
        var file = await File.UploadAsync(new FileUploadCommand()
        {
            File = command.File,
            FileType = command.FileType,
            Id = entity.SurveyFormAnswerId,
        }, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return new Result()
        {
            Id = entity.SurveyFormAnswerId,
            File = file,
        };
    }
}

public class SurveyFormAnswerFileUploadValidator : AbstractValidator<SurveyFormAnswerFileUploadCommand>
{
    public SurveyFormAnswerFileUploadValidator()
    {
        RuleFor(e => e.Id);
        RuleFor(e => e.File).NotNull();
    }
}

public class Result
{
    public int Id { get; set; }
    public JsonFile File { get; set; }
}