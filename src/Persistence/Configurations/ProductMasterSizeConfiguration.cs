// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductMasterSizeConfiguration : IEntityTypeConfiguration<ProductMasterSize>
{
    public void Configure(EntityTypeBuilder<ProductMasterSize> builder)
    {
        builder.Property(e => e.ProductMasterSizeId).IsRequired();
        builder.Property(e => e.ProductMasterId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}