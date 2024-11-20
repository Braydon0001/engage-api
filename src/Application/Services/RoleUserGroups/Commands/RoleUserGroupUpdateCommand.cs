namespace Engage.Application.Services.RoleUserGroups.Commands;

public class RoleUserGroupUpdateCommand : IMapTo<RoleUserGroup>, IRequest<RoleUserGroup>
{
    public int Id { get; set; }
    public int RoleId { get; init; }
    public int UserGroupId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RoleUserGroupUpdateCommand, RoleUserGroup>();
    }
}

public record RoleUserGroupUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<RoleUserGroupUpdateCommand, RoleUserGroup>
{
    public async Task<RoleUserGroup> Handle(RoleUserGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.RoleUserGroups.SingleOrDefaultAsync(e => e.RoleUserGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateRoleUserGroupValidator : AbstractValidator<RoleUserGroupUpdateCommand>
{
    public UpdateRoleUserGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.RoleId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserGroupId).NotEmpty().GreaterThan(0);
    }
}