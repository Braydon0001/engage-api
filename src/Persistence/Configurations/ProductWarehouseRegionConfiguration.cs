namespace Engage.Persistence.Configurations;

public class ProductWarehouseRegionConfiguration : IEntityTypeConfiguration<ProductWarehouseRegion>
{
    public void Configure(EntityTypeBuilder<ProductWarehouseRegion> builder)
    {
        builder.HasKey(e => new { e.ProductWarehouseId, e.EngageRegionId })
            .IsClustered(false);

        builder.HasOne(x => x.ProductWarehouse)
               .WithMany(c => c.ProductWarehouseRegions)
               .HasForeignKey(x => x.ProductWarehouseId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EngageRegion)
               .WithMany(c => c.ProductWarehouseRegions)
               .HasForeignKey(x => x.EngageRegionId)
               .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
