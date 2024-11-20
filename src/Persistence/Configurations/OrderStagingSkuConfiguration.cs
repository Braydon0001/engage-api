namespace Engage.Persistence.Configurations;

public class OrderStagingSkuConfiguration : IEntityTypeConfiguration<OrderStagingSku>
{
    public void Configure(EntityTypeBuilder<OrderStagingSku> builder)
    {
        builder.Property(e => e.OrderStagingSkuId).IsRequired();
        builder.Property(e => e.OrderStagingId).IsRequired();
        builder.Property(e => e.ProductName).HasMaxLength(120);
        builder.Property(e => e.Barcode).HasMaxLength(120);
        builder.Property(e => e.UnitType).HasMaxLength(120);
        builder.Property(e => e.Quantity);
    }
}