namespace Engage.Application.Services.UserPermissions.Commands;

public class UserPermissionUpdateCommand : IMapTo<UserPermission>, IRequest<UserPermission>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserPermissionUpdateCommand, UserPermission>();
    }
}

public record UserPermissionUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserPermissionUpdateCommand, UserPermission>
{
    public async Task<UserPermission> Handle(UserPermissionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.UserPermissions.SingleOrDefaultAsync(e => e.UserPermissionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateUserPermissionValidator : AbstractValidator<UserPermissionUpdateCommand>
{
    public UpdateUserPermissionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Key).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Description).MaximumLength(200);
    }
}