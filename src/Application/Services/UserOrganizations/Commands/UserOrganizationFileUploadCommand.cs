namespace Engage.Application.Services.UserOrganizations.Commands;

public class UserOrganizationFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record UserOrganizationFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<UserOrganizationFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(UserOrganizationFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.UserOrganizations.SingleOrDefaultAsync(e => e.UserOrganizationId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(UserOrganization),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class UserOrganizationFileUploadValidator : FileUploadValidator<UserOrganizationFileUploadCommand>
{
    public UserOrganizationFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}