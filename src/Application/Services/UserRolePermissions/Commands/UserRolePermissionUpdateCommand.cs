namespace Engage.Application.Services.UserRolePermissions.Commands;

public class UserRolePermissionUpdateCommand : IMapTo<UserRolePermission>, IRequest<UserRolePermission>
{
    public int Id { get; set; }
    public int UserRoleId { get; init; }
    public int UserPermissionId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRolePermissionUpdateCommand, UserRolePermission>();
    }
}

public record UserRolePermissionUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserRolePermissionUpdateCommand, UserRolePermission>
{
    public async Task<UserRolePermission> Handle(UserRolePermissionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.UserRolePermissions.SingleOrDefaultAsync(e => e.UserRolePermissionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateUserRolePermissionValidator : AbstractValidator<UserRolePermissionUpdateCommand>
{
    public UpdateUserRolePermissionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserRoleId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserPermissionId).NotEmpty().GreaterThan(0);
    }
}