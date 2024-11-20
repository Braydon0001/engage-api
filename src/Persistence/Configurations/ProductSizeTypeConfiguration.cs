// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductSizeTypeConfiguration : IEntityTypeConfiguration<ProductSizeType>
{
    public void Configure(EntityTypeBuilder<ProductSizeType> builder)
    {
        builder.Property(e => e.ProductSizeTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}