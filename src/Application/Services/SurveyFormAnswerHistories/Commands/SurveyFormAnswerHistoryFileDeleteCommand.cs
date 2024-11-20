
namespace Engage.Application.Services.SurveyFormAnswerHistories.Commands;

public class SurveyFormAnswerHistoryFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record SurveyFormAnswerHistoryFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SurveyFormAnswerHistoryFileDeleteCommand, bool>
{
    public async Task<bool> Handle(SurveyFormAnswerHistoryFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var answerHistory = await Context.SurveyFormAnswerHistories
                                         .SingleOrDefaultAsync(e => e.SurveyFormAnswerHistoryId == command.Id, cancellationToken);

        if (answerHistory == null)
        {
            return false;
        }

        var surveyAnswer = await Context.SurveyFormAnswers.SingleOrDefaultAsync(e => e.SurveyFormAnswerId == answerHistory.SurveyFormAnswerId, cancellationToken);

        var file = surveyAnswer.Files.Where(e => e.Name.ToLower() == command.FileName.ToLower()).FirstOrDefault();

        if (file == null)
        {
            return false;
        }

        answerHistory.Files = answerHistory.Files.AddFile(file);

        surveyAnswer.Files = surveyAnswer.Files.RemoveFile(command);

        await Context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class SurveyFormAnswerHistoryFileDeleteValidator : FileDeleteValidator<SurveyFormAnswerHistoryFileDeleteCommand>
{
    public SurveyFormAnswerHistoryFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}