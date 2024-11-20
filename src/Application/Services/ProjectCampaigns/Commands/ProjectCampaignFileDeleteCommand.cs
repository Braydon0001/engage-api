namespace Engage.Application.Services.ProjectCampaigns.Commands;

public class ProjectCampaignFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record ProjectCampaignFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProjectCampaignFileDeleteCommand, bool>
{
    public async Task<bool> Handle(ProjectCampaignFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectCampaigns.SingleOrDefaultAsync(e => e.ProjectCampaignId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(ProjectCampaign), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        await Context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class ProjectCampaignFileDeleteValidator : FileDeleteValidator<ProjectCampaignFileDeleteCommand>
{
    public ProjectCampaignFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}