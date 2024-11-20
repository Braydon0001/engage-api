namespace Engage.Application.Services.SurveyFormAnswers.Commands;

public class SurveyFormAnswerFileDeleteWebCommand : FileDeleteCommand, IRequest<bool>
{
}

public record SurveyFormAnswerFileDeleteWebHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SurveyFormAnswerFileDeleteWebCommand, bool>
{

    public async Task<bool> Handle(SurveyFormAnswerFileDeleteWebCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormAnswers.SingleOrDefaultAsync(e => e.SurveyFormAnswerId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(SurveyFormAnswer), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        await Context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class SurveyFormAnswerFileDeleteWebValidator : FileDeleteValidator<SurveyFormAnswerFileDeleteWebCommand>
{
    public SurveyFormAnswerFileDeleteWebValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}