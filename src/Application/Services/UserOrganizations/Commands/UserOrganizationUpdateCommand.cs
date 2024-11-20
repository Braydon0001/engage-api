namespace Engage.Application.Services.UserOrganizations.Commands;

public class UserOrganizationUpdateCommand : IMapTo<UserOrganization>, IRequest<UserOrganization>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public int SupplierId { get; init; }
    public string ThemeName { get; init; }
    public string ThemeColor { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserOrganizationUpdateCommand, UserOrganization>();
    }
}

public record UserOrganizationUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserOrganizationUpdateCommand, UserOrganization>
{
    public async Task<UserOrganization> Handle(UserOrganizationUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.UserOrganizations.SingleOrDefaultAsync(e => e.UserOrganizationId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateUserOrganizationValidator : AbstractValidator<UserOrganizationUpdateCommand>
{
    public UpdateUserOrganizationValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ThemeName).MaximumLength(200);
        RuleFor(e => e.ThemeColor).MaximumLength(200);
    }
}