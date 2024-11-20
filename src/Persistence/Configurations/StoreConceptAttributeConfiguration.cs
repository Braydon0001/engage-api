namespace Engage.Persistence.Configurations;

public class StoreConceptAttributeConfiguration : IEntityTypeConfiguration<StoreConceptAttribute>
{
    public void Configure(EntityTypeBuilder<StoreConceptAttribute> builder)
    {
        builder.Property(e => e.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.Description)
               .HasMaxLength(100);
    }
}