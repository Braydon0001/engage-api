namespace Engage.Persistence.Configurations;

public class StoreStoreConceptPerformanceConfiguration : IEntityTypeConfiguration<StoreStoreConceptPerformance>
{
    public void Configure(EntityTypeBuilder<StoreStoreConceptPerformance> builder)
    {
        builder.HasKey(e => new { e.StoreId, e.StoreConceptId });

        builder.HasOne(s => s.Store)
            .WithMany(x => x.StoreStoreConceptPerformances)
            .HasForeignKey(s => s.StoreId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(s => s.StoreConcept)
            .WithMany(x => x.StoreStoreConceptPerformances)
            .HasForeignKey(s => s.StoreConceptId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(s => s.YearMonth)
            .IsRequired();

        builder.Property(s => s.FormatTarget)
            .IsRequired();

        builder.Property(s => s.StoreSkuCount)
            .IsRequired();
    }
}
