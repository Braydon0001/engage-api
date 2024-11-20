namespace Engage.Application.Services.SecurityPermissions.Commands;

public class SecurityPermissionInsertCommand : IMapTo<SecurityPermission>, IRequest<SecurityPermission>
{
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityPermissionInsertCommand, SecurityPermission>();
    }
}

public record SecurityPermissionInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SecurityPermissionInsertCommand, SecurityPermission>
{
    public async Task<SecurityPermission> Handle(SecurityPermissionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SecurityPermissionInsertCommand, SecurityPermission>(command);

        Context.SecurityPermissions.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SecurityPermissionInsertValidator : AbstractValidator<SecurityPermissionInsertCommand>
{
    public SecurityPermissionInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Key).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Description).MaximumLength(200);
    }
}