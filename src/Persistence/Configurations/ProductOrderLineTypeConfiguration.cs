namespace Engage.Persistence.Configurations;

public class ProductOrderLineTypeConfiguration : IEntityTypeConfiguration<ProductOrderLineType>
{
    public void Configure(EntityTypeBuilder<ProductOrderLineType> builder)
    {
        builder.Property(e => e.ProductOrderLineTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}