namespace Engage.Application.Services.UserOrganizationRoles.Commands;

public class UserOrganizationRoleUpdateCommand : IMapTo<UserOrganizationRole>, IRequest<UserOrganizationRole>
{
    public int Id { get; set; }
    public int UserId { get; init; }
    public int UserOrganizationId { get; init; }
    public int UserRoleId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserOrganizationRoleUpdateCommand, UserOrganizationRole>();
    }
}

public record UserOrganizationRoleUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserOrganizationRoleUpdateCommand, UserOrganizationRole>
{
    public async Task<UserOrganizationRole> Handle(UserOrganizationRoleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.UserOrganizationRoles.SingleOrDefaultAsync(e => e.UserOrganizationRoleId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateUserOrganizationRoleValidator : AbstractValidator<UserOrganizationRoleUpdateCommand>
{
    public UpdateUserOrganizationRoleValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserOrganizationId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserRoleId).NotEmpty().GreaterThan(0);
    }
}