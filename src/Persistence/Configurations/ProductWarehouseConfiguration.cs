namespace Engage.Persistence.Configurations;

public class ProductWarehouseConfiguration : IEntityTypeConfiguration<ProductWarehouse>
{
    public void Configure(EntityTypeBuilder<ProductWarehouse> builder)
    {
        builder.Property(e => e.ProductWarehouseId).IsRequired();
        builder.Property(e => e.EngageRegionId);
        builder.Property(e => e.ParentId);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}