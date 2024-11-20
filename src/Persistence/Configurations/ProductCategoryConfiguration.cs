// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.Property(e => e.ProductCategoryId).IsRequired();
        builder.Property(e => e.ProductSubGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}