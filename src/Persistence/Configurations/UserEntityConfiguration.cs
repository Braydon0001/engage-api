namespace Engage.Persistence.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasIndex(e => e.Entity)
               .IsUnique();

        builder.Property(e => e.Entity)
               .IsRequired()
               .HasMaxLength(120);
    }
}
