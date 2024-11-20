// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductWarehouseSummaryConfiguration : IEntityTypeConfiguration<ProductWarehouseSummary>
{
    public void Configure(EntityTypeBuilder<ProductWarehouseSummary> builder)
    {
        builder.Property(e => e.ProductWarehouseSummaryId).IsRequired();
        builder.Property(e => e.ProductId).IsRequired();
        builder.Property(e => e.ProductWarehouseId).IsRequired();
        builder.Property(e => e.ProductPeriodId).IsRequired();
        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.EngageRegionId);

        // Multi-column indexes 

        builder.HasIndex(e => new { e.ProductId, e.ProductWarehouseId, e.ProductPeriodId, e.EngageRegionId }).IsUnique();
    }
}