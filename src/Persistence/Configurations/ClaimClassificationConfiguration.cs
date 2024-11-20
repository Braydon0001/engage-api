namespace Engage.Persistence.Configurations;

public class ClaimClassificationConfiguration : IEntityTypeConfiguration<ClaimClassification>
{
    public void Configure(EntityTypeBuilder<ClaimClassification> builder)
    {
        builder.Property(e => e.Name)
               .HasMaxLength(100)
               .IsRequired();
    }
}
