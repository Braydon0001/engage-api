namespace Engage.Application.Services.ProjectCampaigns.Commands;

public class ProjectCampaignFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record ProjectCampaignFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProjectCampaignFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(ProjectCampaignFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectCampaigns.SingleOrDefaultAsync(e => e.ProjectCampaignId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(ProjectCampaign),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class ProjectCampaignFileUploadValidator : FileUploadValidator<ProjectCampaignFileUploadCommand>
{
    public ProjectCampaignFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}