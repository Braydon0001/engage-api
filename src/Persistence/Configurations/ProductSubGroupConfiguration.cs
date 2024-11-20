// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductSubGroupConfiguration : IEntityTypeConfiguration<ProductSubGroup>
{
    public void Configure(EntityTypeBuilder<ProductSubGroup> builder)
    {
        builder.Property(e => e.ProductSubGroupId).IsRequired();
        builder.Property(e => e.ProductGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}