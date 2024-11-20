namespace Engage.Application.Services.SecurityPermissions.Commands;

public class SecurityPermissionRolesCreateCommand : IRequest<OperationStatus>
{
    public List<int> SecurityPermissionIds { get; set; }
    public int RoleId { get; set; }
    public bool Save { get; set; } = true;
}
public record SecurityPermissionRolesCreateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityPermissionRolesCreateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SecurityPermissionRolesCreateCommand command, CancellationToken cancellationToken)
    {
        var currentPermissions = await Context.SecurityPermissionRoles
                                              .Where(e => e.SecurityRoleId == command.RoleId)
                                              .ToListAsync(cancellationToken);

        if (currentPermissions.Any())
        {
            Context.SecurityPermissionRoles.RemoveRange(currentPermissions);
        }

        foreach (var permissionid in command.SecurityPermissionIds)
        {
            Context.SecurityPermissionRoles.Add(new SecurityPermissionRole
            {
                SecurityRoleId = command.RoleId,
                SecurityPermissionId = permissionid
            });
        }

        if (command.Save)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            return new();
        }
    }
}