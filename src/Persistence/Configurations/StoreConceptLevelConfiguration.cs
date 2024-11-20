namespace Engage.Persistence.Configurations;

public class StoreConceptLevelConfiguration : IEntityTypeConfiguration<StoreConceptLevel>
{
    public void Configure(EntityTypeBuilder<StoreConceptLevel> builder)
    {
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.Concepts).HasColumnType("json");
        builder.Property(e => e.BlobUrl).HasMaxLength(1000);
        builder.Property(e => e.BlobName).HasMaxLength(1000);

        // Indexes
        builder.HasIndex(e => new { e.StoreId, e.StoreConceptId }).IsUnique();
    }
}
