namespace Engage.Persistence.Configurations;

public class DCStockOnHandConfiguration : IEntityTypeConfiguration<DCStockOnHand>
{
    public void Configure(EntityTypeBuilder<DCStockOnHand> builder)
    {
        builder.Property(e => e.DCStockOnHandId).IsRequired();
        builder.Property(e => e.DCProductId).IsRequired();
        builder.Property(e => e.OnOrderQuantity).IsRequired();
        builder.Property(e => e.StockDate).IsRequired();
        builder.Property(e => e.Value).IsRequired();
        builder.Property(e => e.Note).HasMaxLength(200);
    }
}