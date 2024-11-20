namespace Engage.Persistence.Configurations;

public class ProductOrderTypeConfiguration : IEntityTypeConfiguration<ProductOrderType>
{
    public void Configure(EntityTypeBuilder<ProductOrderType> builder)
    {
        builder.Property(e => e.ProductOrderTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}