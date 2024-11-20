namespace Engage.Persistence.Configurations;

public class UserRolePermissionConfiguration : IEntityTypeConfiguration<UserRolePermission>
{
    public void Configure(EntityTypeBuilder<UserRolePermission> builder)
    {
        builder.Property(e => e.UserRolePermissionId).IsRequired();
        builder.Property(e => e.UserRoleId).IsRequired();
        builder.Property(e => e.UserPermissionId).IsRequired();
    }
}