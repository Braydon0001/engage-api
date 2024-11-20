namespace Engage.Application.Services.SecurityRoleUsers.Commands;

public class SecurityRoleUserUpdateUserRolesCommand : IRequest<OperationStatus>
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public bool Save { get; set; } = true;
}
public record SecurityRoleUserUpdateUserHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityRoleUserUpdateUserRolesCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SecurityRoleUserUpdateUserRolesCommand command, CancellationToken cancellationToken)
    {
        var previousRole = await Context.SecurityRoleUsers.Where(e => e.UserId == command.UserId).FirstOrDefaultAsync(cancellationToken);

        if (previousRole != null)
        {
            Context.SecurityRoleUsers.Remove(previousRole);
        }

        Context.SecurityRoleUsers.Add(new SecurityRoleUser
        {
            UserId = command.UserId,
            SecurityRoleId = command.RoleId,
        });

        if (command.Save)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        return new();
    }
}
