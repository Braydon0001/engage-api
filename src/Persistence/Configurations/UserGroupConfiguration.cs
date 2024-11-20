namespace Engage.Persistence.Configurations;

public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.Property(e => e.Name)
               .HasMaxLength(120)
               .IsRequired();

        builder.Property(e => e.Description)
                .HasMaxLength(1000);
    }
}
