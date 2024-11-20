namespace Engage.Persistence.Configurations;

public class SecurityOrganizationConfiguration : IEntityTypeConfiguration<SecurityOrganization>
{
    public void Configure(EntityTypeBuilder<SecurityOrganization> builder)
    {
        builder.Property(e => e.SecurityOrganizationId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Slug).IsRequired().HasMaxLength(200);
        builder.Property(e => e.ExternalId).HasMaxLength(200);
        builder.Property(e => e.OwnerId).IsRequired();
    }
}