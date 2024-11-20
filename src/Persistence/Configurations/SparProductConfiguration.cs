namespace Engage.Persistence.Configurations;

public class SparProductConfiguration : IEntityTypeConfiguration<SparProduct>
{
    public void Configure(EntityTypeBuilder<SparProduct> builder)
    {
        builder.Property(e => e.SparProductId).IsRequired();
        builder.Property(e => e.ItemCode).IsRequired().HasMaxLength(30);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.UnitSize);
        builder.Property(e => e.SparUnitTypeId);
        builder.Property(e => e.Barcode).IsRequired().HasMaxLength(30);
        builder.Property(e => e.EngageBrandId).IsRequired();
        builder.Property(e => e.SupplierId).IsRequired();
        builder.Property(e => e.EngageSubCategoryId).IsRequired();
        builder.Property(e => e.SparProductStatusId).IsRequired();
        builder.Property(e => e.SparAnalysisGroupId).IsRequired();
        builder.Property(e => e.SparSystemStatusId).IsRequired();
        builder.Property(e => e.EvoLedgerId).IsRequired();
        builder.Property(e => e.Files).HasColumnType("json");
    }
}