namespace Engage.Persistence.Configurations;

public class SecurityRoleConfiguration : IEntityTypeConfiguration<SecurityRole>
{
    public void Configure(EntityTypeBuilder<SecurityRole> builder)
    {
        builder.Property(e => e.SecurityRoleId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Key).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).HasMaxLength(200);
    }
}