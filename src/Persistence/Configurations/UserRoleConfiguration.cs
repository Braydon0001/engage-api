namespace Engage.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.Property(e => e.UserRoleId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Key).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).HasMaxLength(200);
    }
}