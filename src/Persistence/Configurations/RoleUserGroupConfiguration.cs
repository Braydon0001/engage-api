namespace Engage.Persistence.Configurations;

public class RoleUserGroupConfiguration : IEntityTypeConfiguration<RoleUserGroup>
{
    public void Configure(EntityTypeBuilder<RoleUserGroup> builder)
    {
        builder.Property(e => e.RoleUserGroupId).IsRequired();
        builder.Property(e => e.RoleId).IsRequired();
        builder.Property(e => e.UserGroupId).IsRequired();
        builder.HasIndex(e => new { e.RoleId, e.UserGroupId }).IsUnique();
    }
}