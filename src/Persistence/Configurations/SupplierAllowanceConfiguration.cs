namespace Engage.Persistence.Configurations;

public class SupplierAllowanceConfiguration : IEntityTypeConfiguration<SupplierAllowance>
{
    public void Configure(EntityTypeBuilder<SupplierAllowance> builder)
    {
        builder.Property(e => e.SupplierAllowanceId).IsRequired();
        builder.Property(e => e.SupplierId).IsRequired();
        builder.Property(e => e.Vendor).HasMaxLength(100);
        builder.Property(e => e.NCircular).HasMaxLength(100);
        builder.Property(e => e.WarehouseAllowancePercent);
        builder.Property(e => e.WarehouseAllowanceNote).HasMaxLength(100);
        builder.Property(e => e.RedistributionPercent);
        builder.Property(e => e.RedistributionNote).HasMaxLength(100);
        builder.Property(e => e.SwellPercent);
        builder.Property(e => e.SwellNote).HasMaxLength(100);
        builder.Property(e => e.RebatePercent);
        builder.Property(e => e.RebateNote).HasMaxLength(100);
        builder.Property(e => e.SettlementPercent);
        builder.Property(e => e.SettlementNote).HasMaxLength(100);
        builder.Property(e => e.EncoreHouseAllowancePercent);
        builder.Property(e => e.EncoreHouseAllowanceNote).HasMaxLength(100);
        builder.Property(e => e.EncoreTradeMarketingPercent);
        builder.Property(e => e.EncoreTradeMarketingNote).HasMaxLength(100);
        builder.Property(e => e.AdvertisingMarketingAllowancePercent);
        builder.Property(e => e.AdvertisingMarketingAllowanceNote).HasMaxLength(100);
        builder.Property(e => e.CatmanPercent);
        builder.Property(e => e.CatmanNote).HasMaxLength(100);
        builder.Property(e => e.EngagePercent);
        builder.Property(e => e.EngageNote).HasMaxLength(100);
        builder.Property(e => e.Comment);
        builder.Property(e => e.Note);
        builder.Property(e => e.SupplierSalesLeadId).IsRequired();
        builder.Property(e => e.GlSubCode).HasMaxLength(100);
        builder.Property(e => e.GlMainCode).HasMaxLength(100);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}