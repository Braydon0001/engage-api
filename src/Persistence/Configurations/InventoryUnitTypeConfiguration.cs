// auto-generated
namespace Engage.Persistence.Configurations;

public class InventoryUnitTypeConfiguration : IEntityTypeConfiguration<InventoryUnitType>
{
    public void Configure(EntityTypeBuilder<InventoryUnitType> builder)
    {
        builder.Property(e => e.InventoryUnitTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
    }
}