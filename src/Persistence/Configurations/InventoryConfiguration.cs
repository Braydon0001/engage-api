namespace Engage.Persistence.Configurations;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.Property(e => e.InventoryId).IsRequired();
        builder.Property(e => e.InventoryGroupId).IsRequired();
        builder.Property(e => e.InventoryStatusId).IsRequired();
        builder.Property(e => e.InventoryUnitTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
        builder.Property(e => e.BarCode).HasMaxLength(100);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}