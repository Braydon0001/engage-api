namespace Engage.Application.Services.UserOrganizationRoles.Commands;

public class UserOrganizationRoleInsertCommand : IMapTo<UserOrganizationRole>, IRequest<UserOrganizationRole>
{
    public int UserId { get; init; }
    public int UserOrganizationId { get; init; }
    public int UserRoleId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserOrganizationRoleInsertCommand, UserOrganizationRole>();
    }
}

public record UserOrganizationRoleInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserOrganizationRoleInsertCommand, UserOrganizationRole>
{
    public async Task<UserOrganizationRole> Handle(UserOrganizationRoleInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<UserOrganizationRoleInsertCommand, UserOrganizationRole>(command);
        
        Context.UserOrganizationRoles.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class UserOrganizationRoleInsertValidator : AbstractValidator<UserOrganizationRoleInsertCommand>
{
    public UserOrganizationRoleInsertValidator()
    {
        RuleFor(e => e.UserId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserOrganizationId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserRoleId).NotEmpty().GreaterThan(0);
    }
}