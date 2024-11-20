namespace Engage.Persistence.Configurations;

public class ProductAnalysisConfiguration : IEntityTypeConfiguration<ProductAnalysis>
{
    public void Configure(EntityTypeBuilder<ProductAnalysis> builder)
    {
        builder.Property(e => e.Supplier)
               .HasMaxLength(200);

        builder.Property(e => e.Vendor)
                .HasMaxLength(200);

        builder.Property(e => e.Manufacturer)
                 .HasMaxLength(200);

        builder.Property(e => e.Product)
                  .HasMaxLength(200);

        builder.Property(e => e.ProductDescription)
               .HasMaxLength(200);

        builder.Property(e => e.Size)
               .HasMaxLength(100);

        builder.Property(e => e.Key)
               .HasMaxLength(100);

        builder.Property(e => e.Barcode)
               .HasMaxLength(100);

        builder.Property(e => e.LedgerCode)
               .HasMaxLength(100);
    }
}
