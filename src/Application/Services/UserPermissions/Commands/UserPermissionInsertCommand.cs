namespace Engage.Application.Services.UserPermissions.Commands;

public class UserPermissionInsertCommand : IMapTo<UserPermission>, IRequest<UserPermission>
{
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserPermissionInsertCommand, UserPermission>();
    }
}

public record UserPermissionInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserPermissionInsertCommand, UserPermission>
{
    public async Task<UserPermission> Handle(UserPermissionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<UserPermissionInsertCommand, UserPermission>(command);
        
        Context.UserPermissions.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class UserPermissionInsertValidator : AbstractValidator<UserPermissionInsertCommand>
{
    public UserPermissionInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Key).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Description).MaximumLength(200);
    }
}