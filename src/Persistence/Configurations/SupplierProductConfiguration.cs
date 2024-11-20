namespace Engage.Persistence.Configurations;

public class SupplierProductConfiguration : IEntityTypeConfiguration<SupplierProduct>
{
    public void Configure(EntityTypeBuilder<SupplierProduct> builder)
    {
        builder.HasKey(e => new { e.SupplierId, e.EngageMasterProductId })
            .IsClustered(false);

        builder.HasOne(x => x.Supplier)
            .WithMany(c => c.SupplierProducts)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EngageMasterProduct)
            .WithMany(c => c.SupplierProducts)
            .HasForeignKey(x => x.EngageMasterProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
