// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductPackSizeTypeConfiguration : IEntityTypeConfiguration<ProductPackSizeType>
{
    public void Configure(EntityTypeBuilder<ProductPackSizeType> builder)
    {
        builder.Property(e => e.ProductPackSizeTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}