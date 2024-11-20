namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record SurveyFormQuestionGroupFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SurveyFormQuestionGroupFileDeleteCommand, bool>
{
    public async Task<bool> Handle(SurveyFormQuestionGroupFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestionGroups.SingleOrDefaultAsync(e => e.SurveyFormQuestionGroupId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(SurveyFormQuestionGroup), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class SurveyFormQuestionGroupFileDeleteValidator : FileDeleteValidator<SurveyFormQuestionGroupFileDeleteCommand>
{
    public SurveyFormQuestionGroupFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}