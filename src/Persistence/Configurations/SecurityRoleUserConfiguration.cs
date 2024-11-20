namespace Engage.Persistence.Configurations;

public class SecurityRoleUserConfiguration : IEntityTypeConfiguration<SecurityRoleUser>
{
    public void Configure(EntityTypeBuilder<SecurityRoleUser> builder)
    {
        builder.Property(e => e.SecurityRoleUserId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.SecurityRoleId).IsRequired();
    }
}