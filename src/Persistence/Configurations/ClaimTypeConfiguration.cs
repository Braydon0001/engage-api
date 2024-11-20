namespace Engage.Persistence.Configurations;

public class ClaimTypeConfiguration : IEntityTypeConfiguration<ClaimType>
{
    public void Configure(EntityTypeBuilder<ClaimType> builder)
    {
        builder.Property(e => e.Name)
               .HasMaxLength(100)
               .IsRequired();
    }
}
