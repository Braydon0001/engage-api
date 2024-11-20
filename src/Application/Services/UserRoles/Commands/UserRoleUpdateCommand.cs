namespace Engage.Application.Services.UserRoles.Commands;

public class UserRoleUpdateCommand : IMapTo<UserRole>, IRequest<UserRole>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRoleUpdateCommand, UserRole>();
    }
}

public record UserRoleUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserRoleUpdateCommand, UserRole>
{
    public async Task<UserRole> Handle(UserRoleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.UserRoles.SingleOrDefaultAsync(e => e.UserRoleId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateUserRoleValidator : AbstractValidator<UserRoleUpdateCommand>
{
    public UpdateUserRoleValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Key).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Description).MaximumLength(200);
    }
}