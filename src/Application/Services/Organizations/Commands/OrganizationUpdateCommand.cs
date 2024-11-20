namespace Engage.Application.Services.Organizations.Commands;

public class OrganizationUpdateCommand : IMapTo<Organization>, IRequest<Organization>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string TenantIdentifier { get; init; }
    public List<JsonSetting> Settings { get; init; }

    public string ThemeColor { get; init; }
    public string ThemeCustomColor { get; init; }
    public JsonThemeSetting JsonTheme { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrganizationUpdateCommand, Organization>();
    }
}

public record OrganizationUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganizationUpdateCommand, Organization>
{
    public async Task<Organization> Handle(OrganizationUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Organizations.SingleOrDefaultAsync(e => e.OrganizationId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateOrganizationValidator : AbstractValidator<OrganizationUpdateCommand>
{
    public UpdateOrganizationValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.TenantIdentifier).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Settings);
        RuleFor(e => e.ThemeColor);
        RuleFor(e => e.ThemeCustomColor);
        RuleFor(e => e.JsonTheme);
    }
}