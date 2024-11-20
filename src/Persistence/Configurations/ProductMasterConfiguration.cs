// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductMasterConfiguration : IEntityTypeConfiguration<ProductMaster>
{
    public void Configure(EntityTypeBuilder<ProductMaster> builder)
    {
        builder.Property(e => e.ProductMasterId).IsRequired();
        builder.Property(e => e.ProductBrandId).IsRequired();
        builder.Property(e => e.ProductReasonId).IsRequired();
        builder.Property(e => e.ProductSubCategoryId).IsRequired();
        builder.Property(e => e.ProductMasterStatusId).IsRequired();
        builder.Property(e => e.ProductMasterSystemStatusId).IsRequired();
        builder.Property(e => e.ProductVendorId).IsRequired();
        builder.Property(e => e.ProductManufacturerId).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.Code).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
        builder.Property(e => e.LedgerCode).HasMaxLength(100);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}