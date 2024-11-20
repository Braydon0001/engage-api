namespace Engage.Application.Services.Organizations.Commands;

public class OrganizationInsertCommand : IMapTo<Organization>, IRequest<Organization>
{
    public string Name { get; init; }
    public string TenantIdentifier { get; init; }
    public List<JsonSetting> Settings { get; init; }

    public string ThemeColor { get; init; }
    public string ThemeCustomColor { get; init; }
    public JsonThemeSetting JsonTheme { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrganizationInsertCommand, Organization>();
    }
}

public record OrganizationInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganizationInsertCommand, Organization>
{
    public async Task<Organization> Handle(OrganizationInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<OrganizationInsertCommand, Organization>(command);

        Context.Organizations.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrganizationInsertValidator : AbstractValidator<OrganizationInsertCommand>
{
    public OrganizationInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.TenantIdentifier).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Settings);
        RuleFor(e => e.ThemeColor);
        RuleFor(e => e.ThemeCustomColor);
        RuleFor(e => e.JsonTheme);
    }
}