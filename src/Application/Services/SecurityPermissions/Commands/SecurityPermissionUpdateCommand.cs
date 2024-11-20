namespace Engage.Application.Services.SecurityPermissions.Commands;

public class SecurityPermissionUpdateCommand : IMapTo<SecurityPermission>, IRequest<SecurityPermission>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityPermissionUpdateCommand, SecurityPermission>();
    }
}

public record SecurityPermissionUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityPermissionUpdateCommand, SecurityPermission>
{
    public async Task<SecurityPermission> Handle(SecurityPermissionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SecurityPermissions.SingleOrDefaultAsync(e => e.SecurityPermissionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSecurityPermissionValidator : AbstractValidator<SecurityPermissionUpdateCommand>
{
    public UpdateSecurityPermissionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Key).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Description).MaximumLength(200);
    }
}