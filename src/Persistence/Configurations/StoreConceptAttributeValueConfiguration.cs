namespace Engage.Persistence.Configurations;

public class StoreConceptAttributeValueConfiguration : IEntityTypeConfiguration<StoreConceptAttributeValue>
{
    public void Configure(EntityTypeBuilder<StoreConceptAttributeValue> builder)
    {
        builder.HasIndex(e => new { e.StoreId, e.StoreConceptAttributeId })
               .IsUnique();

        builder.Property(e => e.Value)
               .IsRequired()
               .HasMaxLength(100);
    }
}
