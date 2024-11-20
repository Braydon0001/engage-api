// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductVendorConfiguration : IEntityTypeConfiguration<ProductVendor>
{
    public void Configure(EntityTypeBuilder<ProductVendor> builder)
    {
        builder.Property(e => e.ProductVendorId).IsRequired();
        builder.Property(e => e.Code).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}