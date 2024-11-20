namespace Engage.Persistence.Configurations;

public class SparSubProductConfiguration : IEntityTypeConfiguration<SparSubProduct>
{
    public void Configure(EntityTypeBuilder<SparSubProduct> builder)
    {
        builder.Property(e => e.SparSubProductId).IsRequired();
        builder.Property(e => e.SparProductId).IsRequired();
        builder.Property(e => e.DCCode).IsRequired().HasMaxLength(30);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Barcode).HasMaxLength(30);
        builder.Property(e => e.CaseBarcode).HasMaxLength(30);
        builder.Property(e => e.ShrinkBarcode).HasMaxLength(30);
        builder.Property(e => e.PalletBarcode).HasMaxLength(30);
        builder.Property(e => e.IsPrimary).IsRequired();
        builder.Property(e => e.SparSubProductStatusId).IsRequired();
        builder.Property(e => e.SparSourceId);
        builder.Property(e => e.DistributionCenterId).IsRequired();
        builder.Property(e => e.Warehouse).HasMaxLength(30);
        builder.Property(e => e.StockOnHand);
        builder.Property(e => e.StockOnOrder);
        builder.Property(e => e.Note).HasMaxLength(220);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}