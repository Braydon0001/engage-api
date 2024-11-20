// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductMasterColorConfiguration : IEntityTypeConfiguration<ProductMasterColor>
{
    public void Configure(EntityTypeBuilder<ProductMasterColor> builder)
    {
        builder.Property(e => e.ProductMasterColorId).IsRequired();
        builder.Property(e => e.ProductMasterId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}