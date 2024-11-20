namespace Engage.Application.Services.RoleUserGroups.Commands;

public class RoleUserGroupInsertCommand : IMapTo<RoleUserGroup>, IRequest<RoleUserGroup>
{
    public int RoleId { get; init; }
    public int UserGroupId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RoleUserGroupInsertCommand, RoleUserGroup>();
    }
}

public record RoleUserGroupInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<RoleUserGroupInsertCommand, RoleUserGroup>
{
    public async Task<RoleUserGroup> Handle(RoleUserGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<RoleUserGroupInsertCommand, RoleUserGroup>(command);
        
        Context.RoleUserGroups.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class RoleUserGroupInsertValidator : AbstractValidator<RoleUserGroupInsertCommand>
{
    public RoleUserGroupInsertValidator()
    {
        RuleFor(e => e.RoleId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserGroupId).NotEmpty().GreaterThan(0);
    }
}