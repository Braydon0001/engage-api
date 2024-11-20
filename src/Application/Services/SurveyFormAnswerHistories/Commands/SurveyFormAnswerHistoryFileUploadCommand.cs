using Engage.Application.Services.SurveyFormAnswers.Commands;

namespace Engage.Application.Services.SurveyFormAnswerHistories.Commands;

public class SurveyFormAnswerHistoryFileUploadCommand : IRequest<Result>
{
    public int Id { get; set; }
    public IFormFile File { get; set; }
    public string FileType { get; set; }
}

public record SurveyFormAnswerHistoryFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SurveyFormAnswerHistoryFileUploadCommand, Result>
{
    public async Task<Result> Handle(SurveyFormAnswerHistoryFileUploadCommand command, CancellationToken cancellationToken)
    {
        var historyItem = await Context.SurveyFormAnswerHistories.SingleOrDefaultAsync(e => e.SurveyFormAnswerHistoryId == command.Id, cancellationToken);

        if (historyItem == null)
        {
            return null;
        }

        var entity = await Context.SurveyFormAnswers.SingleOrDefaultAsync(e => e.SurveyFormAnswerId == historyItem.SurveyFormAnswerId, cancellationToken);

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
