namespace Engage.Persistence.Configurations;

public class ProductOrderLineConfiguration : IEntityTypeConfiguration<ProductOrderLine>
{
    public void Configure(EntityTypeBuilder<ProductOrderLine> builder)
    {
        builder.Property(e => e.ProductOrderLineId).IsRequired();
        builder.Property(e => e.ProductOrderId).IsRequired();
        builder.Property(e => e.ProductId).IsRequired();
        builder.Property(e => e.ProductOrderLineStatusId).IsRequired();
        builder.Property(e => e.ProductOrderLineTypeId).IsRequired();
        builder.Property(e => e.Amount).IsRequired();
        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.Note).HasMaxLength(220);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}