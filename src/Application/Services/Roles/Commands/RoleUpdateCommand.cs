namespace Engage.Application.Services.Roles.Commands;

public class RoleUpdateCommand : IMapTo<Role>, IRequest<Role>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public List<int> UserGroupIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RoleUpdateCommand, Role>();
    }
}

public record RoleUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<RoleUpdateCommand, Role>
{
    public async Task<Role> Handle(RoleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Roles.Include(e => e.RoleUserGroups).SingleOrDefaultAsync(e => e.RoleId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        //add the userGroups
        if (command.UserGroupIds != null)
        {
            entity.RoleUserGroups.Clear();
            foreach (var userGroupId in command.UserGroupIds)
            {
                var userGroup = await Context.UserGroups.IgnoreQueryFilters().SingleOrDefaultAsync(e => e.UserGroupId == userGroupId, cancellationToken);
                if (userGroup != null)
                {
                    entity.RoleUserGroups.Add(new RoleUserGroup { Role = entity, UserGroup = userGroup });
                }
            }
        }

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateRoleValidator : AbstractValidator<RoleUpdateCommand>
{
    public UpdateRoleValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Description).MaximumLength(200);
    }
}