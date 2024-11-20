namespace Engage.Application.Services.UserRoles.Commands;

public class UserRoleInsertCommand : IMapTo<UserRole>, IRequest<UserRole>
{
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRoleInsertCommand, UserRole>();
    }
}

public record UserRoleInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserRoleInsertCommand, UserRole>
{
    public async Task<UserRole> Handle(UserRoleInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<UserRoleInsertCommand, UserRole>(command);
        
        Context.UserRoles.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class UserRoleInsertValidator : AbstractValidator<UserRoleInsertCommand>
{
    public UserRoleInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Key).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Description).MaximumLength(200);
    }
}