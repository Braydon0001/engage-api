// auto-generated
namespace Engage.Persistence.Configurations;

public class InventoryGroupConfiguration : IEntityTypeConfiguration<InventoryGroup>
{
    public void Configure(EntityTypeBuilder<InventoryGroup> builder)
    {
        builder.Property(e => e.InventoryGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
    }
}