namespace Engage.Application.Services.Roles.Commands;

public class RoleInsertCommand : IMapTo<Role>, IRequest<Role>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public List<int> UserGroupIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RoleInsertCommand, Role>();
    }
}

public record RoleInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<RoleInsertCommand, Role>
{
    public async Task<Role> Handle(RoleInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<RoleInsertCommand, Role>(command);

        Context.Roles.Add(entity);

        //add each of the userGroups
        if (command.UserGroupIds != null)
        {
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

        return entity;
    }
}

public class RoleInsertValidator : AbstractValidator<RoleInsertCommand>
{
    public RoleInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Description).MaximumLength(200);
    }
}