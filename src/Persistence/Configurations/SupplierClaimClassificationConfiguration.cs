namespace Engage.Persistence.Configurations;

public class SupplierClaimClassificationConfiguration : IEntityTypeConfiguration<SupplierClaimClassification>
{
    public void Configure(EntityTypeBuilder<SupplierClaimClassification> builder)
    {
        builder.HasKey(e => new { e.SupplierId, e.ClaimClassificationId })
            .IsClustered(false);

        builder.HasOne(x => x.Supplier)
            .WithMany(e => e.SupplierClaimClassifications)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.ClaimClassification)
            .WithMany(e => e.SupplierClaimClassifications)
            .HasForeignKey(x => x.ClaimClassificationId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
