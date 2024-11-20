namespace Engage.Application.Services.UserRolePermissions.Commands;

public class UserRolePermissionInsertCommand : IMapTo<UserRolePermission>, IRequest<UserRolePermission>
{
    public int UserRoleId { get; init; }
    public int UserPermissionId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRolePermissionInsertCommand, UserRolePermission>();
    }
}

public record UserRolePermissionInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserRolePermissionInsertCommand, UserRolePermission>
{
    public async Task<UserRolePermission> Handle(UserRolePermissionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<UserRolePermissionInsertCommand, UserRolePermission>(command);
        
        Context.UserRolePermissions.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class UserRolePermissionInsertValidator : AbstractValidator<UserRolePermissionInsertCommand>
{
    public UserRolePermissionInsertValidator()
    {
        RuleFor(e => e.UserRoleId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserPermissionId).NotEmpty().GreaterThan(0);
    }
}