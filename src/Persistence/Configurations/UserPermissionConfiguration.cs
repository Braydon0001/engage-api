namespace Engage.Persistence.Configurations;

public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.Property(e => e.UserPermissionId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Key).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).HasMaxLength(200);
    }
}