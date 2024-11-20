using Engage.Application.Services.OrganizationSettings.Commands;

namespace Engage.Application.Services.Organizations.Commands;

public class OrganizationSettingsUpsertCommand : IRequest<Organization>
{
    public int OrganizationId { get; init; }
    public OrganizationSettingsCommand OrganizationSetting { get; init; }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrganizationSetting, OrganizationSetting>();
            CreateMap<OrganizationSettingsCommand, OrganizationSetting>();
        }
    }
}

public record OrganizationSettingsUpsertCommandHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganizationSettingsUpsertCommand, Organization>
{
    public async Task<Organization> Handle(OrganizationSettingsUpsertCommand command, CancellationToken cancellationToken)
    {
        var existingOrganization = await Context.Organizations
            .Include(o => o.OrganizationSetting)
            .FirstOrDefaultAsync(o => o.OrganizationId == command.OrganizationId, cancellationToken);

        if (existingOrganization == null)
        {
            return null;
        }

        if (existingOrganization.OrganizationSetting == null)
        {
            // Create
            var organizationSettingsEntity = Mapper.Map<OrganizationSetting>(command.OrganizationSetting);
            Context.OrganizationSettings.Add(organizationSettingsEntity);
            await Context.SaveChangesAsync(cancellationToken);

            // Retrieve the ID of the newly created OrganizationSettings entry
            var organizationSettingsId = organizationSettingsEntity.OrganizationSettingId;

            existingOrganization.OrganizationSettingId = organizationSettingsId;
        }
        else
        {
            // Update
            command.OrganizationSetting.OrganizationSettingId = existingOrganization.OrganizationSetting.OrganizationSettingId;
            Mapper.Map(command.OrganizationSetting, existingOrganization.OrganizationSetting);
        }

        await Context.SaveChangesAsync(cancellationToken);

        return existingOrganization;
    }
}

public class OrganizationSettingsUpsertCommandValidator : AbstractValidator<OrganizationSettingsUpsertCommand>
{
    public OrganizationSettingsUpsertCommandValidator()
    {
        RuleFor(e => e.OrganizationId).NotEmpty();
        RuleFor(e => e.OrganizationSetting).NotEmpty();
    }
}