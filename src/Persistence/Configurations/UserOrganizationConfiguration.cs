namespace Engage.Persistence.Configurations;

public class UserOrganizationConfiguration : IEntityTypeConfiguration<UserOrganization>
{
    public void Configure(EntityTypeBuilder<UserOrganization> builder)
    {
        builder.Property(e => e.UserOrganizationId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.SupplierId).IsRequired();
        builder.Property(e => e.ThemeName).HasMaxLength(200);
        builder.Property(e => e.ThemeColor).HasMaxLength(200);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}