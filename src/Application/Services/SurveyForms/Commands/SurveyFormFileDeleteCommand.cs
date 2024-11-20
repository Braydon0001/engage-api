namespace Engage.Application.Services.SurveyForms.Commands;

public class SurveyFormFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record SurveyFormFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SurveyFormFileDeleteCommand, bool>
{
    public async Task<bool> Handle(SurveyFormFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(SurveyForm), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class SurveyFormFileDeleteValidator : FileDeleteValidator<SurveyFormFileDeleteCommand>
{
    public SurveyFormFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}