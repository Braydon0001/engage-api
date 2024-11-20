namespace Engage.Application.Services.SurveyFormQuestions.Commands;

public class SurveyFormQuestionFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record SurveyFormQuestionFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SurveyFormQuestionFileDeleteCommand, bool>
{
    public async Task<bool> Handle(SurveyFormQuestionFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestions.SingleOrDefaultAsync(e => e.SurveyFormQuestionId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(SurveyFormQuestion), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class SurveyFormQuestionFileDeleteValidator : FileDeleteValidator<SurveyFormQuestionFileDeleteCommand>
{
    public SurveyFormQuestionFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}