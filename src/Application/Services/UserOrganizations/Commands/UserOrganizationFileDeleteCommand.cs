namespace Engage.Application.Services.UserOrganizations.Commands;

public class UserOrganizationFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record UserOrganizationFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<UserOrganizationFileDeleteCommand, bool>
{
    public async Task<bool> Handle(UserOrganizationFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.UserOrganizations.SingleOrDefaultAsync(e => e.UserOrganizationId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(UserOrganization), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class UserOrganizationFileDeleteValidator : FileDeleteValidator<UserOrganizationFileDeleteCommand>
{
    public UserOrganizationFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}