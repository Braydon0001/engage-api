namespace Engage.Application.Services.UserOrganizations.Commands;

public class UserOrganizationInsertCommand : IMapTo<UserOrganization>, IRequest<UserOrganization>
{
    public string Name { get; init; }
    public int SupplierId { get; init; }
    public string ThemeName { get; init; }
    public string ThemeColor { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserOrganizationInsertCommand, UserOrganization>();
    }
}

public record UserOrganizationInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserOrganizationInsertCommand, UserOrganization>
{
    public async Task<UserOrganization> Handle(UserOrganizationInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<UserOrganizationInsertCommand, UserOrganization>(command);
        
        Context.UserOrganizations.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class UserOrganizationInsertValidator : AbstractValidator<UserOrganizationInsertCommand>
{
    public UserOrganizationInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ThemeName).MaximumLength(200);
        RuleFor(e => e.ThemeColor).MaximumLength(200);
    }
}