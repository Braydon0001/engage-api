namespace Engage.Persistence.Configurations;

public class SecurityPermissionRoleConfiguration : IEntityTypeConfiguration<SecurityPermissionRole>
{
    public void Configure(EntityTypeBuilder<SecurityPermissionRole> builder)
    {
        builder.Property(e => e.SecurityPermissionRoleId).IsRequired();
        builder.Property(e => e.SecurityRoleId).IsRequired();
        builder.Property(e => e.SecurityPermissionId).IsRequired();
    }
}