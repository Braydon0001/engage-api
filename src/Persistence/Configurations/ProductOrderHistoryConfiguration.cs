namespace Engage.Persistence.Configurations;

public class ProductOrderHistoryConfiguration : IEntityTypeConfiguration<ProductOrderHistory>
{
    public void Configure(EntityTypeBuilder<ProductOrderHistory> builder)
    {
        builder.Property(e => e.ProductOrderHistoryId).IsRequired();
        builder.Property(e => e.ProductOrderId).IsRequired();
        builder.Property(e => e.ProductOrderStatusId).IsRequired();
        builder.Property(e => e.Reason).HasMaxLength(120);
    }
}