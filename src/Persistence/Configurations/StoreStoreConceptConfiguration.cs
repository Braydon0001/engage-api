namespace Engage.Persistence.Configurations;

public class StoreStoreConceptConfiguration : IEntityTypeConfiguration<StoreStoreConcept>
{
    public void Configure(EntityTypeBuilder<StoreStoreConcept> builder)
    {
        builder.HasKey(e => new { e.StoreId, e.StoreConceptId })
               .IsClustered(false);

        builder.Property(e => e.ImageUrl)
               .HasMaxLength(1000);
    }
}
