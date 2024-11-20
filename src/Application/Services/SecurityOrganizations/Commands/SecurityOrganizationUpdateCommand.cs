namespace Engage.Application.Services.SecurityOrganizations.Commands;

public class SecurityOrganizationUpdateCommand : IMapTo<SecurityOrganization>, IRequest<SecurityOrganization>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Slug { get; init; }
    public string ExternalId { get; set; }
    public int OwnerId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityOrganizationUpdateCommand, SecurityOrganization>();
    }
}

public record SecurityOrganizationUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityOrganizationUpdateCommand, SecurityOrganization>
{
    public async Task<SecurityOrganization> Handle(SecurityOrganizationUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SecurityOrganizations.SingleOrDefaultAsync(e => e.SecurityOrganizationId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSecurityOrganizationValidator : AbstractValidator<SecurityOrganizationUpdateCommand>
{
    public UpdateSecurityOrganizationValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Slug).NotEmpty().MaximumLength(200);
        RuleFor(e => e.OwnerId).NotEmpty().GreaterThan(0);
    }
}