using Engage.Application.Services.SecurityPermissions.Commands;

namespace Engage.Application.Services.SecurityRoles.Commands;

public class SecurityRoleInsertCommand : IMapTo<SecurityRole>, IRequest<SecurityRole>
{
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public List<int> SecurityPermissionIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityRoleInsertCommand, SecurityRole>();
    }
}

public record SecurityRoleInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SecurityRoleInsertCommand, SecurityRole>
{
    public async Task<SecurityRole> Handle(SecurityRoleInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SecurityRoleInsertCommand, SecurityRole>(command);

        Context.SecurityRoles.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        var permissionIdSave = await Mediator.Send(new SecurityPermissionRolesCreateCommand
        {
            RoleId = entity.SecurityRoleId,
            SecurityPermissionIds = command.SecurityPermissionIds
        }, cancellationToken);

        return entity;
    }
}

public class SecurityRoleInsertValidator : AbstractValidator<SecurityRoleInsertCommand>
{
    public SecurityRoleInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Key).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Description).MaximumLength(200);
    }
}