using Engage.Application.Services.SecurityPermissions.Commands;

namespace Engage.Application.Services.SecurityRoles.Commands;

public class SecurityRoleUpdateCommand : IMapTo<SecurityRole>, IRequest<SecurityRole>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public List<int> SecurityPermissionIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityRoleUpdateCommand, SecurityRole>();
    }
}

public record SecurityRoleUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SecurityRoleUpdateCommand, SecurityRole>
{
    public async Task<SecurityRole> Handle(SecurityRoleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SecurityRoles.Include(e => e.SecurityPermissionRoles).SingleOrDefaultAsync(e => e.SecurityRoleId == command.Id, cancellationToken);

        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        var permissionIdSave = await Mediator.Send(new SecurityPermissionRolesCreateCommand
        {
            RoleId = entity.SecurityRoleId,
            SecurityPermissionIds = command.SecurityPermissionIds,
            Save = false
        }, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSecurityRoleValidator : AbstractValidator<SecurityRoleUpdateCommand>
{
    public UpdateSecurityRoleValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Key).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Description).MaximumLength(200);
    }
}