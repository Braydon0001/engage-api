
namespace Engage.Persistence.Configurations;

public class OrganizationSettingConfiguration : IEntityTypeConfiguration<OrganizationSetting>
{
    public void Configure(EntityTypeBuilder<OrganizationSetting> builder)
    {
        builder.Property(e => e.OrganizationSettingId).IsRequired();
        builder.Property(e => e.OrganizationTheme).HasColumnType("json");
    }
}
