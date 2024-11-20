namespace Engage.Persistence.Configurations;

public class ProductFilterUploadConfiguration : IEntityTypeConfiguration<ProductFilterUpload>
{
    public void Configure(EntityTypeBuilder<ProductFilterUpload> builder)
    {
        builder.Property(e => e.Barcode)
            .HasMaxLength(120);

        builder.Property(e => e.Filter)
               .HasMaxLength(120);

        builder.Property(e => e.EngageVariantProductName)
           .HasMaxLength(220);
    }
}
