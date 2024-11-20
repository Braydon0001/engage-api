namespace Engage.Persistence.Configurations;

public class UserOrganizationRoleConfiguration : IEntityTypeConfiguration<UserOrganizationRole>
{
    public void Configure(EntityTypeBuilder<UserOrganizationRole> builder)
    {
        builder.Property(e => e.UserOrganizationRoleId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.UserOrganizationId).IsRequired();
        builder.Property(e => e.UserRoleId).IsRequired();
    }
}