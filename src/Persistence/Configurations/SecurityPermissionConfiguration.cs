namespace Engage.Persistence.Configurations;

public class SecurityPermissionConfiguration : IEntityTypeConfiguration<SecurityPermission>
{
    public void Configure(EntityTypeBuilder<SecurityPermission> builder)
    {
        builder.Property(e => e.SecurityPermissionId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Key).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).HasMaxLength(200);
    }
}