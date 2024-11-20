namespace Engage.Persistence.Configurations;

public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
{
    public void Configure(EntityTypeBuilder<ProductOrder> builder)
    {
        builder.Property(e => e.ProductOrderId).IsRequired();
        builder.Property(e => e.OrderNumber).HasMaxLength(200);
        builder.Property(e => e.ProductOrderStatusId).IsRequired();
        builder.Property(e => e.ProductWarehouseId).IsRequired();
        builder.Property(e => e.ProductWarehouseOutId);
        builder.Property(e => e.ProductOrderTypeId).IsRequired();
        builder.Property(e => e.ProductPeriodId).IsRequired();
        builder.Property(e => e.ProductSupplierId);
        builder.Property(e => e.OrderDate).IsRequired();
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.Note).HasColumnType("json");
    }
}