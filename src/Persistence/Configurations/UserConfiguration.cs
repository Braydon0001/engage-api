namespace Engage.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(e => e.Email).IsUnique();
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(120);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(120);
        builder.Property(e => e.FullName).HasComputedColumnSql("concat(FirstName,' ',LastName)");
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
        builder.Property(e => e.MobilePhone).HasMaxLength(30);
        builder.Property(e => e.ExternalId).HasMaxLength(120);
        builder.Property(e => e.Settings).HasColumnType("json");
    }
}
