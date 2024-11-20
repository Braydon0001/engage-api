// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.ProductId).IsRequired();
        builder.Property(e => e.ProductMasterId).IsRequired();
        builder.Property(e => e.RelatedProductId);
        builder.Property(e => e.ProductWarehouseId).IsRequired();
        builder.Property(e => e.ProductSizeTypeId).IsRequired();
        builder.Property(e => e.ProductPackSizeTypeId).IsRequired();
        builder.Property(e => e.ProductModuleStatusId).IsRequired();
        builder.Property(e => e.ProductSystemStatusId).IsRequired();
        builder.Property(e => e.ProductMasterColorId);
        builder.Property(e => e.ProductMasterSizeId);
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.Code).HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(200);
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.ProductSize).IsRequired();
        builder.Property(e => e.ProductPackSize).IsRequired();
    }
}