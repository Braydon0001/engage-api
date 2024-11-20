// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductManufacturerConfiguration : IEntityTypeConfiguration<ProductManufacturer>
{
    public void Configure(EntityTypeBuilder<ProductManufacturer> builder)
    {
        builder.Property(e => e.ProductManufacturerId).IsRequired();
        builder.Property(e => e.ProductSupplierId).IsRequired();
        builder.Property(e => e.Code).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}