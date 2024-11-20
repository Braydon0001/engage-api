// auto-generated
namespace Engage.Persistence.Configurations;

public class InventoryWarehouseConfiguration : IEntityTypeConfiguration<InventoryWarehouse>
{
    public void Configure(EntityTypeBuilder<InventoryWarehouse> builder)
    {
        builder.Property(e => e.InventoryWarehouseId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
    }
}