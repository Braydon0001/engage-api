namespace Engage.Persistence.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.Property(e => e.OrganizationId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.TenantIdentifier).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Settings).HasColumnType("json");
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.JsonTheme).HasColumnType("json");
    }
}