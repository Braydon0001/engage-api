// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductSubCategoryConfiguration : IEntityTypeConfiguration<ProductSubCategory>
{
    public void Configure(EntityTypeBuilder<ProductSubCategory> builder)
    {
        builder.Property(e => e.ProductSubCategoryId).IsRequired();
        builder.Property(e => e.ProductCategoryId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}